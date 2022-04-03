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
            AttackCycleMeteor(bsm);
        }
        else
        {
            //meteor fall dodge pattern (easy - stay on ground)
            AttackCycleMeteor(bsm);
        }

        lookAtPlayer = bsm.playerDirection.normalized;
        lookAtPlayer.y = 0;
        bsm.transform.rotation = Quaternion.RotateTowards(bsm.transform.rotation, Quaternion.LookRotation(lookAtPlayer), 120 * Time.deltaTime);
    }

    void AttackCycleMeteor(Boss_StateMachine bsm)//should be using object pool in here...
    {
        if (timer >= 1f)
        {
            timer = 0;
            cycles++;
            Instantiate(meteorVFX, playerGroundPosition.position + (Vector3.up * 0.1f), Quaternion.identity);
            Invoke(nameof(MeteorTimed), 0.33f);
            Invoke(nameof(MeteorTimed), 0.66f);
            if (cycles > cycleInitialization - 1)
            {
                cycles = 0;
                bsm.BossSwitchState(bsm.chaseState);
            }
        }
    }
    Vector3 aroundPlayerVec;
    void MeteorTimed()
    {
        aroundPlayerVec =
            (playerGroundPosition.position + (Vector3.up * 0.1f))
            + (playerGroundPosition.forward * IntWithNegativeRNG(4,8))
            + (playerGroundPosition.right * IntWithNegativeRNG(4, 8));
        Instantiate(meteorVFX, aroundPlayerVec, Quaternion.identity);
    }

    int IntWithNegativeRNG(int start, int end)
    {
        if (Random.Range(1, 3) > 1)
        {
            return Random.Range(start, end + 1);
        }
        else
        {
            return Random.Range(-end, -start - 1);
        }
    }
}
