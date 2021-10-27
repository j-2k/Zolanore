using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/BuffAbility")]
public class BuffAbility : Ability
{
    public float scaleIncrease;
    Transform meshTransform;

    public override void OnActivate(GameObject parent)
    {
        Debug.Log("Actiavetability");
        meshTransform = parent.transform.GetChild(0);
        scaleIncrease = 1;
        //playerTransform.localScale = new Vector3(scaleIncrease, scaleIncrease, scaleIncrease);
    }

    public override void AbilityUpdateActive(GameObject parent)
    {
        scaleIncrease += 1 * Time.deltaTime;
        scaleIncrease = Mathf.Clamp(scaleIncrease, 1, 1.5f);
        meshTransform.localScale = new Vector3(scaleIncrease, scaleIncrease, scaleIncrease);

    }

    public override void OnBeginCoolDown(GameObject parent)
    {
        //scaleIncrease = 1;
        //meshTransform.localScale = Vector3.one;
    }

    public override void AbilityUpdateCooldown(GameObject parent)
    {
        scaleIncrease -= 1 * Time.deltaTime;
        scaleIncrease = Mathf.Clamp(scaleIncrease, 1, 1.5f);
        meshTransform.localScale = new Vector3(scaleIncrease, scaleIncrease, scaleIncrease);
    }

}
