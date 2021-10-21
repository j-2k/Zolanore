using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image image;
    [SerializeField] ItemTooltip tooltip;


    public event Action<Item> OnRightClickEvent;

    [SerializeField] Item _item;

    public Item Item
    {
        get
        {
            return _item;
        }
        set
        {
            _item = value;

            if (_item == null)
            {
                image.enabled = false;
            }
            else
            {
                image.enabled = true;
                image.sprite = _item.Icon;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //check if the item slot was clicked to equip a item
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (Item != null && OnRightClickEvent != null)
            {
                OnRightClickEvent(Item);
            }
        }
    }

    protected virtual void OnValidate()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }

        if (tooltip == null)
        {
            tooltip = FindObjectOfType<ItemTooltip>();
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Item is EquippableItem)
        {
            tooltip.ShowTooltip((EquippableItem)Item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }
}
