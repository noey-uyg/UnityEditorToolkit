using UnityEditor;
using UnityEngine;

namespace noeyToolkit
{
    public enum RandomizerMode
    {
        Selection,  // 선택된 오브젝트에 적용
        Spawn       // 선택된 오브젝트를 복제하여 적용
    }

    public class RandomizerUI
    {
        private static RandomizerMode mode = RandomizerMode.Selection;
        private static int spawnCount = 1;
        
        private static Vector3 posRange = Vector3.zero;
        private static Vector3 rotRange = Vector3.zero;
        private static Vector3 scaleMin = Vector3.one;
        private static Vector3 scaleMax = Vector3.one;

        public static void Draw()
        {
            GUILayout.Label("Randomizer Tool", EditorStyles.boldLabel);

            mode = (RandomizerMode)EditorGUILayout.EnumPopup("Mode", mode);

            if (mode == RandomizerMode.Spawn)
            {
                spawnCount = EditorGUILayout.IntSlider("SpawnCount", spawnCount, 1, 100);
            }

            GUILayout.Space(5);
            posRange = EditorGUILayout.Vector3Field("Position Range", posRange);
            rotRange = EditorGUILayout.Vector3Field("Rotation Range", rotRange);
            scaleMin = EditorGUILayout.Vector3Field("Scale Min", scaleMin);
            scaleMax = EditorGUILayout.Vector3Field("Scale Max", scaleMax);
            GUILayout.Space(10);

            if (GUILayout.Button("Apply"))
            {
                var selected = Selection.gameObjects;
                RandomizerLogic.ApplyRandomization(selected, mode, spawnCount, posRange, rotRange, scaleMin, scaleMax);
            }
        }
    }
}
