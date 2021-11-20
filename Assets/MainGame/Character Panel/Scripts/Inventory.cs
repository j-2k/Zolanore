using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : ItemContainer
{
    [SerializeField] Item[] startingItems;
    [SerializeField] Transform itemsParent;

    protected override void Awake()
    {
        base.Awake();
        SetStartingItems();
    }

    protected override void OnValidate()
    {
        if (itemsParent != null)
        {
            itemsParent.GetComponentsInChildren<ItemSlot>(includeInactive: true, result: itemSlots); //add objects that are even disabled
        }

        SetStartingItems();
    }

    void SetStartingItems()
    {
        Clear();
        foreach (Item item in startingItems)
        {
            AddItem(item.GetCopy());
        }
    }
}
