using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhotoSceneController : MonoBehaviour
{
    public RawImage photoDisplay;

    void Start()
    {
        if (CameraModel.CapturedPhoto)
            photoDisplay.texture = CameraModel.CapturedPhoto;
    }

    public void ReturnToCameraScene()
    {
        SceneManager.LoadScene("CameraScene");
    }
}