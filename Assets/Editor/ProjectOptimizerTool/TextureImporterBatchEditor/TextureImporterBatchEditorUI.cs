using Codice.Client.BaseCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace noeyToolkit
{
    public class TextureImporterBatchEditorUI
    {
        private static int maxSize = 1024;
        private static TextureImporterCompression compression = TextureImporterCompression.Compressed;
        private static bool enableMipMaps = true;
        private static bool applyToAndroid = true;
        private static bool applyToIOS = false;
        private static bool detectAlphaChannel = true;
        private static TextureImporterType textureType = TextureImporterType.Default;

        private static readonly int[] sizeOptions = new[] { 16, 32, 64, 128, 256, 512, 1024, 2048, 4096};
        private static readonly string[] sizeLabels = System.Array.ConvertAll(sizeOptions, s => s.ToString());

        public static void Draw()
        {
            GUILayout.Label("Textrue Improter Batch Editor", EditorStyles.boldLabel);

            maxSize = EditorGUILayout.IntPopup("Max Texture Size", maxSize, sizeLabels, sizeOptions);
            compression = (TextureImporterCompression)EditorGUILayout.EnumPopup("Compression", compression);
            enableMipMaps = EditorGUILayout.Toggle("Gnerate Mip Maps", enableMipMaps);
            textureType = (TextureImporterType)EditorGUILayout.EnumPopup("Texture Type", textureType);

            GUILayout.Space(5);
            GUILayout.Label("Playform Overrides", EditorStyles.boldLabel);
            applyToAndroid = EditorGUILayout.Toggle("Apply to Android", applyToAndroid);
            applyToIOS = EditorGUILayout.Toggle("Apply to IOS", applyToIOS);

            GUILayout.Space(5);
            detectAlphaChannel = EditorGUILayout.Toggle("Detect Alpha Channel", detectAlphaChannel);

            GUILayout.Space(10);
            if(GUILayout.Button("Apply To Selected Textures"))
            {
                var selectedTextures = Selection.GetFiltered<Texture2D>(SelectionMode.DeepAssets);
                if(selectedTextures.Length == 0)
                {
                    EditorUtility.DisplayDialog("No Texture Selected", "Please select one or more textures in the Project view.", "OK");
                    return;
                }

                int updated = TextureImporterBatchEditorLogic.ApplySettingsToTextrues(selectedTextures, maxSize, compression, enableMipMaps, applyToAndroid, applyToIOS, detectAlphaChannel, textureType);
                EditorUtility.DisplayDialog("Batch Apply Complete", $"Update {updated} texture.", "OK");
            }

            GUILayout.Space(10);
            GUILayout.Label("Preview", EditorStyles.boldLabel);
            var previewTextures = Selection.GetFiltered<Texture2D>(SelectionMode.DeepAssets);

            foreach(var tex in previewTextures)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(AssetPreview.GetAssetPreview(tex), GUILayout.Width(64), GUILayout.Height(64));
                GUILayout.Label(tex.name);
                GUILayout.EndHorizontal();
            }
        }
    }
}
