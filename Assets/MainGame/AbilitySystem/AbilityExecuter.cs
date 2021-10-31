using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityExecuter : MonoBehaviour
{
    [SerializeField] Ability ability;
    GameObject player;

    float cooldownTime;
    public float cooldownTimeMax;
    float activeTime;
    float gcd = 3;

    public CombatType abilityType;

    public enum AbilityState
    {
        ready,
        active,
        cooldown,
        gcd
    }

    public AbilityState abilityState = AbilityState.ready;

    [SerializeField] KeyCode abilityKey;

    bool triggerGCD = false;
    bool cancelAbility = false;
    bool triggerCancel = false;

    // Start is called before the first frame update
    void Start()
    {
        
        AbilityManager.OnGCD += GCDCooldown;
        AbilityManager.OnCancelAbility += CancelCurrentAbility;
        player = GameObject.FindGameObjectWithTag("Player");
        cooldownTime = ability.cooldownTime;
        cooldownTimeMax = cooldownTime;

        ability.CacheStart(player);

        if (ability.combatType == CombatType.Melee)
        {
            abilityType = CombatType.Melee;
            return;
        }
        else if (ability.combatType == CombatType.Ranged)
        {
            abilityType = CombatType.Ranged;
            return;
        }
        else if (ability.combatType == CombatType.Magic)
        {
            abilityType = CombatType.Magic;
            return;
        }
        else if (ability.combatType == CombatType.Familiar)
        {
            abilityType = CombatType.Familiar;
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (abilityState)
        {
            case AbilityState.ready:
                if (Input.GetKeyDown(abilityKey))
                {
                    triggerGCD = true;
                    AbilityManager.instance.StartGCD();
                    triggerCancel = true;
                    AbilityManager.instance.CancelAbilites();
                    ability.OnActivate(player);
                    abilityState = AbilityState.active;
                    activeTime = ability.activeTime;
                }
                break;
            case AbilityState.active:
                ability.AbilityUpdateActive(player);
                if (activeTime > 0)
                {
                    triggerCancel = false;
                    //if bypass is true we dont cancel
                    //if bypass is false  && cancel is true cancel ability
                    if (!ability.bypassCancel && cancelAbility || ability.singleTrigger)                     //1st aoe =/ trigger cancel= false / cancel ability = false | TRUE > DELEGATE X > FALSE > FALSE / RUNNING
                    {                                                               //2nd dash =/ trigger cancel = true > DELEGATE FALSE >  / cancel ability = TRUE | TRUE > DELEGATE FALSE > FALSE > FALSE
                        ability.OnBeginCoolDown(player);
                        abilityState = AbilityState.cooldown;
                        cooldownTime = ability.cooldownTime;
                    }

                    activeTime -= Time.deltaTime;
                }
                else
                {
                    ability.OnBeginCoolDown(player);
                    abilityState = AbilityState.cooldown;
                    cooldownTime = ability.cooldownTime;
                }
                break;
            case AbilityState.cooldown:
                ability.AbilityUpdateCooldown(player);
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    abilityState = AbilityState.ready;
                    cooldownTime = 0;
                    triggerGCD = false;
                    cancelAbility = false;
                }
                break;
            case AbilityState.gcd:
                ability.AbilityUpdateCooldown(player);
                if (gcd > 0)
                {
                    gcd -= Time.deltaTime;
                }
                else
                {
                    abilityState = AbilityState.ready;
                    gcd = 3;
                    triggerGCD = false;
                    cancelAbility = false;
                }
                break;
            default:
                break;
        }
    }

    public void GCDCooldown()
    {
        if (!triggerGCD)
        {
            abilityState = AbilityState.gcd;
            gcd = 3;
        }
        if (cooldownTime <= gcd)
        {
            cooldownTime = 0;
            abilityState = AbilityState.gcd;
        }
    }

    public void CancelCurrentAbility()
    {
        if (!triggerCancel)
        {
            cancelAbility = true;
        }
    }
}
