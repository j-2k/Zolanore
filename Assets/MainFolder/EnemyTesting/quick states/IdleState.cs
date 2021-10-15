using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    [SerializeField] AwokenState awokenState;
    BossTesting bTest;


    private void Start()
    {
        bTest = GetComponentInParent<BossTesting>();
    }

    public override State runCurrentState()
    {
        if (bTest.curHealth < bTest.maxHealth)
        {
            return awokenState;
        }
        else
        {
            Debug.Log("BOSS: Idle state...");
            return this;
        }
    }
}
