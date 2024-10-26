using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapData))]
public class MapDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapData mapData = (MapData)target;

        // Draw default inspector
        DrawDefaultInspector();

        // Add button to generate map
        if (GUILayout.Button("Generate Empty Map"))
        {
            mapData.GenerateMap();
            EditorUtility.SetDirty(mapData);
        }

        // Example buttons for different terrain types
        if (GUILayout.Button("Fill with Grass"))
        {
            for (int y = 0; y < mapData.Height; y++)
            {
                for (int x = 0; x < mapData.Width; x++)
                {
                    mapData.SetTile(x, y, TerrainType.Grass);
                }
            }
            EditorUtility.SetDirty(mapData);
        }
    }
}