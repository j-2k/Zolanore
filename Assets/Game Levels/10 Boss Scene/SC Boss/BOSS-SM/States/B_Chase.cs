using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Chase : Boss_State
{
    public override void BossOnCollisionEnter(Boss_StateMachine bsm, Collider collider)
    {
        throw new System.NotImplementedException();
    }

    public override void StartState(Boss_StateMachine bsm)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(Boss_StateMachine bsm)
    {
        Debug.Log(" chasing state");
    }
}
