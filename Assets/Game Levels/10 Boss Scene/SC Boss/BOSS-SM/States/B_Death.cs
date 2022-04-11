using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Death : Boss_State
{
    [SerializeField] ParticleSystem[] vfxs;


    public override void BossOnCollisionEnter(Boss_StateMachine bsm, Collider collider)
    {
        throw new System.NotImplementedException();
    }

    public override void StartState(Boss_StateMachine bsm)
    {
        foreach (ParticleSystem vfx in vfxs)
        {
            vfx.Stop();
        }
        Destroy(bsm.gameObject, 20);
        //enable loot somewhere & destroy body after 20
    }

    public override void UpdateState(Boss_StateMachine bsm)
    {
        bsm.anim.SetBool("Death", true);

        //do loot stuff in case there is or other operations for proceeding
    }
}
