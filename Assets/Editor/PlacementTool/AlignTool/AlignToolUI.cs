using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace noeyToolkit
{
    // 정렬 방식
    public enum AlignType 
    { 
        Linear, // 선택된 객체들 지정한 축에 따라 일정한 간격으로 정렬
        Center, // 선택된 객체들 선택된 위치의 중심으로 이동
        Grid    // 선택된 객체들을 그리드 형태로 정렬
    }

    // 정렬 축
    public enum AlignAxis
    {
        X,
        Y,
        Z
    }

    // 정렬 기준점
    public enum AlignOrigin 
    {
        FirstSelected,      // 첫번째 선택된 객체 기준
        CenterOfSelection   // 모든 객체의 중심 기준
    }

    public class AlignToolUI
    {
        private static float linearSpacing = 1f;
        private static bool reverseDirection = false;
        private static AlignType alignType = AlignType.Linear;
        private static AlignAxis alignAxis = AlignAxis.X;
        private static AlignOrigin alignOrigin = AlignOrigin.FirstSelected;
        private static int rowCount = 2;
        private static int colCount = 2;
        private static Vector2 gridSpacing = new Vector2(1f, 1f);

        public static void Draw()
        {
            GUILayout.Label("Align Objects", EditorStyles.boldLabel);

            alignType = (AlignType)EditorGUILayout.EnumPopup("Type", alignType);

            switch (alignType)
            {
                case AlignType.Linear:
                    {
                        alignAxis = (AlignAxis)EditorGUILayout.EnumPopup("Axis", alignAxis);
                        alignOrigin = (AlignOrigin)EditorGUILayout.EnumPopup("Origin", alignOrigin);
                        linearSpacing = EditorGUILayout.FloatField("Spacing", linearSpacing);
                        reverseDirection = EditorGUILayout.Toggle("Reverse Direction", reverseDirection);
                    }
                    break;
                case AlignType.Grid:
                    {
                        rowCount = EditorGUILayout.IntField("Rows", rowCount);
                        colCount = EditorGUILayout.IntField("Columns", colCount);
                        gridSpacing = EditorGUILayout.Vector2Field("Spacing", gridSpacing);
                    }
                    break;

            }

            GUILayout.Space(10);

            if (GUILayout.Button("Apply"))
            {
                var selected = Selection.gameObjects;
                AlignToolLogic.AlignObjects(selected, linearSpacing, reverseDirection, alignType, alignAxis, alignOrigin, rowCount, colCount, gridSpacing);
            }
        }
    }
}
