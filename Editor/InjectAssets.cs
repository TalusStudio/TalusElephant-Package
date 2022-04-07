using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ElephantSDK
{
    public class InjectAssets
    {
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
            }
            catch (Exception e)
            {
                // Ignore
            }
        }
    }
}