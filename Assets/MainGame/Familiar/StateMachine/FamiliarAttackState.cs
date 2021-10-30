using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FamiliarAttackState : State
{
    [SerializeField] FamiliarFollowState followState;
    PlayerFamiliar playerFamiliar;
    NavMeshAgent familiarAgent;
    GameObject player;

    float catchUpTimer;

    // Start is called before the first frame update
    void Start()
    {
        //getting componenets
        playerFamiliar = GetComponentInParent<PlayerFamiliar>();
        for (int i = 0; i < playerFamiliar.states.Length; i++)
        {
            if (playerFamiliar.states[i].name == "FollowState")
            {
                followState = (FamiliarFollowState)playerFamiliar.states[i];
            }
        }

        player = playerFamiliar.player;
        familiarAgent = playerFamiliar.agentFamiliar;
    }

    public override State runCurrentState()
    {
        if (playerFamiliar.focusEnemy == null || Vector3.Distance(familiarAgent.transform.position, player.transform.position) >= 12)
        {
            return followState;
        }
        else
        {
            familiarAgent.SetDestination(playerFamiliar.focusEnemy.transform.position);
            
            if (familiarAgent.remainingDistance <= 2)
            {
                AttackEnemy();
            }
            else
            {
                /*
                catchUpTimer += Time.deltaTime;
                if (catchUpTimer >= 5)
                {
                    familiarAgent.transform.position = player.transform.position;
                }
                */
            }

            return this;
        }
    }

    void AttackEnemy()
    {
        Debug.Log("Swinging at enemy");
    }
}
