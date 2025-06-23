using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace noeyToolkit
{
    public class ProjectOptimizerWindow : EditorWindow
    {
        private enum OptimizerToolType
        { 
            UnusedAssetsFinder,
            TextureBatchEditor,
            LargeMeshDetector,
            BuildSizeAnalyzer
        }

        private Dictionary<OptimizerToolType, Action> tabActions;
        private OptimizerToolType _selectedToolType = OptimizerToolType.UnusedAssetsFinder;

        [MenuItem("Tools/NoeyToolkit/ProjectOptimizerWindow")]
        public static void ShowWindow()
        {
            GetWindow<ProjectOptimizerWindow>("Project Optimizer Toolkit");
        }

        private void OnEnable()
        {
            tabActions = new Dictionary<OptimizerToolType, Action>
            {
                { OptimizerToolType.UnusedAssetsFinder, UnusedAssetsFinderUI.Draw },
                { OptimizerToolType.TextureBatchEditor, TextureImporterBatchEditorUI.Draw },
                { OptimizerToolType.LargeMeshDetector, LargeMeshDetectorUI.Draw }
            };
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Tool", GUILayout.Width(50));
            _selectedToolType = (OptimizerToolType)EditorGUILayout.EnumPopup(_selectedToolType);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);

            if (tabActions.TryGetValue(_selectedToolType, out var drawAction))
                drawAction?.Invoke();
        }

    }
}
