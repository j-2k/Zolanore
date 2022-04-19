using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Death : Boss_State
{
    [SerializeField] ParticleSystem[] vfxs;
    [SerializeField] ParticleSystem thunderChaseVFX;
    [SerializeField] GameObject portalObj;

    public override void BossOnCollisionEnter(Boss_StateMachine bsm, Collider collider)
    {
        throw new System.NotImplementedException();
    }

    public override void StartState(Boss_StateMachine bsm)
    {
        portalObj.SetActive(true);
        BGM.instance.isBossFight = false;
        BGM.instance.BeatBoss();
        thunderChaseVFX.Stop();
        Destroy(thunderChaseVFX.transform.parent.gameObject,4);
        bsm.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        foreach (ParticleSystem vfx in vfxs)
        {
            vfx.Stop();
        }
        Destroy(bsm.gameObject, 20);
        //enable loot somewhere & destroy body after 20
    }

    
    float lower = 0;
    public override void UpdateState(Boss_StateMachine bsm)
    {
        bsm.anim.SetBool("Death", true);



        //do loot stuff in case there is or other operations for proceeding
    }
}
