using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace noeyToolkit
{
    public class PrefabChangeWatcher : AssetPostprocessor
    {
        private const string CacheKey = "noeyToolkit_CachedPrefab";

        // 프리팹 추가/삭제/이동 시 자동으로 CreatePrefab.cs 재생성
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isCompiling)
                return;

            // 자동 모드일 경우에만 처리
            if (!EditorPrefs.GetBool("PrefabAutoMode", true))
                return;

            // 변경된 에셋 목록 중 프리팹이 있는지 확인
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
