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
            // ��� ���� ��� ����
            string[] allAssetPaths = AssetDatabase.GetAllAssetPaths()
                .Where(path =>
                path.StartsWith("Assets/") &&
                !Directory.Exists(path) &&
                !excludedExtensions.Contains(Path.GetExtension(path).ToLower()))
                .ToArray();

            // ���� ���ÿ� ���Ե� �� ������� ���� ���� ����
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

            // ������ �ʴ� ���� ���͸�
            var unusedAssets = allAssetPaths
                .Where(path => !usedAssets.Contains(path))
                .ToList();

            return unusedAssets;
        }
    }

}