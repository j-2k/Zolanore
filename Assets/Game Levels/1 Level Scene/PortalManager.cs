using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    QuestSystem qs;
    // Start is called before the first frame update
    void Start()
    {
        qs = PlayerManager.instance.gameObject.GetComponent<QuestSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (qs.bossPortal == true)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
