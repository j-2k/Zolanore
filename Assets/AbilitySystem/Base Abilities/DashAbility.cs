using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/DashAbility")]
public class DashAbility : Ability
{
    PlayerMain player;
    CharacterController cc;
    public float dashSpeed;
    Transform meshTransform;

    public override void CacheStart(GameObject parent)
    {
        player = parent.GetComponent<PlayerMain>();
        cc = parent.GetComponent<CharacterController>();
        meshTransform = parent.transform.GetChild(0);
    }

    public override void OnActivate(GameObject parent)
    {
        player.isUsingAbility = true;
        player.DashID();
    }

    bool oneRun = true;
    public override void AbilityUpdateActive(GameObject parent)
    {

        if (oneRun)
        {
            meshTransform.transform.localRotation = Quaternion.identity;
            float smooth = 0.1f;
            float targetRot = player.cameraRig.eulerAngles.y;
            player.gameObject.transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(player.gameObject.transform.eulerAngles.y, targetRot, ref smooth, 0);
            oneRun = false;
        }

        cc.Move(((player.gameObject.transform.forward) * dashSpeed) * Time.deltaTime); //(player.gameObject.transform.forward + Vector3.down )
        player.DashAttack();
    }

    public override void OnBeginCoolDown(GameObject parent)
    {
        oneRun = true;
        player.isUsingAbility = false;
        meshTransform.transform.localRotation = Quaternion.identity;
    }

    public override void AbilityUpdateCooldown(GameObject parent)
    {

    }

    /*
    void PeakofAttack()
    {
        Debug.Log("Peak of Attack");
        //MIGHT USE ANOTHER TYPE OF COLLISION LOGIC HERE THIS IS PLACE HOLDER

        hitColliders = Physics.OverlapSphere(sphereColl.transform.position, attackColliderRadius);

        //out going dmg calc maybe change in the future for better results to scale to higher lvls
        int levelBasedDmg = (int)((levelSystem.currentLevel * 2) * Random.Range(0.7f, 1.1f));
        outgoingDamage = (int)(characterManager.Strength.Value * Random.Range(1.5f, 2.5f) + levelBasedDmg);


        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Enemy")
            {
                Debug.Log("I just hit an enemey");
                //hitCollider.GetComponent<SimpleEnemy>().TakeDamageFromPlayer(outgoingDamage);
                hitCollider.GetComponent<EnemyProtoVersion>().TakeDamageFromPlayer(outgoingDamage);
            }
        }
    }
    */
}
