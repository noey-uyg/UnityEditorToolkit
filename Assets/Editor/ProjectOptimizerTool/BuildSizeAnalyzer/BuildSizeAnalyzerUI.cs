using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace noeyToolkit
{
    public class BuildSizeAnalyzerUI
    {
        public static void Draw()
        {
            GUILayout.Label("Build Size Analyzer", EditorStyles.boldLabel);

            if(GUILayout.Button("Analyze Last Build"))
            {
                var assets = BuildSizeAnalyzerLogic.AnalyzeLastBuild();

                if (assets == null || assets.Count == 0)
                    return;

                long totalBytes = 0;
                int displayCount = Mathf.Min(10, assets.Count);
                var sb = new StringBuilder();
                sb.AppendLine("Top " + displayCount + "Assets by Size:\n");

                for(int i = 0; i < displayCount; i++)
                {
                    var a = assets[i];
                    totalBytes += a.SizeBytes;
                    sb.AppendLine($"{i + 1}. {a.Path} - {EditorUtility.FormatBytes(a.SizeBytes)}");
                }

                sb.AppendLine($"\nTotal Count : {assets.Count}");
                sb.AppendLine($"Estimated Size : {EditorUtility.FormatBytes(totalBytes)}");

                EditorUtility.DisplayDialog("Build Size Summary", sb.ToString(), "OK");

                Debug.Log("<color=cyan>[BuildSizeAnalyzer]</color> Full Result:");
                foreach(var asset in assets)
                {
                    Debug.Log($"{asset.Path} - {EditorUtility.FormatBytes(asset.SizeBytes)}");
                }
            }
        }
    }
}

