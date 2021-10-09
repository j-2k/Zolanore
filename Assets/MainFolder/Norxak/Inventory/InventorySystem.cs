using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{

    #region Singleton Inventory Instance
    public static InventorySystem instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Another inventory?? bug check");
            return;
        }

        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public List<Item> items = new List<Item>();

    [SerializeField] int inventorySpace = 2;

    void Start()
    {
        Debug.Log("inventory space is " + inventorySpace);
        inventorySpace = 10;
    }

    public bool Add(Item item)
    {

       // Item copyItem = Instantiate(item);

        if (!item.isDefaultItem)
        {

            if (items.Count >= inventorySpace)
            {
                Debug.Log("Inventory Space Is Full!");
                return false;
            }

            /*
            for (int i = 0; i < items.Count; i++)
            {
                if (item.itemName == items[i].itemName && item.itemAmount < item.itemMaxStack)
                {
                    item.itemAmount++;
                    if (onItemChangedCallback != null)
                    {
                        onItemChangedCallback.Invoke();
                    }
                    return true;
                }
            }



            /*
            if (items.Contains(item) && item.itemAmount < item.itemMaxStack)
            {//make reverse of this when removing items;
                item.itemAmount++;
                if (onItemChangedCallback != null)
                {
                    onItemChangedCallback.Invoke();
                }
                return true;
            }*/

            //items.Add(copyItem);
            items.Add(item);
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        }

        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
