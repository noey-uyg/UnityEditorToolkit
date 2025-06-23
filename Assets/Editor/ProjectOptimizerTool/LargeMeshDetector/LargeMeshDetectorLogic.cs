using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace noeyToolkit
{
    public static class LargeMeshDetectorLogic
    {
        public class MeshInfo
        {
            public GameObject GameObject;
            public int TriangleCount;
        }

        public static List<MeshInfo> FindLargeMeshes(int triangleThreshold)
        {
            var result = new List<MeshInfo>();
            var allRenderers = GameObject.FindObjectsOfType<Renderer>();

            foreach(var renderer in allRenderers)
            {
                Mesh mesh = null;

                // �Ϲ� MeshRenderer�� �� MeshFilter�� �޽� ����
                if(renderer is MeshRenderer meshRenderer)
                {
                    var mf = meshRenderer.GetComponent<MeshFilter>();
                    if (mf != null) mesh = mf.sharedMesh;
                }
                // SkinnedMeshRenderer�� ���
                else if(renderer is SkinnedMeshRenderer skinned)
                {
                    mesh = skinned.sharedMesh;
                }

                if (mesh == null) continue;

                int triangleCount = mesh.triangles.Length / 3;
                if(triangleCount >= triangleThreshold)
                {
                    result.Add(new MeshInfo
                    {
                        GameObject = renderer.gameObject,
                        TriangleCount = triangleCount
                    });
                }
            }

            return result;
        }
    }
}
