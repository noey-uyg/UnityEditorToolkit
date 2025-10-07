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
   [MenuItem("GameObject/Prefabs/Prefabs/Cube", false, 1)]
   public static void Create_Cube() => CreatePrefabs("Assets/Prefabs/Cube.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Buttons/Close_Button", false, 1)]
   public static void Create_Close_Button() => CreatePrefabs("Assets/Dark UI/Prefabs Buttons/Close Button.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Buttons/Content_1", false, 1)]
   public static void Create_Content_1() => CreatePrefabs("Assets/Dark UI/Prefabs Buttons/Content 1.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Buttons/Facebook_Button", false, 1)]
   public static void Create_Facebook_Button() => CreatePrefabs("Assets/Dark UI/Prefabs Buttons/Facebook Button.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Buttons/Info_Button", false, 1)]
   public static void Create_Info_Button() => CreatePrefabs("Assets/Dark UI/Prefabs Buttons/Info Button.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Buttons/Inventory_Button", false, 1)]
   public static void Create_Inventory_Button() => CreatePrefabs("Assets/Dark UI/Prefabs Buttons/Inventory Button.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Buttons/Item_List", false, 1)]
   public static void Create_Item_List() => CreatePrefabs("Assets/Dark UI/Prefabs Buttons/Item List.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Buttons/Messages_Button", false, 1)]
   public static void Create_Messages_Button() => CreatePrefabs("Assets/Dark UI/Prefabs Buttons/Messages Button.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Buttons/Missions_Button", false, 1)]
   public static void Create_Missions_Button() => CreatePrefabs("Assets/Dark UI/Prefabs Buttons/Missions Button.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Buttons/Music", false, 1)]
   public static void Create_Music() => CreatePrefabs("Assets/Dark UI/Prefabs Buttons/Music.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Buttons/Notification", false, 1)]
   public static void Create_Notification() => CreatePrefabs("Assets/Dark UI/Prefabs Buttons/Notification.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Buttons/Play_Button", false, 1)]
   public static void Create_Play_Button() => CreatePrefabs("Assets/Dark UI/Prefabs Buttons/Play Button.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Buttons/Ranking_Button", false, 1)]
   public static void Create_Ranking_Button() => CreatePrefabs("Assets/Dark UI/Prefabs Buttons/Ranking Button.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Buttons/Send_Hearts_BTN", false, 1)]
   public static void Create_Send_Hearts_BTN() => CreatePrefabs("Assets/Dark UI/Prefabs Buttons/Send Hearts BTN.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Buttons/Settings_Button", false, 1)]
   public static void Create_Settings_Button() => CreatePrefabs("Assets/Dark UI/Prefabs Buttons/Settings Button.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Buttons/Shop", false, 1)]
   public static void Create_Shop() => CreatePrefabs("Assets/Dark UI/Prefabs Buttons/Shop.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Buttons/Sound", false, 1)]
   public static void Create_Sound() => CreatePrefabs("Assets/Dark UI/Prefabs Buttons/Sound.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Panel/Facebook_Login", false, 1)]
   public static void Create_Facebook_Login() => CreatePrefabs("Assets/Dark UI/Prefabs Panel/Facebook Login.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Panel/Inventory_Panel", false, 1)]
   public static void Create_Inventory_Panel() => CreatePrefabs("Assets/Dark UI/Prefabs Panel/Inventory Panel.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Panel/Lose", false, 1)]
   public static void Create_Lose() => CreatePrefabs("Assets/Dark UI/Prefabs Panel/Lose.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Panel/Missions", false, 1)]
   public static void Create_Missions() => CreatePrefabs("Assets/Dark UI/Prefabs Panel/Missions.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Panel/My_Info_Panel", false, 1)]
   public static void Create_My_Info_Panel() => CreatePrefabs("Assets/Dark UI/Prefabs Panel/My Info Panel.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Panel/Panel", false, 1)]
   public static void Create_Panel() => CreatePrefabs("Assets/Dark UI/Prefabs Panel/Panel.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Panel/Ranking", false, 1)]
   public static void Create_Ranking() => CreatePrefabs("Assets/Dark UI/Prefabs Panel/Ranking.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Panel/Settings", false, 1)]
   public static void Create_Settings() => CreatePrefabs("Assets/Dark UI/Prefabs Panel/Settings.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Panel/Shop_1", false, 1)]
   public static void Create_Shop_1() => CreatePrefabs("Assets/Dark UI/Prefabs Panel/Shop.prefab");
   [MenuItem("GameObject/Prefabs/Dark UI/Prefabs Panel/Win", false, 1)]
   public static void Create_Win() => CreatePrefabs("Assets/Dark UI/Prefabs Panel/Win.prefab");

}