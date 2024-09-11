using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public RawImage display;
    private WebCamTexture webcamTexture;

    void Start()
    {
        webcamTexture = new WebCamTexture();
        display.texture = webcamTexture;
        webcamTexture.Play();  // カメラの映像を再生開始
    }

    void OnDisable()
    {
        webcamTexture.Stop();  // カメラの映像を停止
    }
}
