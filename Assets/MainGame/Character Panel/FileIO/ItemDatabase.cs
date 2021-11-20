using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] Item[] items;

    public Item GetItemReference(string itemID)
    {
        foreach (Item item in items)
        {
            if (item.ID == itemID)
            {
                return item;
            }
        }
        return null;

    }


    public Item GetItemCopy(string itemID)
    {
        Item item = GetItemReference(itemID);

        if (item == null) return null;

        return item.GetCopy();
    }

    #if UNITY_EDITOR
    private void OnValidate()
    {
        LoadItems();
    }

    private void OnEnable()
    {
        EditorApplication.projectChanged += LoadItems;
    }

    private void OnDisable()
    {
        EditorApplication.projectChanged -= LoadItems;
    }

    private void LoadItems ()
    {
        items = FindAssetByType<Item>("Assets/MainGame/Character Panel/Item Related Stuff/ALL_ITEMS");
    }

    //load items in editor automatically for item db
    public static T[] FindAssetByType<T>(params string[] folders) where T : Object
    {
        string type = typeof(T).ToString().Replace("UnityEngine.", "");

        string[] guids;

        if(folders == null || folders.Length == 0)
        {
            guids = AssetDatabase.FindAssets("t:" + type);
        }
        else
        {
            guids = AssetDatabase.FindAssets("t:" + type, folders);
        }

        T[] assets = new T[guids.Length];

        for (int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            assets[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
        }
        return assets;
    }
    #endif
}
