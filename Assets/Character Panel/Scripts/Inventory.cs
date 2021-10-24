using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : ItemContainer
{
    [SerializeField] Item[] startingItems;
    [SerializeField] Transform itemsParent;

    protected override void Start()
    {
        base.Start();
        SetStartingItems();
    }

    protected override void OnValidate()
    {
        if (itemsParent != null)
        {
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>(includeInactive: true); //add objects that are even disabled
        }

        SetStartingItems();
    }

    void SetStartingItems()
    {
        Clear();
        for (int i = 0; i < startingItems.Length; i++)
        {
            AddItem(startingItems[i].GetCopy());
        }
    }
}
