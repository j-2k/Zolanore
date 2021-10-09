using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFocus : MonoBehaviour
{
    //THIS SCRIPT GOES ON CAMERA TILT X OR CHANGE IT TO OLD VERSION OF IT BEING WITH MAIN CAM CONTROLLER

    //item raycast check
    Ray itemRay;
    RaycastHit itemHit;
    [SerializeField] LayerMask itemLayer;
    [SerializeField] ItemPickup itemBeingChecked;

    private void LateUpdate()
    {
        ItemInteractable();
    }

    void ItemInteractable()
    {
        itemRay.origin = transform.position;
        itemRay.direction = transform.forward;
        if (Physics.Raycast(itemRay, out itemHit, 3, itemLayer))
        {//VVV COLLIDING

            itemBeingChecked = itemHit.transform.GetComponent<ItemPickup>();
            itemBeingChecked.isFocused = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                itemBeingChecked.hasInteracted = true;
            }
        }
        else
        {//VVV NOT COLLIDING
            if (itemHit.transform == null)
            {
                if (itemBeingChecked != null)
                {
                    itemBeingChecked.isFocused = false;
                }
                itemBeingChecked = null;
            }
        }
        Debug.DrawRay(transform.position, itemRay.direction * 3, Color.magenta);
    }
}
