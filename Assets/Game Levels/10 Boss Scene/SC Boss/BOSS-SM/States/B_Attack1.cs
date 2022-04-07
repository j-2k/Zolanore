using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Attack1 : Boss_State
{
    //choose from 2 attacks (dodge pattern has to be diff)
    [SerializeField] int cycleInitialization;
    [SerializeField] int attackType;
    Vector3 lookAtPlayer;

    [SerializeField] ParticleSystem meteorVFX;
    [SerializeField] ParticleSystem blastCharge;
    [SerializeField] ParticleSystem blastVFX;
    [SerializeField] EGA_EffectSound blastSFX;
    [SerializeField] OneshotDMGPFX oneshotDMG;
    [SerializeField] Transform playerGroundPosition;

    [SerializeField] float timer = 0;
    [SerializeField] int cycles = 0;

    public override void BossOnCollisionEnter(Boss_StateMachine bsm, Collider collider)
    {
        throw new System.NotImplementedException();
    }

    public override void StartState(Boss_StateMachine bsm)
    {
        oneBlast = false;
        Debug.Log("started Attack1");
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
        Debug.Log("Phase Attack 1 > The Attack Type is 1or2 =" + attackType);
        timer += Time.deltaTime * 1;

        if (attackType == 1)
        {
            //blast attack
            BlastAttack(bsm);
        }
        else
        {
            //meteor fall dodge pattern (easy - stay on ground)
            AttackCycle(bsm);
        }

        lookAtPlayer = bsm.playerDirection.normalized;
        lookAtPlayer.y = 0;
        bsm.transform.rotation = Quaternion.RotateTowards(bsm.transform.rotation, Quaternion.LookRotation(lookAtPlayer), 120 * Time.deltaTime);
    }

    bool oneBlast;

    void BlastAttack(Boss_StateMachine bsm)
    {
        if (Vector3.Distance(bsm.transform.position, bsm.player.transform.position) <= 5 && !oneBlast)
        {
            oneBlast = true;
            blastCharge.Play();
            Invoke(nameof(DelayBlast), 1.25f);
        }
        
        if (timer >= 2f)
        {
            oneBlast = false;
            timer = 0;
            cycles++;
            Instantiate(meteorVFX, playerGroundPosition.position + (Vector3.up * 0.1f), Quaternion.identity);
            if (cycles > (cycleInitialization - 1)/2)
            {
                cycles = 0;
                bsm.BossSwitchState(bsm.chaseState);
            }
        }
    }

    void DelayBlast()
    {
        blastVFX.Play();
        blastSFX.PlaySoundOnce();
        Invoke(nameof(DelayOneShotDamage), 1);
    }

    void DelayOneShotDamage()
    {
        oneshotDMG.ApexOfVFX();
    }

    void AttackCycle(Boss_StateMachine bsm)//should be using object pool in here...
    {
        if (timer >= 1f)
        {
            timer = 0;
            cycles++;
            Instantiate(meteorVFX, playerGroundPosition.position + (Vector3.up * 0.1f), Quaternion.identity);
            Instantiate(meteorVFX, (playerGroundPosition.position + (Vector3.up * 0.1f)) + (playerGroundPosition.forward * Random.Range(7, 15)), Quaternion.identity);
            if (cycles > cycleInitialization - 1)
            {
                cycles = 0;
                bsm.BossSwitchState(bsm.chaseState);
            }
        }
    }    
}
