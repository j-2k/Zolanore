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

    public List<Item> items = new List<Item>(); //itemslist
    [SerializeField] int inventorySpace = 2;

    [SerializeField] Transform itemsParent;
    InventorySlot[] slots;

    void Start()
    {
        Debug.Log("inventory space is " + inventorySpace);
        inventorySpace = 10;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        
    }

    public bool Add(Item item)
    {
       //Item copyItem = Instantiate(item);


        if (!item.isDefaultItem)
        {
            if (items.Count >= inventorySpace)
            {
                Debug.Log("Inventory Space Is Full!");
                return false;
            }

            #region pain
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
            */




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

            /*
            if (item.isStackable)
            {
                bool firstItemInventory = false;
                foreach (Item inventoryItem in items)
                {
                    if (inventoryItem.itemName == item.itemName)
                    {
                        if (inventoryItem.itemAmount <= item.itemMaxStack)
                        {
                            inventoryItem.itemAmount++; //+= item.itemAmount;
                            return true;
                        }
                    }
                }

                if (!firstItemInventory)
                {
                    items.Add(item);
                }
                if (onItemChangedCallback != null)
                {
                    onItemChangedCallback.Invoke();
                }
                return true;
            }*/

            //items.Add(copyItem);

            /*
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].itemName && item. < item.itemMaxStack)
                {
                    item.itemAmount++;
                    if (onItemChangedCallback != null)
                    {
                        onItemChangedCallback.Invoke();
                    }
                    return true;
                }
            }*/

            #endregion

            //best attempt so far without using SOs for stacking
            /*
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].itemName == item.itemName && item.isStackable && slots[i].curAmount < slots[i].maxStack)
                {
                    slots[i].curAmount++;
                    if (onItemChangedCallback != null)
                    {
                        onItemChangedCallback.Invoke();
                    }
                    return true;
                }
            }
            */

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
        //check stack here
        /*
        for (int i = 0; i < slots.Length; i++)
        {
            if (items.Contains(item) && item.isStackable && slots[i].curAmount >= 2)//items[i].itemName == item.itemName
            {
                slots[i].curAmount--;
                if (onItemChangedCallback != null)
                {
                    onItemChangedCallback.Invoke();
                }
                return;
            }
            else if (slots[i].curAmount <= 1)
            {
                items.Remove(item);
                if (onItemChangedCallback != null)
                {
                    onItemChangedCallback.Invoke();
                }
                return;
            }
        }
        */

            items.Remove(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
