using UnityEngine;

public class FlatTextureCreator : MonoBehaviour
{
    [Header("Texture Settings")]
    public Color flatColor = Color.white;
    public int textureWidth = 512;
    public int textureHeight = 512;

    [Header("Output")]
    public string outputPath = "./Assets/Output/flat_texture.png";

    [ContextMenu("Create and Save Flat Texture")]
    public void CreateAndSaveFlatTexture()
    {
        // Create texture
        Texture2D texture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);

        // Fill with flat color
        Color[] pixels = new Color[textureWidth * textureHeight];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = flatColor;
        }

        // Apply pixels and save
        texture.SetPixels(pixels);
        texture.Apply();

        // Save as PNG
        byte[] bytes = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(outputPath, bytes);

        // Clean up
        DestroyImmediate(texture);

        Debug.Log($"Flat texture saved to: {outputPath}");

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }
}