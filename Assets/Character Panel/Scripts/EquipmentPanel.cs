using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    [SerializeField] Transform equipmentSlotsParent;
    [SerializeField] EquipmentSlot[] equipmentSlots;


    public event Action<Item> OnItemRightClickedEvent;

    private void Start()
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            equipmentSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
        }
    }

    private void OnValidate()
    {
        equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    public bool Additem (EquippableItem equippableItem, out EquippableItem previousItem)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].EquipmentType == equippableItem.EquipmentType)
            {
                previousItem = (EquippableItem)equipmentSlots[i].Item;
                equipmentSlots[i].Item = equippableItem;
                return true;
            }
        }
        previousItem = null;
        return false;
    }

    public bool RemoveItem(EquippableItem equippableItem)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].Item == equippableItem)
            {
                equipmentSlots[i].Item = null;
                return true;
            }
        }

        return false;
    }

}
