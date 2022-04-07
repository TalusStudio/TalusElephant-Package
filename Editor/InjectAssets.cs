using System;
using System.Collections.Generic;
using System.IO;
using Editor;

using UnityEditor;
using UnityEngine;

namespace ElephantSDK
{
    public class InjectAssets
    {
        public const string TALUS_BACKEND_KEYWORD = "ENABLE_BACKEND";

        public const string ScenePath = "Packages/com.talus.taluselephant/";
        public const string AssetPath = "Packages/com.talus.taluselephant/UI/Textures/Resources/";

        [UnityEditor.Callbacks.DidReloadScripts]
        public static void OnReloadScripts()
        {
            string path = "Assets/StreamingAssets";

            if (!AssetDatabase.IsValidFolder(path))
            {
                AssetDatabase.CreateFolder("Assets", "StreamingAssets");
            }

            try
            {
                FileUtil.CopyFileOrDirectory(Path.Combine(AssetPath, "idfa_4c.png"),
                    Path.Combine(Application.streamingAssetsPath, "idfa_4c.png"));
                FileUtil.CopyFileOrDirectory(Path.Combine(AssetPath, "idfa_bg.png"),
                    Path.Combine(Application.streamingAssetsPath, "idfa_bg.png"));
                FileUtil.CopyFileOrDirectory(Path.Combine(AssetPath, "arrow2.png"),
                    Path.Combine(Application.streamingAssetsPath, "arrow2.png"));

                // copy elephant-scene
                string elephantScenePath = Application.dataPath + "/Scenes/Template_Persistent";
                FileUtil.CopyFileOrDirectory(Path.Combine(ScenePath, "elephant_scene.unity"),
                    Path.Combine(elephantScenePath, "elephant_scene.unity"));

                Debug.Log("elephant_scene copied to: " + elephantScenePath);

                if (!DefineSymbols.Contains(TALUS_BACKEND_KEYWORD))
                {
                    DefineSymbols.Add(TALUS_BACKEND_KEYWORD);
                    Debug.Log("Activating Talus-Elephant-Backend!..");
                }

                // add elephant scene to the active scenes.
                var editorBuildSettingsScenes = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
                var elephantScene = new EditorBuildSettingsScene(elephantScenePath, true);

                if (!editorBuildSettingsScenes.Contains(elephantScene))
                {
                    editorBuildSettingsScenes.Insert(0, elephantScene);
                    EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();

                    Debug.Log("elephant_scene registered as entry point in build settings!..");
                }
            }
            catch (Exception e)
            {
                // Ignore
            }
        }
    }
}