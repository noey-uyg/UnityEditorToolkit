using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;

namespace noeyToolkit
{
    public class UnusedAssetsFinderLogic
    {
        public static List<string> FindUnusedAssets(HashSet<string> excludedExtensions)
        {
            // 모든 에셋 경로 수집
            string[] allAssetPaths = AssetDatabase.GetAllAssetPaths()
                .Where(path =>
                path.StartsWith("Assets/") &&
                !Directory.Exists(path) &&
                !excludedExtensions.Contains(Path.GetExtension(path).ToLower()))
                .ToArray();

            // 빌드 세팅에 포함된 씬 기반으로 사용된 에셋 수집
            var usedAssets = new HashSet<string>();
            var scenePaths = EditorBuildSettings.scenes
                .Where(s=>s.enabled)
                .Select(s =>s.path)
                .ToList();

            foreach(var scene in scenePaths)
            {
                var deps = AssetDatabase.GetDependencies(scene, true);
                foreach(var dep in deps)
                {
                    usedAssets.Add(dep);
                }
            }

            // 사용되지 않는 에셋 필터링
            var unusedAssets = allAssetPaths
                .Where(path => !usedAssets.Contains(path))
                .ToList();

            return unusedAssets;
        }
    }

}