using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<Item> startingItems;
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot[] itemSlots;

    public event Action<Item> OnItemRightClickEvent;

    private void Start()
    {
        //listener for the itemslots event
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnRightClickEvent += OnItemRightClickEvent;
        }
        SetStartingItems();
    }

    private void OnValidate()
    {
        if (itemsParent != null)
        {
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
        }

        SetStartingItems();
    }

    void SetStartingItems()
    {
        int i = 0;

        //for every item we have we assign it to a item slot
        for (; i < startingItems.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = startingItems[i];
        }

        //for remaining slot with no items set to null
        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
        }
    }

    public bool AddItem(Item item)
    {
        //loop through all item slots the first null slot will place the item in it 
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == null)
            {
                itemSlots[i].Item = item;
                return true;
            }
        }
        return false;
    }

    public bool RemoveItem(Item item)
    {
        //vice versa of add
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == item)
            {
                itemSlots[i].Item = null;
                return true;
            }
        }
        return false;
    }

    public bool isInventoryFull()
    {
        //go through all slots if any slot is null != full
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == null)
            {
                return false;
            }
        }

        //go through all slots if 0 slot is null == full
        return true;
    }

}
