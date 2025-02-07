using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class BaseStateUpdater : EditorWindow
{
    private Vector3 newPositionOffset = Vector3.zero;
    private Vector3 newRotationOffset = Vector3.zero;
    private Vector3 newScale = Vector3.one;
    private Color newUIColor = Color.white; // Default color
    private AnimationType newAnimationType = AnimationType.None; // Default animation type

    private List<string> selectedFolders = new List<string>(); // Stores selected folder paths

    [MenuItem("Tools/Update BaseStates in Folders")]
    public static void ShowWindow()
    {
        GetWindow<BaseStateUpdater>("Update BaseStates");
    }

    private void OnGUI()
    {
        GUILayout.Label("Set New Default Values", EditorStyles.boldLabel);

        newPositionOffset = EditorGUILayout.Vector3Field("New Position Offset", newPositionOffset);
        newRotationOffset = EditorGUILayout.Vector3Field("New Rotation Offset", newRotationOffset);
        newScale = EditorGUILayout.Vector3Field("New Scale", newScale);
        newUIColor = EditorGUILayout.ColorField("New UI Color", newUIColor); // Color picker
        newAnimationType = (AnimationType)EditorGUILayout.EnumPopup("New Animation Type", newAnimationType); // Dropdown for animation

        GUILayout.Space(10);

        GUILayout.Label("Selected Folders:", EditorStyles.boldLabel);
        if (selectedFolders.Count == 0)
        {
            GUILayout.Label("No folders selected.");
        }
        else
        {
            foreach (var folder in selectedFolders)
            {
                GUILayout.Label(folder, EditorStyles.miniLabel);
            }
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Select Folders"))
        {
            string folderPath = EditorUtility.OpenFolderPanel("Select Folder", "Assets", "");
            if (!string.IsNullOrEmpty(folderPath))
            {
                string relativePath = "Assets" + folderPath.Replace(Application.dataPath, "");
                if (!selectedFolders.Contains(relativePath))
                {
                    selectedFolders.Add(relativePath);
                }
            }
        }

        if (GUILayout.Button("Clear Selection"))
        {
            selectedFolders.Clear();
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Apply to Selected Folders"))
        {
            if (selectedFolders.Count == 0)
            {
                EditorUtility.DisplayDialog("Error", "No folders selected. Please select at least one folder.", "OK");
                return;
            }

            UpdateBaseStatesInFolders();
        }
    }

    private void UpdateBaseStatesInFolders()
    {
        int updatedCount = 0;

        foreach (string folder in selectedFolders)
        {
            string[] guids = AssetDatabase.FindAssets("t:BaseState", new[] { folder });

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                BaseState state = AssetDatabase.LoadAssetAtPath<BaseState>(path);

                if (state != null)
                {
                    VisualSettings settings = state.GetVisualSettings();
                    AnimationSettings animationSettings = state.GetAnimationSettings();
                    
                    if (settings != null || animationSettings != null)
                    {
                        Undo.RecordObject(state, "Update BaseState Offsets");

                        if (settings != null)
                        {
                            settings.positionOffset = newPositionOffset;
                            settings.rotationOffset = newRotationOffset;
                            settings.scale = newScale;
                            settings.uiColor = newUIColor;
                        }

                        if (animationSettings != null)
                        {
                            animationSettings.enterAnimation = newAnimationType;
                        }

                        EditorUtility.SetDirty(state); // Mark as modified
                        updatedCount++;
                    }
                }
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Updated {updatedCount} BaseStates in selected folders.");
        EditorUtility.DisplayDialog("Update Complete", $"Updated {updatedCount} BaseStates.", "OK");
    }
}
