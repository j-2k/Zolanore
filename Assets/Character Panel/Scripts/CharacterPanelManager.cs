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

    /*
    //trying out level skill system where when u level up u gain 2 points to spend fre ein each skill slot seems to work
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("was" + Defence.BaseValue);
            Defence.BaseValue++;
            Debug.Log("now" + Defence.BaseValue);
            statPanel.UpdateStatValue();
        }


        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //hack fix fast is to make sure player spent all free point sbefore resseting or else they will dupe find a nice fix or do this hack fix
            Debug.Log("resetting all stat values");
            statPanel.ResetAllStatValue();

            int level = 5;

            //give 2 free points per level
            int freePoints = level * 2;
            


            Debug.Log("you have " + freePoints + " points to spend after resetting ur skill levels");
        }
    }
    */

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
