using UnityEngine;
using UnityEditor;

public class CreatePrefab
{
    public static void CreatePrefabs(string assetRelativePath)
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetRelativePath);

        if (prefab != null)
        {
            var instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

            if (Selection.activeTransform != null)
            {
                instance.transform.SetParent(Selection.activeTransform, false);
            }

            EditorGUIUtility.PingObject(instance);
            Undo.RegisterCreatedObjectUndo(instance, "Create " + instance.name);
        }
        else
        {
            Debug.LogError("No Prefab : " + assetRelativePath);
        }
    }

    //Create

}