using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace noeyToolkit
{
    public class RandomizerLogic : MonoBehaviour
    {
        public static void ApplyRandomization(GameObject[] targets, RandomizerMode mode, int spawnCount, Vector3 posRange, Vector3 rotRange, Vector3 scaleMin, Vector3 scaleMax)
        {
            if (targets == null || targets.Length == 0)
                return;

            if(mode == RandomizerMode.Selection)
            {
                foreach(var v in targets)
                {
                    Undo.RecordObject(v.transform, "Randomize Transform");
                    ApplyRandomToTransform(v.transform, posRange, rotRange, scaleMin, scaleMax);
                }
            }
            else if (mode == RandomizerMode.Spawn)
            {
                if(targets.Length != 1)
                {
                    Debug.LogError("[Randomizer]Spawn mode requires exactly one selected object");
                    return;
                }

                GameObject original = targets[0];

                GameObject parent = new GameObject(original.name + "_RandomizedGroup");
                Undo.RegisterCreatedObjectUndo(parent, "Create Randomized Parent");

                for(int i = 0; i < spawnCount; i++)
                {
                    GameObject clone = null;

                    if (PrefabUtility.IsPartOfPrefabAsset(original))
                    {
                        clone = (GameObject)PrefabUtility.InstantiatePrefab(original);
                    }
                    else
                    {
                        clone = GameObject.Instantiate(original);
                        clone.name = original.name + "_Clone";
                    }

                    if(clone == null)
                    {
                        Debug.LogError("[Randomizer] Failed to instantiate object.");
                        return;
                    }

                    Undo.RegisterCreatedObjectUndo(clone, "Spawn Randomized Object");

                    clone.transform.SetParent(parent.transform);
                    ApplyRandomToTransform(clone.transform, posRange, rotRange, scaleMin, scaleMax);
                }
            }
        }

        private static void ApplyRandomToTransform(Transform transform, Vector3 posRange, Vector3 rotRange, Vector3 scaleMin, Vector3 scaleMax)
        {
            transform.localPosition += new Vector3(
                Random.Range(-posRange.x, posRange.x),
                Random.Range(-posRange.y, posRange.y),
                Random.Range(-posRange.z, posRange.z)
            );

            transform.localEulerAngles += new Vector3(
                Random.Range(-rotRange.x, rotRange.x),
                Random.Range(-rotRange.y, rotRange.y),
                Random.Range(-rotRange.z, rotRange.z)
            );

            transform.localScale = new Vector3(
                Random.Range(scaleMin.x, scaleMax.x),
                Random.Range(scaleMin.y, scaleMax.y),
                Random.Range(scaleMin.z, scaleMax.z)
            );
        }
    }
}

