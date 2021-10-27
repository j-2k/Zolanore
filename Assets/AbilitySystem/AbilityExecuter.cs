using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityExecuter : MonoBehaviour
{
    [SerializeField] Ability ability;
    GameObject player;
    [SerializeField] float cooldownTime;
    float activeTime;
    float gcd = 3;

    enum AbilityState
    {
        ready,
        active,
        cooldown,
        gcd
    }

    AbilityState abilityState = AbilityState.ready;

    [SerializeField] KeyCode abilityKey;

    bool gcdTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        AbilityManager.onGCD += GCDCooldown;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch (abilityState)
        {
            case AbilityState.ready:
                if (Input.GetKeyDown(abilityKey))
                {
                    gcdTrigger = true;
                    AbilityManager.instance.StartGCD();
                    ability.OnActivate(player);
                    abilityState = AbilityState.active;
                    activeTime = ability.activeTime;
                }
                break;
            case AbilityState.active:
                ability.AbilityUpdateActive(player);
                if (activeTime > 0)
                {
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
                    cooldownTime = 0;
                    gcdTrigger = false;
                    abilityState = AbilityState.ready;
                }
                break;
            default:
                break;
        }
    }

    public void GCDCooldown()
    {
        if (!gcdTrigger)
        {
            Debug.Log("trigger gcd func");
            abilityState = AbilityState.cooldown;
            cooldownTime = gcd;
        }

        if (cooldownTime <= gcd)
        {
            cooldownTime = gcd;
        }
    }
}
