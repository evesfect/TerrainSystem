using UnityEngine;



public class ButtonTest : MonoBehaviour
{

    public RenderTexture rt;
    public string savePath;



    [ContextMenu("SaveTexture")]
    public void SaveTexture()
    {
        if (rt != null && savePath != null)
        {
            TextureUtils.SaveRTAsPNG(rt, savePath);
        }
    }
}
