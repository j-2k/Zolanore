using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBank : ItemContainer
{

    [SerializeField] Transform itemsParent;
    [SerializeField] KeyCode openBankKey = KeyCode.E;
    [SerializeField] bool isInRange = false;

    bool isOpen;


    protected override void OnValidate()
    {
        if (itemsParent != null)
        {
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>(includeInactive: true); //add objects that are even disabled
        }
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        itemsParent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyDown(openBankKey))
        {
            isOpen = !isOpen;
            itemsParent.gameObject.SetActive(isOpen);
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
            if (!isInRange && isOpen)
            {
                isOpen = false;
                itemsParent.gameObject.SetActive(false);
            }
        }
    }
}
