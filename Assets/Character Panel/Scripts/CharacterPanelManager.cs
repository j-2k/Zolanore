using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanelManager : MonoBehaviour
{


    //THISIS THE WHOLE CHARACTER PANEL MANAGER
    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;

    private void Awake()
    {
        inventory.OnItemRightClickEvent += EquipFromInventory;
        equipmentPanel.OnItemRightClickedEvent += UnequipFromEquipPanel;
    }

    //noway of checking if this item is equippable so we do the check here
    private void EquipFromInventory(Item item)
    {//if the item is an type of equippable item we equip it
        if (item is EquippableItem)
        {
            Equip((EquippableItem)item);
        }
    }


    private void UnequipFromEquipPanel(Item item)
    {//if the item is an type of equippable item we equip it
        if (item is EquippableItem)
        {
            Unequip((EquippableItem)item);
        }
    }

    public void Equip(EquippableItem equippableItem)
    {
        //first remove item from inventory
        if (inventory.RemoveItem(equippableItem))
        {
            EquippableItem previousItem;
            if (equipmentPanel.Additem(equippableItem, out previousItem))
            {
                //adding the new item to the equipment panel

                //first check if there was a item in that equipment panel 
                //if there is put that previous item back in the inventory
                if (previousItem != null)
                {
                    inventory.AddItem(previousItem);
                }
            }
            else
            {
                //if we cant equip fsr return to inventory
                inventory.AddItem(equippableItem);
            }
        }
    }

    public void Unequip(EquippableItem equippableItem)
    {
        //check if the inven is full first
        if (!inventory.isInventoryFull() && equipmentPanel.RemoveItem(equippableItem))
        {
            inventory.AddItem(equippableItem);
        }
    }

}
