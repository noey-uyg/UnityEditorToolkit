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

                // 일반 MeshRenderer일 때 MeshFilter로 메쉬 추출
                if(renderer is MeshRenderer meshRenderer)
                {
                    var mf = meshRenderer.GetComponent<MeshFilter>();
                    if (mf != null) mesh = mf.sharedMesh;
                }
                // SkinnedMeshRenderer인 경우
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
