using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/AOEAbility")]
public class AOEAbility : Ability
{
    PlayerManager player;
    Transform meshTransform;
    CharacterController cc;
    float timer = 0;

    public override void CacheStart(GameObject parent)
    {
        player = parent.GetComponent<PlayerManager>();
        meshTransform = parent.transform.GetChild(0);
        cc = parent.GetComponent<CharacterController>();
    }

    public override void OnActivate(GameObject parent)
    {
        timer = 0;
        player.isMovingAbility = true;
    }

    public override void AbilityUpdateActive(GameObject parent)
    {
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
