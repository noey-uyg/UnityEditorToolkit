using UnityEditor;
using UnityEngine;

namespace noeyToolkit
{
    public class PrefabSettings : EditorWindow
    {
        private bool _isAutoMode = true;

        [MenuItem("Tools/NoeyToolkit/Prefab Settings")]
        public static void ShowWindow()
        {
            GetWindow<PrefabSettings>("Prefab Settings");
        }

        private void OnGUI()
        {
            GUILayout.Label("Prefab Auto Generation Settings", EditorStyles.boldLabel);

            bool newIsAutoMode = EditorGUILayout.Toggle("Auto Mode", _isAutoMode);

            if (newIsAutoMode != _isAutoMode)
            {
                _isAutoMode = newIsAutoMode;
                EditorPrefs.SetBool("PrefabAutoMode", _isAutoMode);
            }
        }
    }
}
