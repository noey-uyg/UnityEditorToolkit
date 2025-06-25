using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build.Reporting;
using UnityEditor;
using UnityEngine;

namespace noeyToolkit
{
    public class BuildSizeAnalyzerLogic
    {
        private const string LastBuildReportKey = "noeyToolkit_lastBuildPath";

        public class AssetSizeInfo
        {
            public string Path;
            public long SizeBytes;
        }

        public static List<AssetSizeInfo> AnalyzeLastBuild()
        {
            var result = new List<AssetSizeInfo>();
            string buildPath = EditorPrefs.GetString(LastBuildReportKey, "");

            if(string.IsNullOrEmpty(buildPath))
            {
                EditorUtility.DisplayDialog("Build Not Found", "No previous build found. Please build the project using 'Tracked Build' first.", "OK");
                return result;
            }

            var buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray(),
                locationPathName = buildPath,
                target = EditorUserBuildSettings.activeBuildTarget,
                options = BuildOptions.BuildScriptsOnly
            };

            var report = BuildPipeline.BuildPlayer(buildPlayerOptions);

            if(report == null || report.packedAssets == null)
            {
                EditorUtility.DisplayDialog("Report Error", "Failed to analyze build. Please run a full build first.", "OK");
                return result;
            }

            foreach(var packedAsset in report.packedAssets)
            {
                foreach(var content in packedAsset.contents)
                {
                    string assetPath = content.sourceAssetPath;
                    long size = (long)content.packedSize;

                    var existing = result.FirstOrDefault(r => r.Path == assetPath);
                    if(existing != null)
                        existing.SizeBytes += size;
                    else
                        result.Add(new AssetSizeInfo { Path = assetPath, SizeBytes = size });
                }
            }

            return result.OrderByDescending(r => r.SizeBytes).ToList();
        }

        public static void StoreBuildPath(string buildPath)
        {
            EditorPrefs.SetString(LastBuildReportKey, buildPath);
        }
    }
}
