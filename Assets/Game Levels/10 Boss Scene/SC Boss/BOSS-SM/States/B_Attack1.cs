using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Attack1 : Boss_State
{
    //choose from 2 attacks (dodge pattern has to be diff)
    [SerializeField] int attackType;
    Vector3 lookAtPlayer;
    public override void BossOnCollisionEnter(Boss_StateMachine bsm, Collider collider)
    {
        throw new System.NotImplementedException();
    }

    public override void StartState(Boss_StateMachine bsm)
    {
        Debug.Log("started Attack1");
        attackType = Random.Range(1, 3);
        bsm.agent.isStopped = true;
    }

    public override void UpdateState(Boss_StateMachine bsm)
    {
        Debug.Log("state Attack1 > Attack Type =" + attackType);
        if (attackType == 1)
        {

        }
        else
        {

        }

        lookAtPlayer = bsm.playerDirection.normalized;
        lookAtPlayer.y = 0;
        bsm.transform.rotation = Quaternion.RotateTowards(bsm.transform.rotation, Quaternion.LookRotation(lookAtPlayer), 120 * Time.deltaTime);
    }
}
