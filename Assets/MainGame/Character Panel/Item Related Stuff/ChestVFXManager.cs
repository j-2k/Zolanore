using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestVFXManager : MonoBehaviour
{
    [SerializeField] GameObject chestGlowVFX;
    [SerializeField] GameObject pickupVFX;
    [SerializeField] Animation openAnim;
    GameObject glowOnly;
    GameObject openChestEffectOnly;
    bool isOpened = false;
    // Start is called before the first frame update
    void Start()
    {
        glowOnly = Instantiate(chestGlowVFX, transform.position + transform.up * 0.05f, Quaternion.identity);
        glowOnly.transform.localScale = Vector3.one * 5;
        glowOnly.transform.parent = this.transform;
    }

    private void Update()
    {
        
    }

    public void OpenChest()
    {
        if (!isOpened)
        {
            openAnim.Play();
        }
        Destroy(openChestEffectOnly);
        openChestEffectOnly = Instantiate(pickupVFX, transform.position + transform.up * 0.05f, Quaternion.identity);
        openChestEffectOnly.transform.localScale = Vector3.one * 3;
        openChestEffectOnly.transform.parent = this.transform;
        isOpened = true;
    }
}
