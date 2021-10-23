using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI amountText;

    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDropEvent;

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
            amountText.enabled = _item != null && _item.MaxStack > 1 && _amount > 1;
            if (amountText.enabled)
            {
                amountText.text = _amount.ToString();
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

    public virtual bool CanReceiveItem(Item item)
    {
        return true;

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

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (OnBeginDragEvent != null)
        {
            OnBeginDragEvent(this);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragEvent != null)
        {
            OnDragEvent(this);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (OnEndDragEvent!=null)
        {
            OnEndDragEvent(this);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (OnDropEvent != null)
        {
            OnDropEvent(this);
        }
    }
}
