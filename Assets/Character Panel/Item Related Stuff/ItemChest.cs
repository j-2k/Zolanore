using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChest : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] Inventory inventory;
    [SerializeField] KeyCode itemPickup = KeyCode.E;

    bool isInRange = false;
    [SerializeField] bool isEmpty = false;       // dont need this can just null the item but if you dont want to lose the reference do this way

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnValidate()
    {
        if (inventory == null)
        {
            inventory = FindObjectOfType<Inventory>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEmpty && !inventory.isInventoryFull() && isInRange && Input.GetKeyDown(itemPickup))
        {
            inventory.AddItem(Instantiate(item));
            isEmpty = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        InRangeTriggerCheck(other.gameObject, true);
    }

    private void OnTriggerExit(Collider other)
    {
        InRangeTriggerCheck(other.gameObject, false);
    }

    void InRangeTriggerCheck(GameObject gameObject, bool state)
    {
        if (gameObject.CompareTag("Player"))
        {
            isInRange = state;
            //text enabeled = state
        }
    }    
}
