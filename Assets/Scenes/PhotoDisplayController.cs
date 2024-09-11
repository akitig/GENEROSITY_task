using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene2Controller : MonoBehaviour
{
    public RawImage photoDisplay;  // 写真を表示するためのRawImage
    public Button exitButton;      // 終了ボタン

    void Start()
    {
        // Scene1で撮影された写真が存在するか確認
        if (CameraController.capturedPhoto != null)
        {
            // RawImageがアサインされているか確認してから表示
            if (photoDisplay != null)
            {
                photoDisplay.texture = CameraController.capturedPhoto;
            }
            else
            {
                Debug.LogError("RawImage component is not assigned in the Inspector.");
            }
        }
        else
        {
            Debug.LogError("No photo captured in CameraController.");
        }

        // 終了ボタンにリスナーを追加して、シーン1に戻る
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ReturnToScene1);
        }
        else
        {
            Debug.LogError("Exit button is not assigned in the Inspector.");
        }
    }

    public void ReturnToScene1()
    {
        // シーン1に戻る
        SceneManager.LoadScene("CameraScene");
    }
}
