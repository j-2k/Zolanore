using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] Ability ability;
    GameObject player;
    float cooldownTime;
    float activeTime;

    enum AbilityState
    {
        ready,
        active,
        cooldown
    }

    AbilityState abilityState = AbilityState.ready;

    [SerializeField] KeyCode abilityKey;

    // Start is called before the first frame update
    void Start()
    {
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
                    abilityState = AbilityState.ready;
                }
                break;
            default:
                break;
        }
    }
}
