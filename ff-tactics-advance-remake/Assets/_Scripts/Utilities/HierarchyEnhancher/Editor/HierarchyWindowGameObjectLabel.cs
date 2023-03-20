using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

[InitializeOnLoad]
public static class HierarchyWindowGameObjectLabel
{
    public static string LabelsDirectory { get; private set; }
    
    public static List<HierarchyLabelPreset> Presets = new List<HierarchyLabelPreset>();

    private static readonly Color SelectedColor = new Color(44f / 255f, 93f / 255f, 135f / 255f, 1f);
    private static readonly Color UnselectedColor = new Color(56f / 255f, 56f / 255f, 56f / 255f);

    static HierarchyWindowGameObjectLabel()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
        AssemblyReloadEvents.afterAssemblyReload += FetchLabels;
    }

    public static void FetchLabels()
    {
        LabelsDirectory = !Directory.Exists($"{Application.dataPath}/HierarchyLabels") ? 
            AssetDatabase.CreateFolder("Assets", "HierarchyLabels") : 
            "Assets/HierarchyLabels/";

        var assets = AssetDatabase.FindAssets("", new[] { LabelsDirectory });

        Presets = new List<HierarchyLabelPreset>();
        
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

    static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        var content = EditorGUIUtility.ObjectContent(EditorUtility.InstanceIDToObject(instanceID), null);
        
        foreach (var preset in Presets)
        {
            if (content.text.StartsWith(preset.identifier))
            {
                string text = content.text.Remove(0,preset.identifier.Length);

                GUI.Label(selectionRect, text, SetStylePreset(preset, instanceID));

                if (content.image != null && preset.icon)
                {
                    GUI.DrawTexture(new Rect(selectionRect.xMax - 20, selectionRect.yMin, 20, 16), HierarchyUtilities.MakeTex(1,1, EditorUtility.InstanceIDToObject(instanceID) == Selection.activeObject ? SelectedColor : UnselectedColor));
                    GUI.DrawTexture(new Rect(selectionRect.xMax - 16, selectionRect.yMin, 16, 16), preset.icon);
                }
            }
        }
    }

    private static bool IsGameObjectEnabled(int instanceID)
    {
        return EditorUtility.GetObjectEnabled(EditorUtility.InstanceIDToObject(instanceID)) != 0;
    }

    private static GUIStyle SetStylePreset(HierarchyLabelPreset _preset, int instanceID)
    {
        GUIStyle style = new GUIStyle();

        var normalStyleState = IsGameObjectEnabled(instanceID) ? new GUIStyleState
        {
            background = EditorUtility.InstanceIDToObject(instanceID) != Selection.activeObject ? HierarchyUtilities.MakeTex(1, 1, _preset.backgroundColor) : HierarchyUtilities.MakeTex(1, 1, SelectedColor),
            textColor = _preset.textColor
        } : new GUIStyleState
        {
            background = EditorUtility.InstanceIDToObject(instanceID) != Selection.activeObject ? HierarchyUtilities.MakeTex(1, 1, _preset.inactiveBackgroundColor) : HierarchyUtilities.MakeTex(1, 1, SelectedColor),
            textColor = _preset.useCustomInactiveColors ? _preset.inactiveTextColor : HierarchyUtilities.ChangeColorBrightness(_preset.textColor, 0.45f)
        };

        style.normal = normalStyleState;
        
        style.fontStyle = _preset.fontStyle;
        style.alignment = _preset.alignment;
        
        return style;
    }

    public static void AddPreset(HierarchyLabelPreset _preset)
    {
        if (!Presets.Contains(_preset))
        {
            Presets.Add(_preset);
        }
        
        EditorApplication.RepaintHierarchyWindow();
    }
    
    public static void RemovePreset(HierarchyLabelPreset _preset)
    {
        if (Presets.Contains(_preset))
        {
            Presets.Remove(_preset);
        }
        
        EditorApplication.RepaintHierarchyWindow();
    }
}

#endif