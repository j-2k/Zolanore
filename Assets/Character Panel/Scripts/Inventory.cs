using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<Item> items;
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot[] itemSlots;

    public event Action<Item> OnItemRightClickEvent;

    private void Start()
    {
        RefreshUI();
        //listener for the itemslots event
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnRightClickEvent += OnItemRightClickEvent;
        }
    }

    private void OnValidate()
    {
        if (itemsParent != null)
        {
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
        }

        RefreshUI();
    }

    void RefreshUI()
    {
        int i = 0;

        //for every item we have we assign it to a item slot
        for (; i < items.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = items[i];
        }

        //for remaining slot with no items set to null
        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
        }
    }

    public bool AddItem(Item item)
    {
        if (isInventoryFull())
        {
            return false;
        }

        items.Add(item);
        RefreshUI();
        return true;
    }

    public bool RemoveItem(Item item)
    {
        if (items.Remove(item))
        {
            RefreshUI();
            return true;
        }

        return false;
    }

    public bool isInventoryFull()
    {
        return items.Count >= itemSlots.Length;
    }

}
