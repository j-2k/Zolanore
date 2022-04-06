using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Chase : Boss_State
{
    //PHASE 1 (AWOKEN / CHASE / ATTACK1 ONLY)
    [SerializeField] float randChaseTimer;
    Vector3 lookAtPlayer;
    [SerializeField] InvulMatScript matScript;

    public override void BossOnCollisionEnter(Boss_StateMachine bsm, Collider collider)
    {
        throw new System.NotImplementedException();
    }

    public override void StartState(Boss_StateMachine bsm)
    {
        matScript.SetMaterialValue(0f);
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
        if (bsm.bossPhase == 1)
        {//phase1
            if (Random.Range(1, 11) < 2)
            {
                matScript.SetMaterialValue(1f);
            }
            EndChase(bsm,bsm.attack1State);
        }
        else
        {//phase2
            if (Random.Range(1, 6) < 2)
            {
                matScript.SetMaterialValue(1f);
            }
            EndChase(bsm,bsm.attack2State);
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
    }

    void EndChase(Boss_StateMachine bsm, Boss_State attackState)
    {
        randChaseTimer -= Time.deltaTime * 1;
        if (randChaseTimer <= 0)
        {
            bsm.agent.isStopped = true;
            bsm.BossSwitchState(attackState);
        }
    }



}
