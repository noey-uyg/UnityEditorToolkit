using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace noeyToolkit
{
    public class LargeMeshDetectorUI
    {
        private static int triangleThreshold = 5000;
        private static List<LargeMeshDetectorLogic.MeshInfo> largeMeshes = new();
        private static Vector2 scrollPos;

        public static void Draw()
        {
            GUILayout.Label("Large Mesh Detector", EditorStyles.boldLabel);

            triangleThreshold = EditorGUILayout.IntField("Triangle Threshold", triangleThreshold);

            if(GUILayout.Button("Scan Scene"))
            {
                largeMeshes = LargeMeshDetectorLogic.FindLargeMeshes(triangleThreshold);
            }

            GUILayout.Space(10);

            if(largeMeshes.Count > 0)
            {
                GUILayout.Label($"Found {largeMeshes.Count} large meshes", EditorStyles.helpBox);

                scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(400));
                foreach(var meshInfo in largeMeshes)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.ObjectField(meshInfo.GameObject.name, meshInfo.GameObject, typeof(GameObject), true);
                    GUILayout.Label($"Tris : {meshInfo.TriangleCount}");

                    if(GUILayout.Button("Ping", GUILayout.Width(50)))
                    {
                        EditorGUIUtility.PingObject(meshInfo.GameObject);
                    }

                    EditorGUILayout.EndHorizontal();
                }

                GUILayout.EndScrollView();
            }
            else
            {
                GUILayout.Label("No large meshes found or not yet scanned.", EditorStyles.miniLabel);
            }
        }
    }
}