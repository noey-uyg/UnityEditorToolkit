using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace noeyToolkit
{
    public class PrefabReplacerLogic
    {
        public static void ReplaceWithPrefab(GameObject[] targets, GameObject prefab)
        {
            if (prefab == null)
            {
                Debug.LogError("[PrefabReplacer] Prefab Is Not Assigned");
                return;
            }

            if(targets == null || targets.Length == 0)
            {
                Debug.LogError("{PrefabReplacer] Target No Selected");
                return;
            }

            if (!PrefabUtility.IsPartOfPrefabAsset(prefab))
            {
                Debug.LogError("[PrefabReplacer] Selected Object Is Not Prefab");
                return;
            }

            foreach(GameObject target in targets)
            {
                Transform originaltransform = target.transform;

                GameObject newObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                if(newObject == null )
                {
                    Debug.LogError("[PrefabReplacer] Failed To Instantiate Prefab");
                    continue;
                }

                Undo.RegisterCreatedObjectUndo(newObject, "Replace With Prefab");
                newObject.transform.SetPositionAndRotation(originaltransform.position, originaltransform.rotation);
                newObject.transform.localScale = originaltransform.localScale;
                newObject.transform.parent = originaltransform.parent;

                Undo.DestroyObjectImmediate(target);
            }
        }
    }

}
