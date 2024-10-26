using UnityEngine;
using System;

[CreateAssetMenu(fileName = "MapData", menuName = "Game/Map Data")]
public class MapData : ScriptableObject
{
    public int width = 50;  // Default size
    public int height = 50;
    [SerializeField] private MapTile[] tiles;
    
    public int Width => width;
    public int Height => height;

    public void GenerateMap()
    {
        tiles = new MapTile[width * height];
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = new MapTile
            {
                terrainType = TerrainType.Dirt, // Default terrain
                isWalkable = true
            };
        }
    }

    public MapTile GetTile(int x, int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
            return null;
            
        return tiles[y * width + x];
    }

    public void SetTile(int x, int y, TerrainType terrainType)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
            return;

        int index = y * width + x;
        if (tiles[index] == null)
            tiles[index] = new MapTile();
            
        tiles[index].terrainType = terrainType;
    }
}