using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Juma.CharacterStats;

public class CharacterManager : MonoBehaviour
{
    public int playerHealth = 100;

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
    [SerializeField] ItemTooltip itemTooltip;
    [SerializeField] Image draggableItem;

    private BaseItemSlot dragItemSlot;

    private void OnValidate()
    {
        if (itemTooltip == null)
        {
            itemTooltip = FindObjectOfType<ItemTooltip>();
        }
    }

    private void Awake()
    {

        statPanel.SetStats(Strength, Dexterity, Intelligence, Defence);
        statPanel.UpdateStatValue();

        //setup events;
        inventory.OnRightClickEvent += InventoryRightClick;
        equipmentPanel.OnRightClickEvent += EquipmentRightClick;

        inventory.OnPointerEnterEvent += ShowTooltip;
        equipmentPanel.OnPointerEnterEvent += ShowTooltip;

        inventory.OnPointerExitEvent += HideTooltip;
        equipmentPanel.OnPointerExitEvent += HideTooltip;

        inventory.OnBeginDragEvent += BeginDrag;
        equipmentPanel.OnBeginDragEvent += BeginDrag;

        inventory.OnEndDragEvent += EndDrag;
        equipmentPanel.OnEndDragEvent += EndDrag;

        inventory.OnDragEvent += Drag;
        equipmentPanel.OnDragEvent += Drag;

        inventory.OnDropEvent += Drop;
        equipmentPanel.OnDropEvent += Drop;
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


    private void InventoryRightClick(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item is EquippableItem)
        {
            Equip((EquippableItem)itemSlot.Item);
        }
        else if (itemSlot.Item is UsableItem)
        {
            UsableItem usableItem = (UsableItem)itemSlot.Item;
            usableItem.Use(this);

            if (usableItem.IsConsumable)
            {
                inventory.RemoveItem(usableItem);
                usableItem.Destroy();
            }
        }
    }

    private void EquipmentRightClick(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item is EquippableItem)
        {
            Unequip((EquippableItem)itemSlot.Item);
        }
    }


    private void ShowTooltip(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            itemTooltip.ShowTooltip(itemSlot.Item);
        }
    }

    private void HideTooltip(BaseItemSlot itemSlot)
    {
        itemTooltip.HideTooltip();
    }

    private void BeginDrag(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            dragItemSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.Icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }

    private void EndDrag(BaseItemSlot itemSlot)
    {
        dragItemSlot = null;
        draggableItem.enabled = false;
    }

    private void Drag(BaseItemSlot itemSlot)
    {
        if (draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
    }

    private void Drop(BaseItemSlot dropItemSlot)
    {
        if (dragItemSlot == null) return;

        if (dropItemSlot.CanAddStack(dragItemSlot.Item))
        {
            AddStacks(dropItemSlot);
        }
        else if (dropItemSlot.CanReceiveItem(dragItemSlot.Item) && dragItemSlot.CanReceiveItem(dropItemSlot.Item))
        {
            SwapItems(dropItemSlot);
        }
    }
    
    private void SwapItems(BaseItemSlot dropItemSlot)
    {
		EquippableItem dragEquipItem = dragItemSlot.Item as EquippableItem;
		EquippableItem dropEquipItem = dropItemSlot.Item as EquippableItem;

		if (dropItemSlot is EquipmentSlot)
		{
			if (dragEquipItem != null) dragEquipItem.Equip(this);
			if (dropEquipItem != null) dropEquipItem.Unequip(this);
		}
		if (dragItemSlot is EquipmentSlot)
		{
			if (dragEquipItem != null) dragEquipItem.Unequip(this);
			if (dropEquipItem != null) dropEquipItem.Equip(this);
		}
		statPanel.UpdateStatValue();

		Item draggedItem = dragItemSlot.Item;
		int draggedItemAmount = dragItemSlot.Amount;

		dragItemSlot.Item = dropItemSlot.Item;
		dragItemSlot.Amount = dropItemSlot.Amount;

		dropItemSlot.Item = draggedItem;
		dropItemSlot.Amount = draggedItemAmount;
    }
    
    /*
    private void SwapItems(BaseItemSlot dropItemSlot)
    {
        EquippableItem dragItem = dragItemSlot.Item as EquippableItem;
        EquippableItem dropItem = dropItemSlot.Item as EquippableItem;
        if (dragItemSlot is EquipmentSlot)
        {
            if (dragItem != null) Unequip(dragItem);
            if (dropItem != null) Equip(dropItem);

        }
        if (dropItemSlot is EquipmentSlot)
        {
            if (dragItem != null) Equip(dragItem);
            if (dropItem != null) Unequip(dropItem);
        }
        statPanel.UpdateStatValue();
        if (!(dragItemSlot is EquipmentSlot) && !(dropItemSlot is EquipmentSlot))
        {
            Item draggedItem = dragItemSlot.Item;
            int draggedItemAmount = dragItemSlot.Amount;

            dragItemSlot.Item = dropItemSlot.Item;
            dragItemSlot.Amount = dropItemSlot.Amount;

            dropItemSlot.Item = draggedItem;
            dropItemSlot.Amount = draggedItemAmount;
        }
    }*/

    private void AddStacks(BaseItemSlot dropItemSlot)
    {
        //Add stacks untill drgpitemslot is full
        //remove the same number of stacks from drag itemslot
        int numAddableStacks = dropItemSlot.Item.MaxStack - dropItemSlot.Amount;
        int stacksToAdd = Mathf.Min(numAddableStacks, dragItemSlot.Amount);

        dropItemSlot.Amount += stacksToAdd;
        dragItemSlot.Amount -= stacksToAdd;
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
        if (!inventory.IsFull() && equipmentPanel.RemoveItem(equippableItem))
        {
            equippableItem.Unequip(this);
            statPanel.UpdateStatValue();
            inventory.AddItem(equippableItem);
        }
    }

}
