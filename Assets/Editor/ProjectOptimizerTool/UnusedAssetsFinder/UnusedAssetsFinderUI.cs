using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace noeyToolkit
{
    public class UnusedAssetsFinderUI
    {
        private static List<string> unusedAssets = new List<string>();
        private static Vector2 scrollPos;
        private static bool searchInProgress = false;

        private static HashSet<string> allExtensions = new HashSet<string>();
        private static Dictionary<string, bool> extensionExcludes = new Dictionary<string, bool>();

        private static void InitExtensions()
        {
            allExtensions.Clear();
            extensionExcludes.Clear();

            var assetPaths = AssetDatabase.GetAllAssetPaths();
            foreach (var path in assetPaths)
            {
                if (!path.StartsWith("Assets")) continue;
                string ext = Path.GetExtension(path).ToLower();
                if (string.IsNullOrEmpty(ext)) continue;
                if (!allExtensions.Contains(ext))
                {
                    allExtensions.Add(ext);
                    extensionExcludes[ext] = false; // 기본은 포함
                }
            }
        }

        public static void Draw()
        {
            GUILayout.Label("Unused Assets Finder", EditorStyles.boldLabel);

            if (allExtensions.Count == 0)
            {
                InitExtensions();
            }

            GUILayout.Label("Exclude Extensions", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Select All"))
            {
                foreach (var key in extensionExcludes.Keys.ToList())
                {
                    extensionExcludes[key] = true;
                }
            }
            if (GUILayout.Button("Deselect All"))
            {
                foreach (var key in extensionExcludes.Keys.ToList())
                {
                    extensionExcludes[key] = false;
                }
            }
            EditorGUILayout.EndHorizontal();

            if(GUILayout.Button("Refresh Asset"))
            {
                InitExtensions();
            }

            // 확장자 선택 UI
            EditorGUILayout.BeginVertical(GUI.skin.box);
            foreach (var ext in new List<string>(extensionExcludes.Keys))
            {
                extensionExcludes[ext] = EditorGUILayout.ToggleLeft(ext, extensionExcludes[ext]);
            }
            EditorGUILayout.EndVertical();

            GUILayout.Space(10);

            if (GUILayout.Button("Find Unused Assets"))
            {
                searchInProgress = true;

                // 제외할 확장자 목록 생성
                var excludedExts = new HashSet<string>();
                foreach (var kvp in extensionExcludes)
                {
                    if (kvp.Value)
                        excludedExts.Add(kvp.Key);
                }

                unusedAssets = UnusedAssetsFinderLogic.FindUnusedAssets(excludedExts);
                searchInProgress = false;
            }

            GUILayout.Space(10);

            if (searchInProgress)
            {
                GUILayout.Label("Seraching");
                return;
            }

            if(unusedAssets.Count > 0)
            {
                GUILayout.Label($"Found {unusedAssets.Count} unused assets", EditorStyles.helpBox);
                scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(200));

                foreach(var assetPath in unusedAssets)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.TextField(assetPath);

                    if(GUILayout.Button("Ping", GUILayout.Width(50)))
                    {
                        var obj = AssetDatabase.LoadMainAssetAtPath(assetPath);
                        EditorGUIUtility.PingObject(obj);
                    }

                    if(GUILayout.Button("Delete", GUILayout.Width(50)))
                    {
                        AssetDatabase.DeleteAsset(assetPath);
                    }
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndScrollView();

                GUILayout.Space(10);

                if(GUILayout.Button("Delete All"))
                {
                    if (EditorUtility.DisplayDialog("Confirm Delete", "Are you sure you want to delete all unused assets?", "Delete", "Cancel"))
                    {
                        foreach(var assetPath in unusedAssets)
                        {
                            AssetDatabase.DeleteAsset(assetPath);
                        }

                        unusedAssets.Clear();
                    }
                }
            }
            else
            {
                GUILayout.Label("No unused assets found or not yet scanned", EditorStyles.miniLabel);
            }
        }
    }
}
