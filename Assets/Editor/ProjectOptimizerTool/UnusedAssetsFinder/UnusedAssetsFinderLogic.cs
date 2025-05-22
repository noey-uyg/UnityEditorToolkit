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
            var unusedAssets = new List<string>();

            string[] allAssetPaths = AssetDatabase.GetAllAssetPaths()
                .Where(path =>
                path.StartsWith("Assets/") &&
                !Directory.Exists(path) &&
                !excludedExtensions.Contains(Path.GetExtension(path).ToLower()))
                .ToArray();

            foreach (var asset in allAssetPaths)
            {
                string[] deps = AssetDatabase.GetDependencies(asset, true);

                if (deps.Length == 1 && deps[0] == asset)
                {
                    unusedAssets.Add(asset);
                }
            }

            return unusedAssets;
        }
    }

}