using UnityEditor;
using UnityEngine;

namespace noeyToolkit
{
    public enum Axis
    {
        X,Y,Z
    }

    public class MirrorObjectUI
    {
        private static Axis mirrorAxis = Axis.X;

        public static void Draw()
        {
            GUILayout.Label("Mirror Tool", EditorStyles.boldLabel);

            mirrorAxis = (Axis)EditorGUILayout.EnumPopup("Mirror Axis", mirrorAxis);
            GUILayout.Space(10);

            if(GUILayout.Button("Mirror Selected Objects"))
            {
                GameObject[] selectedObjects = Selection.gameObjects;
                MirrorObjectLogic.MirrorSelectedObjects(selectedObjects, mirrorAxis);
            }
        }
    }
}
