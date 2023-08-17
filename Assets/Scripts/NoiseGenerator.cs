using System.IO;
using MyBox;
using UnityEngine;

public class NoiseGenerator : MonoBehaviour
{
    [Tooltip("Relative to 'Assets/' folder")]
    [SerializeField] private string savePath;
    [Tooltip("File name (with extension!)")]
    [SerializeField] private string fileName = "Noise.png";
    [SerializeField] private Material material;
    [SerializeField] private Vector2Int textureResolution = new Vector2Int(128, 128);

    [ButtonMethod]
    private void GenerateNoise()
    {
        RenderTexture renderTexture = RenderTexture.GetTemporary(textureResolution.x, textureResolution.y, 0);
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        texture.filterMode = FilterMode.Point;

        Graphics.SetRenderTarget(renderTexture);
        Graphics.Blit(texture, renderTexture, material, 0);
        Graphics.SetRenderTarget(null);

        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        byte[] bytes = texture.EncodeToPNG();

        File.WriteAllBytes(Path.Combine(Application.dataPath, savePath, fileName), bytes);
    }
}