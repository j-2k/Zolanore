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
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
