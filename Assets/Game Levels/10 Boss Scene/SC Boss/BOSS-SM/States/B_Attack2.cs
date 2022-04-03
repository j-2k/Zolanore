using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Attack2 : Boss_State
{
    //choose from 2 attacks (dodge pattern has to be diff)
    [SerializeField] int cycleInitialization;
    [SerializeField] int attackType;
    Vector3 lookAtPlayer;

    [SerializeField] ParticleSystem meteorVFX;
    [SerializeField] Transform playerGroundPosition;

    [SerializeField] float timer = 0;
    [SerializeField] int cycles = 0;

    public override void BossOnCollisionEnter(Boss_StateMachine bsm, Collider collider)
    {
        throw new System.NotImplementedException();
    }

    public override void StartState(Boss_StateMachine bsm)
    {
        Debug.Log("started attack phase 2");
        attackType = Random.Range(1, 3);
        bsm.agent.isStopped = true;
        playerGroundPosition = bsm.player.transform.GetChild(bsm.player.transform.childCount - 1);
        playerGroundPosition = playerGroundPosition.GetChild(0);
        if (cycleInitialization == 0)
        {
            cycleInitialization = 5;
        }
    }

    public override void UpdateState(Boss_StateMachine bsm)
    {
        Debug.Log("Phase Attack 2 > The Attack Type is 1or2 =" + attackType);
        timer += Time.deltaTime * 1;

        if (attackType == 1)
        {
            //laser dodge pattern (easy - stay on ground)
            //AttackCycle(bsm);
        }
        else
        {
            //meteor fall dodge pattern (easy - stay on ground)
            //AttackCycle(bsm);
        }

        lookAtPlayer = bsm.playerDirection.normalized;
        lookAtPlayer.y = 0;
        bsm.transform.rotation = Quaternion.RotateTowards(bsm.transform.rotation, Quaternion.LookRotation(lookAtPlayer), 120 * Time.deltaTime);
    }

    int rand = 0;
    int randMeteorRange = 0;
    void AttackCycle(Boss_StateMachine bsm)//should be using object pool in here...
    {
        if (timer >= 1f)
        {
            timer = 0;
            cycles++;
            rand = Random.Range(1, 3);
            randMeteorRange = Random.Range(7, 15);
            Instantiate(meteorVFX, playerGroundPosition.position + (Vector3.up * 0.1f), Quaternion.identity);
            if (rand == 1)
            {
                Instantiate(meteorVFX, (playerGroundPosition.position + (Vector3.up * 0.1f)) + (playerGroundPosition.forward * randMeteorRange), Quaternion.identity);
            }
            else
            {
                Instantiate(meteorVFX, (playerGroundPosition.position + (Vector3.up * 0.1f)) + (playerGroundPosition.forward * randMeteorRange), Quaternion.identity);
            }

            if (cycles > cycleInitialization - 1)
            {
                cycles = 0;
                bsm.BossSwitchState(bsm.chaseState);
            }
        }
    }
}
