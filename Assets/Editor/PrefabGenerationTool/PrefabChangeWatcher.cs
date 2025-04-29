using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace noeyToolkit
{
    public class PrefabChangeWatcher : AssetPostprocessor
    {
        private const string CacheKey = "noeyToolkit_CachedPrefab";

        // ������ �߰�/����/�̵� �� �ڵ����� CreatePrefab.cs �����
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isCompiling)
                return;

            // �ڵ� ����� ��쿡�� ó��
            if (!EditorPrefs.GetBool("PrefabAutoMode", true))
                return;

            // ����� ���� ��� �� �������� �ִ��� Ȯ��
            bool prefabChanged = importedAssets.Concat(deletedAssets)
                .Concat(movedAssets)
                .Concat(movedFromAssetPaths)
                .Any(path => path.EndsWith(".prefab"));

            if (!prefabChanged)
                return;

            string[] currentPrefabs = Directory.GetFiles(Application.dataPath, "*.prefab", SearchOption.AllDirectories)
                .Select(fullPath => "Assets" + fullPath.Replace(Application.dataPath, "").Replace("\\", "/"))
                .OrderBy(x => x)
                .ToArray();

            string current = string.Join("\n", currentPrefabs).GetHashCode().ToString();

            string previous = EditorPrefs.GetString(CacheKey, "");

            if (current != previous)
            {
                AutoGenerateCreatePrefabCode.RefeshPrefab();
                EditorPrefs.SetString(CacheKey, current);
            }
        }
    }
}
