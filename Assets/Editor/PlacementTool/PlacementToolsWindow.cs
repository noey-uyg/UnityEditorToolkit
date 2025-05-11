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
            AlignTool,
            PrefabReplacer,
            GroupByType,
            Mirror,
            Randomizer
        }

        private Dictionary<PlacementToolType, Action> tabActions; // ���� ���� ó�� �Լ� ����
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
                { PlacementToolType.AlignTool, AlignToolUI.Draw },
                { PlacementToolType.PrefabReplacer, PrefabReplacerUI.Draw },
                { PlacementToolType.GroupByType, GroupByTypeTab },
                { PlacementToolType.Mirror, MirrorObjectUI.Draw },
                { PlacementToolType.Randomizer, RandomizerUI.Draw }
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

        private void GroupByTypeTab()
        { 
            GUILayout.Label("Group by Type", EditorStyles.boldLabel);
        }
    }
}

