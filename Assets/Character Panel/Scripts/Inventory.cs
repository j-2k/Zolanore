using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : ItemContainer
{
    [SerializeField] Item[] startingItems;
    [SerializeField] Transform itemsParent;

    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;
    public event Action<BaseItemSlot> OnRightClickEvent;
    public event Action<BaseItemSlot> OnBeginDragEvent;
    public event Action<BaseItemSlot> OnDragEvent;
    public event Action<BaseItemSlot> OnEndDragEvent;
    public event Action<BaseItemSlot> OnDropEvent;

    private void Start()
    {
        //listener for the itemslots event
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
            itemSlots[i].OnPointerExitEvent += slot => OnPointerExitEvent(slot);
            itemSlots[i].OnRightClickEvent += slot => OnRightClickEvent(slot);
            itemSlots[i].OnBeginDragEvent += slot => OnBeginDragEvent(slot);
            itemSlots[i].OnDragEvent += slot => OnDragEvent(slot);
            itemSlots[i].OnEndDragEvent += slot => OnEndDragEvent(slot);
            itemSlots[i].OnDropEvent += slot => OnDropEvent(slot);
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
        Clear();
        for (int i = 0; i < startingItems.Length; i++)
        {
            AddItem(startingItems[i].GetCopy());
        }
    }
}
