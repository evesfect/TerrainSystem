using UnityEngine;

public class TextureUtils : MonoBehaviour
{
    public static Texture2D GetRTPixels(RenderTexture rt)
    {
        RenderTexture currentActiveRT = RenderTexture.active;
        RenderTexture.active = rt;

        Texture2D tex = new Texture2D(rt.width, rt.height);
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
        tex.Apply();

        RenderTexture.active = currentActiveRT;
        return tex;
    }
    public static void SaveRTAsPNG(RenderTexture rt, string filePath)
    {
        Texture2D tex = GetRTPixels(rt);
        byte[] bytes = tex.EncodeToPNG();
        System.IO.File.WriteAllBytes(filePath, bytes);
        DestroyImmediate(tex);

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }
}
