using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Attack1 : Boss_State
{
    public override void BossOnCollisionEnter(Boss_StateMachine bsm, Collider collider)
    {
        throw new System.NotImplementedException();
    }

    public override void StartState(Boss_StateMachine bsm)
    {
        Debug.Log("started Attack1");
    }

    public override void UpdateState(Boss_StateMachine bsm)
    {
        Debug.Log("state Attack1");
    }
}
