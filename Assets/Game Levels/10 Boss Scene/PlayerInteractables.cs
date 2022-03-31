using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractables : MonoBehaviour
{
    public bool isFocused = false;
    [SerializeField] KeyCode interact;
    [SerializeField] GameObject bossObject;

    // Update is called once per frame
    void Update()
    {
        if (isFocused && Input.GetKeyDown(interact))
        {
            BossAlterCheck();
        }
    }

    void BossAlterCheck()
    {
        if (this.gameObject.name != "BOSSALTAR")
        {
            return;
        }
        else
        {
            bossObject.SetActive(true);
        }
    }
}
