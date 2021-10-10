using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Transform itemsParent;

    GameObject inventoryUI;

    InventorySlot[] slots;

    InventorySystem inventory;


    // Start is called before the first frame update
    void Start()
    {
        inventory = InventorySystem.instance;
        inventory.onItemChangedCallback += UpdateUI;
        //V refrenceing inventoryUI by getting hte first child from this object heirarchy order is important care
        inventoryUI = gameObject.transform.GetChild(0).gameObject;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        //slots = GetComponentsInChildren<InventorySlot>(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (inventoryUI.activeSelf)
            {
                inventoryUI.SetActive(false);
            }
            else
            {
                inventoryUI.SetActive(true);
            }
        }
    }

    void UpdateUI()
    {
        Debug.Log("Updating new UI Changes");
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                Debug.Log("in ui update");
                /*
                if (slots[i].curAmount >= 2)
                {
                    Debug.Log("ui enable test & tostring cur amount");
                    slots[i].tmpItemAmount.enabled = true;
                    slots[i].tmpItemAmount.text = slots[i].curAmount.ToString("n0");
                }
                else if (slots[i].curAmount == 1)
                {
                    Debug.Log("ui disable text ");
                    slots[i].tmpItemAmount.enabled = false;
                }
                else if(slots[i].curAmount == 0)
                {
                    Debug.Log("slot add item");
                    slots[i].AddItem(inventory.items[i]);
                }*/

                slots[i].AddItem(inventory.items[i]);
                //slots[i].tmpItemAmount.enabled = true;
                //slots[i].tmpItemAmount.text = inventory.items[i].itemAmount.ToString("n0");
            }
            else
            {
                Debug.Log("clear slot");
                //check stack here?
                slots[i].ClearSlot();
            }

        }
    }
}
