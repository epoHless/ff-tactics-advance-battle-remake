
using System;
using System.Collections.Generic;
using GridSystem;
using UnityEditor;
using UnityEngine;

public class PathHelperWindow : EditorWindow
{
    private Tile startTile;
    private Tile endTile;
    private Character character;

    private List<Vector3> positions = new List<Vector3>();

    [MenuItem("Utilities/Pathing/Path Finder")]
    private static void ShowWindow()
    {
        var window = GetWindow<PathHelperWindow>();
        window.titleContent = new GUIContent("Path Finder");
        window.Show();
    }

    private void OnFocus()
    {
        SceneView.duringSceneGui += SceneViewOnduringSceneGui;
    }

    private void SceneViewOnduringSceneGui(SceneView obj)
    {
        Handles.BeginGUI();

        if (positions.Count > 0 && positions != null)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                Handles.color = Color.blue;
                if(i < positions.Count) Handles.DrawLine(positions[i], positions[i++]);
            }
            
        }
        
        SceneView.lastActiveSceneView.Repaint();
        Handles.EndGUI();
    }

    private void OnGUI()
    {
        character = (Character)EditorGUILayout.ObjectField(character, typeof(Character), true);
        startTile = (Tile)EditorGUILayout.ObjectField(startTile, typeof(Tile), true);
        endTile = (Tile)EditorGUILayout.ObjectField(endTile, typeof(Tile), true);

        
        if (GUILayout.Button("Calculate path"))
        {
            if (startTile && endTile)
            {
                var path = PathFinder.GetPath(character, endTile);

                foreach (var tile in path)
                {
                    tile.SelectionBox.SetActive(true);
                    positions.Add(tile.transform.position);
                }
            }
        }
        
        if (GUILayout.Button("Remove path"))
        {
            if (startTile && endTile)
            {
                var path = PathFinder.GetPath(character, endTile);

                foreach (var tile in path)
                {
                    tile.SelectionBox.SetActive(false);
                    positions.Add(tile.transform.position);
                }
            }
        }

        EditorGUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Get Range"))
        {
            if (character.Movement.MovementData.Range > 0)
            {
                foreach (var tile in PathFinder.GetTilesInRange(character))
                {
                    tile.SelectionBox.SetActive(true);
                }
            }            
        }
        
        if (GUILayout.Button("Disable Range"))
        {
            if (character.Movement.MovementData.Range > 0)
            {
                foreach (var tile in PathFinder.GetTilesInRange(character))
                {
                    tile.SelectionBox.SetActive(false);
                }
            }            
        }
        
        EditorGUILayout.EndHorizontal();
    }
}
