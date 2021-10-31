using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FamiliarFollowState : State
{
    [SerializeField] FamiliarChaseState attackState;
    PlayerFamiliar playerFamiliar;
    NavMeshAgent familiarAgent;
    GameObject player;

    float timeToTeleport;
    float teleTimer;

    void Awake()
    {
        timeToTeleport = 6;

        //getting componenets
        playerFamiliar = GetComponentInParent<PlayerFamiliar>();
        for (int i = 0; i < playerFamiliar.states.Length; i++)
        {
            if (playerFamiliar.states[i].name == "ChaseState")
            {
                attackState = (FamiliarChaseState)playerFamiliar.states[i];
                break;
            }
        }

        player = playerFamiliar.player;
        familiarAgent = playerFamiliar.agentFamiliar;
    }

    // Update is called once per frame
    public override State runCurrentState()
    {
        if (playerFamiliar.isEnemyHit)
        {
            return attackState;
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
