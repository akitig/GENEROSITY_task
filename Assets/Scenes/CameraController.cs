using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;  // TextMeshProUGUIを使う場合に必要

public class CameraController : MonoBehaviour
{
    public RawImage display;              // Webカメラの映像を表示するためのRawImage
    public GameObject countdownText;      // カウントダウン表示用のTextまたはTextMeshProUGUI
    private WebCamTexture webcamTexture;  // Webカメラ用のテクスチャ

    void Start()
    {
        // Webカメラの初期化
        webcamTexture = new WebCamTexture();
        display.texture = webcamTexture;
        webcamTexture.Play();  // カメラ映像を再生
    }

    void OnDisable()
    {
        webcamTexture.Stop();  // カメラ映像を停止
    }

    // シャッターボタンを押したときに呼ばれる
    public void TakePhoto()
    {
        StartCoroutine(CapturePhotoAfterDelay(3));  // 3秒後に撮影
    }

    // カウントダウンを表示してから写真を撮影する
    IEnumerator CapturePhotoAfterDelay(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            // TextかTextMeshProUGUIのどちらかが存在するかを確認して、カウントダウン表示を更新
            if (countdownText != null)
            {
                if (countdownText.GetComponent<Text>() != null)
                {
                    countdownText.GetComponent<Text>().text = i.ToString();
                }
                else if (countdownText.GetComponent<TextMeshProUGUI>() != null)
                {
                    countdownText.GetComponent<TextMeshProUGUI>().text = i.ToString();
                }
            }
            yield return new WaitForSeconds(1);
        }

        if (countdownText != null)
        {
            countdownText.SetActive(false);  // カウントダウンを非表示に
        }

        CapturePhoto();  // 写真を撮影してモノトーン加工を適用
    }

    // 写真を撮影し、モノトーン加工を適用
    void CapturePhoto()
    {
        Texture2D photo = new Texture2D(webcamTexture.width, webcamTexture.height);
        photo.SetPixels(webcamTexture.GetPixels());
        photo.Apply();

        // モノトーンフィルターを適用
        ApplyMonochrome(photo);

        // 写真を保存する
        byte[] bytes = photo.EncodeToPNG();
        string path = Application.persistentDataPath + "/photo.png";
        System.IO.File.WriteAllBytes(path, bytes);

        Debug.Log("Photo saved to: " + path);
    }

    // モノトーンフィルターを適用する関数
    void ApplyMonochrome(Texture2D photo)
    {
        for (int y = 0; y < photo.height; y++)
        {
            for (int x = 0; x < photo.width; x++)
            {
                Color color = photo.GetPixel(x, y);
                float gray = (color.r + color.g + color.b) / 3f;  // グレースケールに変換
                photo.SetPixel(x, y, new Color(gray, gray, gray));
            }
        }
        photo.Apply();
    }
}
