using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatSheet : MonoBehaviour
{
    CharacterManager characterStats;
    public int playerHealthFinal;

    private void Start()
    {
        //characterStats = GameObject.FindGameObjectWithTag("CharacterPanel").GetComponent<CharacterPanelManager>();
    }

    private void Update()
    {
        //Debug.Log("Defence.Value" + characterStats.Defence.Value);
        //Debug.Log("Defence.BaseValue" + characterStats.Defence.BaseValue);
        //Debug.Log("Defence.Defence" + characterStats.Defence);

    }

}
