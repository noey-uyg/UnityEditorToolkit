using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace noeyToolkit
{
    // ���� ���
    public enum AlignType 
    { 
        Linear, // ���õ� ��ü�� ������ �࿡ ���� ������ �������� ����
        Center, // ���õ� ��ü�� ���õ� ��ġ�� �߽����� �̵�
        Grid    // ���õ� ��ü���� �׸��� ���·� ����
    }

    // ���� ��
    public enum AlignAxis
    {
        X,
        Y,
        Z
    }

    // ���� ������
    public enum AlignOrigin 
    {
        FirstSelected,      // ù��° ���õ� ��ü ����
        CenterOfSelection   // ��� ��ü�� �߽� ����
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
