
using System;
using System.Collections.Generic;
using System.Linq;
using GridSystem;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    private List<Tile> tiles;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate Tile Relations"))
        {
            if (tiles.Count <= 0) return;
            
            foreach (var tile in tiles)
            {
                tile.GetNeighbors();
            }
        }
    }

    private void Awake()
    {
        tiles = FindObjectsOfType<Tile>().ToList();
    }
}
