using UnityEngine;
using System;

public class MapRenderer : MonoBehaviour
{
    [SerializeField] private MapData mapData;
    [SerializeField] private TerrainSprites terrainSprites;
    [SerializeField] private Transform mapContainer;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private float tileSize = 1f;
    
    private SpriteRenderer[,] tileRenderers;
    private Camera mainCamera;
    
    private void Start()
    {
        mainCamera = Camera.main;
        InitializeMap();
        CenterMap();
    }

    private void CenterMap()
    {
        // Calculate center position for the map
        float mapWidth = mapData.Width * tileSize;
        float mapHeight = mapData.Height * tileSize;
        
        // Move the map container so (0,0) is at the bottom-left of the map
        // and the center of the map aligns with world origin (0,0)
        mapContainer.position = new Vector3(
            -mapWidth / 2f,
            -mapHeight / 2f,
            0f
        );

        // Optional: Move camera to center of map
        if (mainCamera != null)
        {
            // Calculate how much the camera should see
            float desiredHeight = mapHeight * 1.1f; // 1.1f adds a small margin
            mainCamera.orthographicSize = desiredHeight / 2f;
            
            // Center camera on map
            mainCamera.transform.position = new Vector3(0, 0, mainCamera.transform.position.z);
        }
    }
    
    private void InitializeMap()
    {
        if (mapData == null || terrainSprites == null)
        {
            Debug.LogError("MapData or TerrainSprites not assigned!");
            return;
        }

        tileRenderers = new SpriteRenderer[mapData.Width, mapData.Height];
        
        for (int y = 0; y < mapData.Height; y++)
        {
            for (int x = 0; x < mapData.Width; x++)
            {
                CreateTile(x, y);
            }
        }
    }
    
    private void CreateTile(int x, int y)
    {
        Vector3 position = new Vector3(x * tileSize, y * tileSize, 0);
        GameObject tileObj = Instantiate(tilePrefab, position, Quaternion.identity, mapContainer);
        
        SpriteRenderer renderer = tileObj.GetComponent<SpriteRenderer>();
        MapTile tileData = mapData.GetTile(x, y);
        
        // Get sprite data from TerrainSprites
        var terrainData = terrainSprites.GetTerrainData(tileData.terrainType);
        if (terrainData != null)
        {
            renderer.sprite = terrainData.sprite;
            tileData.isWalkable = terrainData.isWalkable;
        }
        
        renderer.sortingOrder = 0;
        tileObj.transform.localScale = new Vector3(tileSize, tileSize, 1f);
        
        tileRenderers[x, y] = renderer;
    }

    // Helper method to convert world position to tile coordinates
    public Vector2Int WorldToTilePosition(Vector3 worldPos)
    {
        // Account for map container offset
        Vector3 localPos = worldPos - mapContainer.position;
        int x = Mathf.FloorToInt(localPos.x / tileSize);
        int y = Mathf.FloorToInt(localPos.y / tileSize);
        return new Vector2Int(x, y);
    }
    
    // Helper method to convert tile coordinates to world position
    public Vector3 TileToWorldPosition(int x, int y)
    {
        return mapContainer.position + new Vector3(x * tileSize, y * tileSize, 0);
    }
}