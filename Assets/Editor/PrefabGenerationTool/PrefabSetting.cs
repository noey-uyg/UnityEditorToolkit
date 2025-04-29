using UnityEditor;
using UnityEngine;

namespace noeyToolkit
{
    public class PrefabSettings : EditorWindow
    {
        private bool isAutoMode = true;

        [MenuItem("Tools/Prefab Settings")]
        public static void ShowWindow()
        {
            GetWindow<PrefabSettings>("Prefab Settings");
        }

        private void OnGUI()
        {
            GUILayout.Label("Prefab Auto Generation Settings", EditorStyles.boldLabel);

            bool newIsAutoMode = EditorGUILayout.Toggle("Auto Mode", isAutoMode);

            if (newIsAutoMode != isAutoMode)
            {
                isAutoMode = newIsAutoMode;
                EditorPrefs.SetBool("PrefabAutoMode", isAutoMode);
            }
        }
    }
}
