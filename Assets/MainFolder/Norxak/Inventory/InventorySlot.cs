using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{

    [SerializeField] Image icon;
    [SerializeField] Button removeButton;

    Item item;

    public TextMeshProUGUI tmpItemAmount;

    public int maxStack;
    public int curAmount;

    private void Start()
    {
        tmpItemAmount.enabled = false;
        maxStack = 4;
        curAmount = 1; //curAmount = 0;
        tmpItemAmount.text = curAmount.ToString();
    }

    public void AddItem(Item newItem)
    {//check amount of itme shere

        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
        //curAmount++;
        //tmpItemAmount.enabled = true;
    }

    public void ClearSlot()
    {

        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;

        tmpItemAmount.enabled = false;
    }

    public void OnRemoveButton()
    {
        InventorySystem.instance.Remove(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }

    private void OnValidate()
    {
        //tmpItemAmount.text = curAmount.ToString();
    }
}
