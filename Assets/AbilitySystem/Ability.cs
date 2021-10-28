using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public new string name;
    public float cooldownTime;
    public float activeTime;
    public bool singleTrigger;

    public virtual void CacheStart(GameObject parent)
    {

    }

    public virtual void OnActivate(GameObject parent)
    {

    }

    public virtual void OnBeginCoolDown(GameObject parent)
    {

    }

    public virtual void AbilityUpdateActive(GameObject parent)
    {

    }

    public virtual void AbilityUpdateCooldown(GameObject parent)
    {

    }
}
