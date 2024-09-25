using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public CameraView View;
    private CameraModel Model;
    private WebCamTexture webcamTexture;

    void Start()
    {
        Model = new CameraModel();
        webcamTexture = new WebCamTexture();
        View.ShowPhoto(webcamTexture);
        webcamTexture.Play();
    }

    void OnDisable()
    {
        if (webcamTexture != null)
        {
            webcamTexture.Stop();
        }
    }

    public void TakePhoto()
    {
        StartCoroutine(CapturePhotoAfterDelay(3));
    }

    IEnumerator CapturePhotoAfterDelay(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            View.UpdateCountdown(i);
            yield return new WaitForSeconds(1);
        }

        View.HideCountdown();
        Model.CapturePhoto(webcamTexture);

        View.ShowPhoto(CameraModel.CapturedPhoto);

        ChangeToPhotoScene();
    }

    public void ChangeToPhotoScene()
    {
        SceneManager.LoadScene("PhotoScene");
    }
}