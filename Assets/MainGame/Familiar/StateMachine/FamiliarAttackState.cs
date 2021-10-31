using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FamiliarAttackState : State
{
    [SerializeField] FamiliarFollowState followState;
    [SerializeField] FamiliarChaseState chaseState;
    PlayerFamiliar playerFamiliar;
    NavMeshAgent familiarAgent;
    GameObject player;

    [SerializeField] EnemyProtoVersion enemyCache;

    float attackTimer;

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
            if (playerFamiliar.states[i].name == "ChaseState")
            {
                chaseState = (FamiliarChaseState)playerFamiliar.states[i];
            }
        }

        player = playerFamiliar.player;
        familiarAgent = playerFamiliar.agentFamiliar;
    }

    public override State runCurrentState()
    {
        if (playerFamiliar.focusEnemy == null)
        {
            return FinishedAttacking();
        }
        else
        {
            attackTimer += Time.deltaTime;
        }

        if (playerFamiliar.isAggressiveFamiliar)
        {//keep attacking the enemy untill it dies player must be in range at all times
            if (attackTimer >= 1)
            {
                Debug.Log("<color=blue>Attacked Enemy</color>");
                attackTimer = 0;
                return chaseState;
            }
        }
        else
        {//attack once and return if not aggro
            if (attackTimer >= 1)
            {
                Debug.Log("<color=blue>Attacked Enemy</color>");
                return FinishedAttacking();
            }
        }

        return this;
    }

    FamiliarFollowState FinishedAttacking()
    {
        attackTimer = 0;
        playerFamiliar.isEnemyHit = false;
        return followState;
    }
}
