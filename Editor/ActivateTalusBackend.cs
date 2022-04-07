using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

namespace Editor
{
    [InitializeOnLoad]
    public class ActivateTalusBackendListener
    {
        private const string TALUS_BACKEND_NAME = "TalusFramework-Backend";
        private const string TALUS_BACKEND_KEYWORD = "ENABLE_BACKEND";

        private const string ELEPHANT_SCENE_PATH = "Assets/Elephant/elephant_scene.unity";

        static ActivateTalusBackendListener() => AssetDatabase.importPackageCompleted += OnImportPackageCompleted;

        private static void OnImportPackageCompleted(string packageName)
        {
            if (packageName != TALUS_BACKEND_NAME)
            {
                return;
            }

            Debug.Log("Activating Talus-Elephant-Backend!..");

            // activate TalusFramework-Backend symbol.
            if (!DefineSymbols.Contains(TALUS_BACKEND_KEYWORD))
            {
                DefineSymbols.Add(TALUS_BACKEND_KEYWORD);
            }

            // add elephant scene to the active scenes.
            var editorBuildSettingsScenes = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
            var elephantScene = new EditorBuildSettingsScene(ELEPHANT_SCENE_PATH, true);

            if (!editorBuildSettingsScenes.Contains(elephantScene))
            {
                editorBuildSettingsScenes.Insert(0, elephantScene);
                EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();
            }
        }
    }
}