using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class BaseItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI amountText;

    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;
    public event Action<BaseItemSlot> OnRightClickEvent;

    [SerializeField] Item _item;

    private Color normalColor = Color.white;
    private Color disabledColor = Color.clear;

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
                image.color = disabledColor;
            }
            else
            {
                image.color = normalColor;
                image.sprite = _item.Icon;
            }
        }
    }

    private int _amount;
    public int Amount
    {
        get { return _amount; }
        set 
        { 
            _amount = value;

            if (_amount < 0)
            {
                _amount = 0;
            }

            if (_amount == 0)
            {
                Item = null;
            }

            if (amountText != null)
            {
                amountText.enabled = _item != null && _amount > 1;
                if (amountText.enabled)
                {
                    amountText.text = _amount.ToString();
                }
            }
        }
    }

    protected virtual void OnValidate()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }

        if (amountText == null)
        {
            amountText = GetComponentInChildren<TextMeshProUGUI>();
        }

    }

    public virtual bool CanAddStack (Item item, int amount = 1)
    {
        return Item != null && Item.ID == item.ID;
    }


    public virtual bool CanReceiveItem(Item item)
    {
        return false;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //check if the item slot was clicked to equip a item
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (OnRightClickEvent != null)
            {
                OnRightClickEvent(this);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnPointerEnterEvent != null)
        {
            OnPointerEnterEvent(this);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnPointerExitEvent != null)
        {
            OnPointerExitEvent(this);
        }
    }
}
