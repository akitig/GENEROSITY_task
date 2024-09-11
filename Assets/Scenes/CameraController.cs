using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // シーン管理用の名前空間
using System.Collections;
using TMPro;

public class CameraController : MonoBehaviour
{
    public RawImage display;
    public GameObject countdownText;
    private WebCamTexture webcamTexture;
    public static Texture2D capturedPhoto;  // 撮影した写真を保持


    void Start()
    {
        webcamTexture = new WebCamTexture();
        display.texture = webcamTexture;
        webcamTexture.Play();  // カメラ映像を再生
    }

    void OnDisable()
    {
        // webcamTextureがnullでない場合のみStop()を呼び出す
        if (webcamTexture != null)
        {
            webcamTexture.Stop();
        }
    }

    public void TakePhoto()
    {
        StartCoroutine(CapturePhotoAfterDelay(3));  // 3秒後に撮影
    }

    IEnumerator CapturePhotoAfterDelay(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
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
            countdownText.SetActive(false);
        }

        CapturePhoto();  // 写真を撮影

        // 撮影が完了したら、シーン2に遷移する
        SceneManager.LoadScene("PhotoScene");
    }

    void CapturePhoto()
    {
        capturedPhoto = new Texture2D(webcamTexture.width, webcamTexture.height);
        capturedPhoto.SetPixels(webcamTexture.GetPixels());
        capturedPhoto.Apply();

        ApplyMonochrome(capturedPhoto);

        // 写真を保存する (シーン間で写真を渡すなら保存も可能)
        byte[] bytes = capturedPhoto.EncodeToPNG();
        string path = Application.persistentDataPath + "/photo.png";
        System.IO.File.WriteAllBytes(path, bytes);

        Debug.Log("Photo saved to: " + path);
    }

    void ApplyMonochrome(Texture2D photo)
    {
        for (int y = 0; y < photo.height; y++)
        {
            for (int x = 0; x < photo.width; x++)
            {
                Color color = photo.GetPixel(x, y);
                float gray = (color.r + color.g + color.b) / 3f;
                photo.SetPixel(x, y, new Color(gray, gray, gray));
            }
        }
        photo.Apply();
    }
}
