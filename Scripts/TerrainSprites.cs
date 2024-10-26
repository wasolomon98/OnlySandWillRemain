using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TerrainSprites", menuName = "Game/Terrain Sprites")]
public class TerrainSprites : ScriptableObject
{
    [Serializable]
    public class TerrainSpriteData
    {
        public TerrainType terrainType;
        public Sprite sprite;
        public bool isWalkable = true;
    }

    public List<TerrainSpriteData> terrainSprites = new List<TerrainSpriteData>();

    // Cache dictionary for quick lookup
    private Dictionary<TerrainType, TerrainSpriteData> spriteCache;

    private void OnEnable()
    {
        InitializeCache();
    }

    private void InitializeCache()
    {
        spriteCache = new Dictionary<TerrainType, TerrainSpriteData>();
        foreach (var data in terrainSprites)
        {
            spriteCache[data.terrainType] = data;
        }
    }

    public TerrainSpriteData GetTerrainData(TerrainType type)
    {
        if (spriteCache == null)
        {
            InitializeCache();
        }
        return spriteCache.TryGetValue(type, out var data) ? data : null;
    }
}