using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Chase : Boss_State
{
    //PHASE 1 (AWOKEN / CHASE / ATTACK1 ONLY)
    [SerializeField] int chasePhase = 1;
    [SerializeField] float randChaseTimer;
    Vector3 lookAtPlayer;

    public override void BossOnCollisionEnter(Boss_StateMachine bsm, Collider collider)
    {
        throw new System.NotImplementedException();
    }

    public override void StartState(Boss_StateMachine bsm)
    {
        Debug.Log("start chase");
        randChaseTimer = Random.Range(3, 5);
        //randChaseTimer = 100;
        /*
        if (boss health is less than 50%)
            chasePhase = 2;
            else
            chasePhase = 1;
         */
    }

    public override void UpdateState(Boss_StateMachine bsm)
    {
        Debug.Log(" chasing state");
        if (chasePhase == 1)
        {
            randChaseTimer -= Time.deltaTime * 1;
            if (randChaseTimer <= 0)
            {
                bsm.agent.isStopped = true;
                bsm.BossSwitchState(bsm.attack1State);
                //stop do attack ... then event into timer set to higher number.
                Debug.Log("going into attack1 state");
            }

        }
        else
        {

        }

        if (Vector3.Distance(bsm.transform.position, bsm.player.transform.position) <= bsm.agent.stoppingDistance)
        {
            bsm.agent.isStopped = true;
            lookAtPlayer = bsm.playerDirection.normalized;
            lookAtPlayer.y = 0;

            bsm.transform.rotation = Quaternion.RotateTowards(bsm.transform.rotation, Quaternion.LookRotation(lookAtPlayer), 120 * Time.deltaTime);
        }
        else
        {
            bsm.agent.isStopped = false;
            bsm.agent.SetDestination(bsm.player.transform.position);
        }

        /*
        //check health here and change to chasephase 2
        if (bsm.agent.isStopped = false && Time.time >= startOfChaseTime + 30)
        {
            bsm.BossSwitchState(bsm.attack1State);
            //chasePhase = 2;
        }
        */

    }
}
