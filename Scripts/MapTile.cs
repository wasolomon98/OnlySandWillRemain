using UnityEngine;
using System;

[Serializable]
public class MapTile
{
    public TerrainType terrainType;
    public bool isWalkable = true;
    [SerializeField] private byte flags;

    public bool HasRoof
    {
        get => (flags & 1) != 0;
        set => flags = (byte)((flags & ~1) | (value ? 1 : 0));
    }
    
    public bool IsPowered
    {
        get => (flags & 2) != 0;
        set => flags = (byte)((flags & ~2) | (value ? 2 : 0));
    }
}