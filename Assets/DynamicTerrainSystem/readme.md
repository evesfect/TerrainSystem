#Created by evesfect

How to use the DynamicTerrain Asset

Setup Requirements:
A game object with DynamicTerrainManager attached,
An ortographic camera with culling mask set to TerrainMeshes layer, post-process disabled, output texture set to a render texture
A terrain object with TerrainHeightmapUpdater attached, enable "Draw Instanced" 
Target meshes should be in "TerrainMeshes" layer, they should all use M_HeightShader

Usage:
You can udpate the camera and terrain from DynamicTerrainManager context menu (right click)
You can then apply render texture to terrain from the same context menu, this function auto flushes to terrain data
You can attach the render texture to a canvas + raw image to view what the scan camera sees.

A prefab is provided which not includes the terrain object.
Don't forget to create a layer "TargetMeshes" and set it for your target meshes.
