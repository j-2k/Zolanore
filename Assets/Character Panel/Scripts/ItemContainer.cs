using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemContainer : MonoBehaviour, IItemContainer
{
    [SerializeField] protected ItemSlot[] itemSlots;

    public virtual bool AddItem(Item item)
    {
        //loop through all item slots the first null slot will place the item in it 
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == null || itemSlots[i].CanAddStack(item))
            {
                itemSlots[i].Item = item;
                itemSlots[i].Amount++;
                return true;
            }
        }
        return false;
    }

    public virtual bool RemoveItem(Item item)
    {
        //vice versa of add
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == item)
            {
                itemSlots[i].Amount--;
                return true;
            }
        }
        return false;
    }


    //new remove item with ID
    public Item RemoveItem(string itemID)
    {
        //vice versa of add
        for (int i = 0; i < itemSlots.Length; i++)
        {
            Item item = itemSlots[i].Item;
            //go through all items & check the slots if there is a item we compare its id to the ID we are looking for when we find it just empty & return the item reference
            if (item != null && item.ID == itemID)
            {
                itemSlots[i].Amount--;
                return item;
            }
        }
        return null;
    }

    public virtual int ItemCount(string itemID)
    {
        int number = 0;

        for (int i = 0; i < itemSlots.Length; i++)
        {
            Item item = itemSlots[i].Item;
            if (item != null && item.ID == itemID)
            {
                number++;
            }
        }
        return number;
    }

    public virtual bool IsFull()
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
