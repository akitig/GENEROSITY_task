using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraView : MonoBehaviour
{
    public RawImage Display;
    public GameObject CountdownText;

    public void UpdateCountdown(int seconds)
    {
        var textMeshProComponent = CountdownText.GetComponent<TextMeshProUGUI>();
        if (textMeshProComponent != null)
        {
            textMeshProComponent.text = seconds.ToString();
        }
        else
        {
            Debug.LogError("CountdownText does not have a TextMeshProUGUI component.");
        }
    }

    public void HideCountdown()
    {
        CountdownText.SetActive(false);
    }

    public void ShowPhoto(Texture photo)
    {
        Display.texture = photo;
    }
}