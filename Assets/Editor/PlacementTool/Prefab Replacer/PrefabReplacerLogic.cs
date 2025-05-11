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
                Debug.LogError("[PrefabReplacer] Prefab is not assigned");
                return;
            }

            if(targets == null || targets.Length == 0)
            {
                Debug.LogError("[PrefabReplacer] Target no selected");
                return;
            }

            if (!PrefabUtility.IsPartOfPrefabAsset(prefab))
            {
                Debug.LogError("[PrefabReplacer] Selected object is not prefab");
                return;
            }

            foreach(GameObject target in targets)
            {
                Transform originaltransform = target.transform;
                int siblingIndex = originaltransform.GetSiblingIndex();

                GameObject newObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                if(newObject == null )
                {
                    Debug.LogError("[PrefabReplacer] Failed to instantiate prefab");
                    continue;
                }

                Undo.RegisterCreatedObjectUndo(newObject, "Replace With Prefab");
                newObject.transform.SetPositionAndRotation(originaltransform.position, originaltransform.rotation);
                newObject.transform.localScale = originaltransform.localScale;
                newObject.transform.parent = originaltransform.parent;
                newObject.transform.SetSiblingIndex(siblingIndex);

                Undo.DestroyObjectImmediate(target);
            }
        }
    }

}
