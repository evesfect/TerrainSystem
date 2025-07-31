using UnityEngine;

[ExecuteInEditMode]
public class TerrainHeightmapUpdater : MonoBehaviour
{
    [Header("Input Render Texture (matches terrain heightmap resolution)")]
    public RenderTexture sourceHeightmap;

    [Tooltip("Automatically sync terrain LOD after update")]
    public bool autoSync = true;

    private Terrain terrain;
    private TerrainData terrainData;

    void Awake()
    {
        terrain = GetComponent<Terrain>();
        if (terrain == null)
        {
            Debug.LogError("TerrainHeightmapUpdater must be attached to a GameObject with a Terrain component.");
            enabled = false;
            return;
        }

        terrainData = terrain.terrainData;
    }

    [ContextMenu("ApplyTexToHeightmap")]
    public void ApplyRenderTextureToHeightmap()
    {
        if (sourceHeightmap == null)
        {
            Debug.LogError("No RenderTexture assigned.");
            return;
        }

        // Ensure the resolution matches terrainData.heightmapResolution
        if (sourceHeightmap.width != terrainData.heightmapResolution ||
            sourceHeightmap.height != terrainData.heightmapResolution)
        {
            Debug.LogWarning($"RenderTexture resolution ({sourceHeightmap.width}x{sourceHeightmap.height}) does not match Terrain heightmap resolution ({terrainData.heightmapResolution}).");
        }

        // Set active RenderTexture for GPU copy
        RenderTexture.active = sourceHeightmap;

        // Define source rect and destination offset (0,0 = full overwrite)
        RectInt srcRect = new RectInt(0, 0, sourceHeightmap.width, sourceHeightmap.height);
        Vector2Int dest = Vector2Int.zero;

        terrainData.CopyActiveRenderTextureToHeightmap(srcRect, dest, TerrainHeightmapSyncControl.None);

        if (autoSync)
        {
            terrainData.SyncHeightmap();
        }

        RenderTexture.active = null;

        Debug.Log("Heightmap updated from RenderTexture.");
    }
}
