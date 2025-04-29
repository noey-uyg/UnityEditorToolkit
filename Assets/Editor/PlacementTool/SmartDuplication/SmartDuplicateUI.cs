using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace noeyToolkit
{
    public enum NumberingFormat
    {
        Underscore,     // Cube_01
        Dash,           // Cube-1
        Parentheses,    // Cube(1)
        Dot,            // Cube.01
        Hash,           // Cube#1
        LeadingZeros3   // Cube001
    }

    public class SmartDuplicateUI : EditorWindow
    {
        private static int count = 1;
        private static Vector3 posOffset, rotOffset, scaleOffset;
        private static NumberingFormat format;

        public static void Draw()
        {
            GUILayout.Label("Smart Duplicate", EditorStyles.boldLabel);

            count = EditorGUILayout.IntSlider("Count", count, 1, 100);
            format = (NumberingFormat)EditorGUILayout.EnumPopup("Numbering", format);
            posOffset = EditorGUILayout.Vector3Field("Position Offset", posOffset);
            rotOffset = EditorGUILayout.Vector3Field("Rotation Offset", rotOffset);
            scaleOffset = EditorGUILayout.Vector3Field("Scale Offset", scaleOffset);

            GUILayout.Space(10);
            GUILayout.Label("Preview of Numbering Format:", EditorStyles.boldLabel);
            ShowNumberingPreview();
            GUILayout.Space(10);
            if (GUILayout.Button("Duplicate Selected"))
            {
                SmartDuplicateLogic.Duplicate(count, format, posOffset, rotOffset, scaleOffset);
            }
        }

        private static void ShowNumberingPreview()
        {
            string baseName = "GameObject";
            for (int i = 0; i < 3; i++)
            {
                string numberedName = GetFormatName(baseName, i + 1);
                GUILayout.Label($"{numberedName}");
            }
        }

        private static string GetFormatName(string baseName, int index)
        {
            switch (format)
            {
                case NumberingFormat.Underscore:
                    return $"{baseName}_{index:D2}";
                case NumberingFormat.Dash:
                    return $"{baseName}-{index}";
                case NumberingFormat.Dot:
                    return $"{baseName}.{index:D2}";
                case NumberingFormat.Hash:
                    return $"{baseName}#{index}";
                case NumberingFormat.LeadingZeros3:
                    return $"{baseName}{index:D3}";
                default:
                    return $"{baseName}({index})";
            }
        }
    }
}

