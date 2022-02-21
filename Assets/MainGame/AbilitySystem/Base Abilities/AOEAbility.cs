using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[CreateAssetMenu(menuName = "Abilities/AOEAbility")]
public class AOEAbility : Ability
{
    PlayerManager player;
    Transform meshTransform;
    CharacterController cc;
    float timer = 0;
    Animator anim;

    public override void CacheStart(GameObject parent,GameObject gameManagerObj)
    {
        combatType = CombatType.Melee;
        player = parent.GetComponent<PlayerManager>();
        meshTransform = parent.transform.GetChild(0);
        Transform last =  meshTransform.GetChild(meshTransform.childCount-1);
        anim = parent.GetComponent<Animator>();
        cc = parent.GetComponent<CharacterController>();
    }

    public override void OnActivate(GameObject parent)
    {
        timer = 0;
        player.isMovingAbility = true;
    }

    public override void AbilityUpdateActive(GameObject parent)
    {
        player.isMovingAbility = true;
        meshTransform.transform.Rotate(0, 1000 * Time.deltaTime, 0);
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            player.AOEAttack();
            timer = 0;
        }
        player.GroundedUpdate();
        Debug.Log("<color=red>SPINNING</color>");
    }

    public override void OnBeginCoolDown(GameObject parent)
    {
        player.isMovingAbility = false;
        meshTransform.transform.localRotation = Quaternion.identity;
    }

    public override void AbilityUpdateCooldown(GameObject parent)
    {
        meshTransform.transform.localRotation = Quaternion.identity;
    }



}
