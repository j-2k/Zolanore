using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FamiliarFollowState : State
{
    [SerializeField] FamiliarChaseState chaseState;
    [SerializeField] FamiliarAttackState attackState;
    PlayerFamiliar playerFamiliar;
    NavMeshAgent familiarAgent;
    GameObject player;

    float timeToTeleport;
    float teleTimer;

    void Start()
    {
        timeToTeleport = 6;
        playerFamiliar = GetComponentInParent<PlayerFamiliar>();
        familiarAgent = GetComponentInParent<NavMeshAgent>();
        player = playerFamiliar.player;
    }

    // Update is called once per frame
    public override State runCurrentState()
    {
        if (playerFamiliar.isEnemyHit)
        {
            if (playerFamiliar.abilityTrigger)
            {
                return attackState;
            }
            playerFamiliar.callFamiliarBack = false;
            return chaseState;
        }
        else
        {
            familiarAgent.SetDestination(player.transform.position);

            if (Vector3.Distance(familiarAgent.transform.position, player.transform.position) >= familiarAgent.stoppingDistance + 3)//5+3
            {
                teleTimer += Time.deltaTime;
                if (teleTimer >= timeToTeleport)
                {
                    familiarAgent.transform.position = player.transform.position;
                    teleTimer = 0;
                }
            }
            else
            {
                teleTimer = 0;
            }
            return this;
        }
    }
}
