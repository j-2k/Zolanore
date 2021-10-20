using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Juma.CharacterStats;

public class CharacterPanelManager : MonoBehaviour
{
    //ADD NEW STATS HERE
    public CharacterStat Strength;
    public CharacterStat Dexterity;
    public CharacterStat Intelligence;
    public CharacterStat Defence;
    //

    //THISIS THE WHOLE CHARACTER PANEL MANAGER
    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] StatPanel statPanel;

    private void Awake()
    {
        statPanel.SetStats(Strength, Dexterity, Intelligence, Defence);
        statPanel.UpdateStatValue();

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
                    previousItem.Unequip(this);
                    statPanel.UpdateStatValue();
                }
                equippableItem.Equip(this);
                statPanel.UpdateStatValue();
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
            equippableItem.Unequip(this);
            statPanel.UpdateStatValue();
            inventory.AddItem(equippableItem);
        }
    }

}
