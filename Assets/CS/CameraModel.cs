using UnityEngine;
public class CameraModel
{
    public static Texture2D CapturedPhoto { get; private set; }

    public void CapturePhoto(WebCamTexture webcamTexture)
    {
        CapturedPhoto = new Texture2D(webcamTexture.width, webcamTexture.height);
        CapturedPhoto.SetPixels(webcamTexture.GetPixels());
        CapturedPhoto.Apply();
        ApplyMonochrome(CapturedPhoto);
    }

    private void ApplyMonochrome(Texture2D photo)
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