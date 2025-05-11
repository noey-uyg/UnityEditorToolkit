using UnityEditor;
using UnityEngine;

namespace noeyToolkit
{
    public class PrefabReplacerUI
    {
        private static GameObject prefabToReplaceWith;

        public static void Draw()
        {
            GUILayout.Label("Prefab Replacer Tool", EditorStyles.boldLabel);

            prefabToReplaceWith = (GameObject)EditorGUILayout.ObjectField(
                "Replacement Prefab",
                prefabToReplaceWith,
                typeof(GameObject),
                false);

            GUILayout.Space(10);

            if (GUILayout.Button("Replace"))
            {
                GameObject[] selectedObjects = Selection.gameObjects;
                PrefabReplacerLogic.ReplaceWithPrefab(selectedObjects, prefabToReplaceWith);
            }
        }
    }
}

