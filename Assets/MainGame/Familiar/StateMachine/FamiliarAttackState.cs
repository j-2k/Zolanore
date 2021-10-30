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
    void Awake()
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
        if (playerFamiliar.focusEnemy == null)
        {
            return FollowState();
        }
        else
        {
            familiarAgent.SetDestination(playerFamiliar.focusEnemy.transform.position);

            familiarAgent.stoppingDistance = 3;

            Debug.Log("going to enemy");
            if (Vector3.Distance(familiarAgent.transform.position, playerFamiliar.focusEnemy.transform.position) <= familiarAgent.stoppingDistance) //familiarAgent.stoppingDistance this shit fucked me for 2 hours i had another number instead
            {   //IF U WANT TO CHANGE THE DISTANCE OF THE ATTACK RANGE YOU MUST CHANGE THE STOPPING DISTANCE  = familiarAgent.stoppingDistance I MESSED UP BEFORE
                Debug.Log("FAMILIAR IS ATTACKING!!!");
                return FollowState();
            }
            else if (Vector3.Distance(familiarAgent.transform.position, player.transform.position) >= 15)
            {
                catchUpTimer += Time.deltaTime;
                Debug.Log("inside catchup");
                if (catchUpTimer >= 5)
                {
                    return FollowState();
                }
            }
            else
            {
                catchUpTimer = 0;
            }

            return this;
        }
    }

    FamiliarFollowState FollowState()
    {
        Debug.Log("Attack To Following State");
        familiarAgent.stoppingDistance = 5;
        playerFamiliar.isEnemyHit = false;
        catchUpTimer = 0;
        return followState;
    }
}
