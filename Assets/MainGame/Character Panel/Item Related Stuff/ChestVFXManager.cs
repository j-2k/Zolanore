using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestVFXManager : MonoBehaviour
{
    [SerializeField] GameObject pickupVFX;
    [SerializeField] GameObject sparkleVFX;
    [SerializeField] Animation openAnim;

    GameObject openChestEffectOnly;
    bool isOpened = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        
    }

    public void OpenChest()
    {
        if (!isOpened)
        {
            sparkleVFX.gameObject.SetActive(true);
            openAnim.Play();
        }
        Destroy(openChestEffectOnly);
        openChestEffectOnly = Instantiate(pickupVFX, transform.position + transform.up * 0.05f, Quaternion.identity);
        openChestEffectOnly.transform.localScale = Vector3.one * 3;
        openChestEffectOnly.transform.parent = this.transform;
        isOpened = true;
    }
}
