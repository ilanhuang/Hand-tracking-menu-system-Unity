using UnityEngine.XR.Hands;   
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class BaseStateAudioUpdater : EditorWindow
{
    private string statesFolder = "";
    private string audiosFolder = "";

    private AudioClip indexTipaudio;
    private AudioClip middleTipaudio;
    private AudioClip ringTipaudio;
    private AudioClip littleTipaudio;
    private AudioClip exitaudio; // back_1

    private Dictionary<XRHandJointID, AudioClip> jointaudioMapping = new Dictionary<XRHandJointID, AudioClip>();

    [MenuItem("Tools/Update audios in BaseStates")]
    public static void ShowWindow()
    {
        GetWindow<BaseStateAudioUpdater>("Update BaseStates audios");
    }

    private void OnGUI()
    {
        GUILayout.Label("Select BaseStates Folder", EditorStyles.boldLabel);
        if (GUILayout.Button("Select Folder"))
        {
            statesFolder = EditorUtility.OpenFolderPanel("Select BaseStates Folder", "Assets", "");
            if (!string.IsNullOrEmpty(statesFolder))
            {
                statesFolder = "Assets" + statesFolder.Replace(Application.dataPath, "");
            }
        }
        GUILayout.Label($"States Folder: {statesFolder}");

        GUILayout.Space(10);

        GUILayout.Label("Select audios Folder", EditorStyles.boldLabel);
        if (GUILayout.Button("Select Folder"))
        {
            audiosFolder = EditorUtility.OpenFolderPanel("Select audios Folder", "Assets", "");
            if (!string.IsNullOrEmpty(audiosFolder))
            {
                audiosFolder = "Assets" + audiosFolder.Replace(Application.dataPath, "");
            }
        }
        GUILayout.Label($"audios Folder: {audiosFolder}");

        GUILayout.Space(10);
        GUILayout.Label("Assign audios to Joint IDs", EditorStyles.boldLabel);
        indexTipaudio = (AudioClip)EditorGUILayout.ObjectField("Index Tip audio", indexTipaudio, typeof(AudioClip), false);
        middleTipaudio = (AudioClip)EditorGUILayout.ObjectField("Middle Tip audio", middleTipaudio, typeof(AudioClip), false);
        ringTipaudio = (AudioClip)EditorGUILayout.ObjectField("Ring Tip audio", ringTipaudio, typeof(AudioClip), false);
        littleTipaudio = (AudioClip)EditorGUILayout.ObjectField("Little Tip audio", littleTipaudio, typeof(AudioClip), false);

        GUILayout.Space(10);
        GUILayout.Label("Exit audio for All Updated States", EditorStyles.boldLabel);
        exitaudio = (AudioClip)EditorGUILayout.ObjectField("Exit audio (back_1)", exitaudio, typeof(AudioClip), false);

        GUILayout.Space(10);
        if (GUILayout.Button("Apply to Selected Folder"))
        {
            if (string.IsNullOrEmpty(statesFolder) || string.IsNullOrEmpty(audiosFolder))
            {
                EditorUtility.DisplayDialog("Error", "Please select both a BaseStates folder and a audios folder.", "OK");
                return;
            }

            UpdateBaseStateaudios();
        }
    }

    private void UpdateBaseStateaudios()
    {
        // Mapping joint IDs to their corresponding audios
        jointaudioMapping.Clear();
        jointaudioMapping[XRHandJointID.IndexTip] = indexTipaudio;
        jointaudioMapping[XRHandJointID.MiddleTip] = middleTipaudio;
        jointaudioMapping[XRHandJointID.RingTip] = ringTipaudio;
        jointaudioMapping[XRHandJointID.LittleTip] = littleTipaudio;

        string[] guids = AssetDatabase.FindAssets("t:BaseState", new[] { statesFolder });
        int updatedCount = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            BaseState state = AssetDatabase.LoadAssetAtPath<BaseState>(path);

            if (state != null)
            {
                VisualSettings visualSettings = state.GetVisualSettings();
                AudioSettings audioSettings = state.GetAudioSettings();

                if (visualSettings != null && audioSettings != null)
                {
                    Undo.RecordObject(state, "Update BaseState audios");

                    // Set enterAudio based on the joint ID
                    if (jointaudioMapping.TryGetValue(visualSettings.jointID, out AudioClip matchedaudio) && matchedaudio != null)
                    {
                        audioSettings.enterAudio = matchedaudio;
                    }

                    // Set exitAudio to back_1 for all updated states
                    if (exitaudio != null)
                    {
                        audioSettings.exitAudio = exitaudio;
                    }

                    EditorUtility.SetDirty(state); // Mark as modified
                    updatedCount++;
                }
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Updated audios in {updatedCount} BaseStates.");
        EditorUtility.DisplayDialog("Update Complete", $"Updated audios in {updatedCount} BaseStates.", "OK");
    }
}
