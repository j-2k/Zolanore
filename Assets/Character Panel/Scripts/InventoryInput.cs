using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    [SerializeField] KeyCode toggleInventoryPanelKey;
    [SerializeField] GameObject inventoryPanelGameobject;
    [SerializeField] KeyCode toggleEquipmentPanelKey;
    [SerializeField] GameObject equipmentPanelGameObject;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(toggleInventoryPanelKey))
        {
            inventoryPanelGameobject.SetActive(!inventoryPanelGameobject.activeSelf);

            if (inventoryPanelGameobject.activeSelf || equipmentPanelGameObject.activeSelf)
            {
                ShowMouseCursor();
            }
            else
            {
                HideMouseCursor();
            }
        }


        if (Input.GetKeyDown(toggleEquipmentPanelKey))
        {
            equipmentPanelGameObject.SetActive(!equipmentPanelGameObject.activeSelf);

            if (inventoryPanelGameobject.activeSelf || equipmentPanelGameObject.activeSelf)
            {
                ShowMouseCursor();
            }
            else
            {
                HideMouseCursor();
            }
        }
    }

    public void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideMouseCursor()
    {
        Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Confined;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ToggleEquipmentPanel()
    {
        equipmentPanelGameObject.SetActive(!equipmentPanelGameObject.activeSelf);
    }

}
