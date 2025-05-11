using UnityEditor;
using UnityEngine;

namespace noeyToolkit
{
    public enum Axis
    {
        X,Y,Z
    }

    public enum MirrorPivotMode
    {
        IndividualOrigins,
        GlobalOrigin,
        SelectionCenter
    }

    public class MirrorObjectUI
    {
        private static Axis mirrorAxis = Axis.X;
        private static MirrorPivotMode pivotMode = MirrorPivotMode.IndividualOrigins;

        public static void Draw()
        {
            GUILayout.Label("Mirror Tool", EditorStyles.boldLabel);

            mirrorAxis = (Axis)EditorGUILayout.EnumPopup("Mirror Axis", mirrorAxis);
            pivotMode = (MirrorPivotMode)EditorGUILayout.EnumPopup("Pivot Mode", pivotMode);

            GUILayout.Space(10);

            if(GUILayout.Button("Mirror Selected Objects"))
            {
                GameObject[] selectedObjects = Selection.gameObjects;
                MirrorObjectLogic.MirrorSelectedObjects(selectedObjects, mirrorAxis, pivotMode);
            }
        }
    }
}
