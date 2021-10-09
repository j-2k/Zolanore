using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName = "Test Item";

    public Sprite icon = null;

    public bool isDefaultItem = false;

    public int itemAmount = 1;

    [Range(1,4)]
    public int itemMaxStack = 4;

    public virtual void Use()
    {
        Debug.Log("Using item " + itemName);
    }
}
