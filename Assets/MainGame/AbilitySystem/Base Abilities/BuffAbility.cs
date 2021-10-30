using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/BuffAbility")]
public class BuffAbility : Ability
{
    public float scaleIncrease;
    Transform meshTransform;
    CharacterManager cm;
    PlayerManager player;

    public override void CacheStart(GameObject parent)
    {
        cm = parent.GetComponent<CharacterManager>();
        player = parent.GetComponent<PlayerManager>();
    }

    public override void OnActivate(GameObject parent)
    {
        meshTransform = parent.transform.GetChild(0);
        scaleIncrease = 1;
        cm.Strength.BaseValue += 10f;
        cm.UpdateStatSkillPoint();
        player.isMovingAbility = false;
    }

    public override void AbilityUpdateActive(GameObject parent)
    {
        if (scaleIncrease <= 1.3f)
        {
            scaleIncrease += 1 * Time.deltaTime;
            scaleIncrease = Mathf.Clamp(scaleIncrease, 1, 1.3f);
            meshTransform.localScale = new Vector3(scaleIncrease, scaleIncrease, scaleIncrease);
        }
    }

    public override void OnBeginCoolDown(GameObject parent)
    {
        cm.Strength.BaseValue -= 10f;
        cm.UpdateStatSkillPoint();
    }

    public override void AbilityUpdateCooldown(GameObject parent)
    {
        if (scaleIncrease >= 1f)
        {
            scaleIncrease -= 1 * Time.deltaTime;
            scaleIncrease = Mathf.Clamp(scaleIncrease, 1, 1.3f);
            meshTransform.localScale = new Vector3(scaleIncrease, scaleIncrease, scaleIncrease);
        }
    }

}
