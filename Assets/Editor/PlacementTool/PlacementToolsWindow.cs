using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace noeyToolkit
{
    public class PlacementToolsWindow : EditorWindow
    {
        private enum PlacementToolType
        {
            SmartDurplicate,
            BatchParenting,
            AlignTool,
            PrefabReplacer,
            GroupByType,
            Mirror,
            Randomizer
        }

        private Dictionary<PlacementToolType, Action> tabActions; // 툴에 따른 처리 함수 저장
        private PlacementToolType _selectedToolType = PlacementToolType.SmartDurplicate;

        [MenuItem("Tools/NoeyToolkit/PlacementTools Window")]
        public static void ShowWindow()
        {
            GetWindow<PlacementToolsWindow>("Placement Toolkit");
        }

        private void OnEnable()
        {
            tabActions = new Dictionary<PlacementToolType, Action>
            {
                { PlacementToolType.SmartDurplicate, SmartDuplicateUI.Draw },
                { PlacementToolType.BatchParenting, BatchParentingTab },
                { PlacementToolType.AlignTool, AlignToolTab },
                { PlacementToolType.PrefabReplacer, PrefabReplacerTab },
                { PlacementToolType.GroupByType, GroupByTypeTab },
                { PlacementToolType.Mirror, MirrorTab },
                { PlacementToolType.Randomizer, RandomizerTab }
            };
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Tool", GUILayout.Width(50));
            _selectedToolType = (PlacementToolType)EditorGUILayout.EnumPopup(_selectedToolType);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);

            if (tabActions.TryGetValue(_selectedToolType, out var drawAction))
                drawAction?.Invoke();
        }

        private void BatchParentingTab()
        { 
            GUILayout.Label("Batch Parenting", EditorStyles.boldLabel);

        }
        private void AlignToolTab()
        { 
            GUILayout.Label("Align Tool", EditorStyles.boldLabel);

        }
        private void PrefabReplacerTab()
        { 
            GUILayout.Label("Prefab Replacer", EditorStyles.boldLabel);

        }
        private void GroupByTypeTab()
        { 
            GUILayout.Label("Group by Type", EditorStyles.boldLabel);

        }
        private void MirrorTab()
        { 
            GUILayout.Label("Mirror Tool", EditorStyles.boldLabel);

        }
        private void RandomizerTab() { 
            GUILayout.Label("Randomizer", EditorStyles.boldLabel);
        }
    }
}

