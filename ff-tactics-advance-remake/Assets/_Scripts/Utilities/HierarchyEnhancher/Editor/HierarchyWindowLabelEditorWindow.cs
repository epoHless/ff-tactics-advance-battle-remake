﻿using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class HierarchyWindowLabelEditorWindow : EditorWindow
{
    private HierarchyLabelPreset selectedPreset = null;
    private List<HierarchyLabelPreset> Presets = new List<HierarchyLabelPreset>();

    private GUIStyle sidebarStyle;
    
    string labelName = String.Empty;

    private Vector2 scrollPos = Vector2.zero;
    
    [MenuItem("Utilities/Hierarchy Labels")]
    private static void ShowWindow()
    {
        var window = GetWindow<HierarchyWindowLabelEditorWindow>();
        window.titleContent = new GUIContent("Hierarchy Editor");
        window.Show();
    }

    private void Awake()
    {
        // FetchPresets();

        Presets = new List<HierarchyLabelPreset>(HierarchyWindowGameObjectLabel.Presets);
        
        sidebarStyle = new GUIStyle()
        {
            hover = new GUIStyleState()
            {
                textColor = Color.cyan
            },
            normal = new GUIStyleState()
            {
                background = HierarchyUtilities.MakeTex(1,1, Color.gray)
            }
        };

        minSize = new Vector2(700, 400);
        maxSize = minSize;

        if(Presets.Count > 0) selectedPreset = Presets[0];
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical();
        
        RenderPresets();
        
        GUILayout.FlexibleSpace();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add All", GUILayout.Width(110)))
        {
            for (int i = 0; i < Presets.Count; i++)
            {
                AddPreset(Presets[i]);
            }
        }
        
        if (GUILayout.Button("Remove All", GUILayout.Width(110)))
        {
            for (int i = 0; i < Presets.Count; i++)
            {
                RemovePreset(Presets[i]);
            }
        }

        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal();

        labelName = GUILayout.TextField(labelName, GUILayout.Width(110));
        
        if (GUILayout.Button("New Label", GUILayout.Width(110)))
        {
            if(labelName != String.Empty) AddNewLabel(labelName);
        }

        GUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        if(selectedPreset) //Create the editor for the selected Preset
        {
            var editor = Editor.CreateEditor(selectedPreset) as LabelColorPresetEditor;

            EditorGUILayout.BeginVertical();

            // Shows the label properties to modify

            GUILayout.Space(10);
            GUILayout.Label(selectedPreset.name.Split('_')[1], new GUIStyle()
            {
                fontStyle = FontStyle.Bold,
                normal = new GUIStyleState()
                {
                    textColor = selectedPreset.textColor
                },
                fontSize = 14
            });
            GUILayout.Space(10);

            editor!.ShowIdentifierIcon();
            GUILayout.Space(20);
            editor!.ShowFontStyleAlignment();
            GUILayout.Space(20);
            editor!.ShowTextColorBGColor();
            GUILayout.Space(20);
            editor!.ShowCustomInactiveColors();
            GUILayout.Space(20);
            GUILayout.FlexibleSpace();
            editor!.ShowPresetButtons();
        
            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndHorizontal();
        
        EditorApplication.RepaintHierarchyWindow();
    }

    /// <summary>
    /// Creates all the label buttons
    /// </summary>
    private void RenderPresets()
    {
        scrollPos = GUILayout.BeginScrollView(scrollPos, false, false);
        
        var guiColor = GUI.color;
        
        for (int i = 0; i < Presets.Count; i++)
        {
            GUILayout.BeginHorizontal();
            
            GUI.color = Presets[i].textColor;

            if (GUILayout.Button(Presets[i].name.Split("_")[1], GUILayout.Width(200)))
            {
                selectedPreset = Presets[i];
            }
            GUI.color = Color.red;

            if (GUILayout.Button("X" , GUILayout.Width(20)))
            {
                DeleteLabel(Presets[i]);
            }

            GUI.color = guiColor;
            
            GUILayout.EndHorizontal();
        }
        
        GUILayout.EndScrollView();
    }

    /// <summary>
    /// Create and store in the default folder a new label with a given name.
    /// </summary>
    /// <param name="name"></param>
    private void AddNewLabel(string name)
    {
        var label = ScriptableObject.CreateInstance<HierarchyLabelPreset>();
        string labelPath = $"Assets/ScriptableObjects/_EditorUtilities/HierarchyLabels/LabelPreset_{name}.asset";

        if (!File.Exists(labelPath))
        {
            AssetDatabase.CreateAsset(label, labelPath);
            
            label.identifier = labelName;
            label.textColor = Color.white;
            label.inactiveTextColor = Color.white;
            label.backgroundColor = new Color(0.2196079f, 0.2196079f, 0.2196079f, 1);
            label.inactiveBackgroundColor = new Color(0.2196079f, 0.2196079f, 0.2196079f, 1);
        
            AddPreset(label);
            HierarchyWindowGameObjectLabel.AddPreset(label);
            EditorApplication.RepaintHierarchyWindow();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            selectedPreset = label;
        }
        else
        {
            EditorUtility.DisplayDialog("ERROR", $"asset already exists at path: {labelPath}", "OK");
        }
        
        labelName = String.Empty;
    }

    /// <summary>
    /// Deletes the selected label from the Assets Folder
    /// </summary>
    /// <param name="_label"></param>
    private void DeleteLabel(HierarchyLabelPreset _label)
    {
        HierarchyWindowGameObjectLabel.RemovePreset(_label);
        RemovePreset(_label);

        string assetPath = $"Assets/ScriptableObjects/_EditorUtilities/HierarchyLabels/{_label.name}.asset";

        if (File.Exists(assetPath))
        {
            AssetDatabase.DeleteAsset(assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            selectedPreset = Presets[0];
        }
        else
        {
            EditorUtility.DisplayDialog("ERROR", $"Could not find asset at path: {assetPath}", "OK");
        }
    }
    
    /// <summary>
    /// Fetch all the Labels ScriptableObjects in the default folder
    /// </summary>
    private void FetchPresets()
    {
        var assets = AssetDatabase.FindAssets("t:ScriptableObject", new[] { "Assets/ScriptableObjects/_EditorUtilities/HierarchyLabels/" });
        
        foreach (var asset in assets)
        {
            var path = AssetDatabase.GUIDToAssetPath(asset);
            var item = AssetDatabase.LoadAssetAtPath(path, typeof(HierarchyLabelPreset)) as HierarchyLabelPreset;
        
            if (item)
            {
                AddPreset(item);
            }
        }
    }

    /// <summary>
    /// Add a preset to the list of presets
    /// </summary>
    /// <param name="_preset"></param>
    private void AddPreset(HierarchyLabelPreset _preset)
    {
        if (!Presets.Contains(_preset))
        {
            Presets.Add(_preset);
        }
        
        HierarchyWindowGameObjectLabel.AddPreset(_preset);
    }
    
    /// <summary>
    /// Remove a preset from the list of presets
    /// </summary>
    /// <param name="_preset"></param>
    private void RemovePreset(HierarchyLabelPreset _preset)
    {
        HierarchyWindowGameObjectLabel.RemovePreset(_preset);
    }
}
