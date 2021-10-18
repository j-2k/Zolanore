using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public bool isFocused;
    public bool hasInteracted;

    [SerializeField] Item item;

    // Update is called once per frame
    void Update()
    {
        if (hasInteracted == true)
        {
            PickUpItem();
            hasInteracted = false;
        }
    }

    void PickUpItem()
    {
        Debug.Log("trying to pick Up " + item.itemName);
        bool isPickingUp = InventorySystem.instance.Add(item);

        if (isPickingUp == true)
        {
            Debug.Log("Successfully picked up " + item.itemName);
            Destroy(gameObject);
        }
    }
}
