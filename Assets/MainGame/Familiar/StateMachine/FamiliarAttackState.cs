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

    GameObject enemyCache;

    float attackTimer;
    bool isFarFromPlayer;

    // Start is called before the first frame update
    void Start()
    {
        playerFamiliar = GetComponentInParent<PlayerFamiliar>();
        familiarAgent = GetComponentInParent<NavMeshAgent>();
        player = playerFamiliar.player;
    }

    bool assignOnce = true;

    public override State runCurrentState()
    {
        if (assignOnce)
        {
            enemyCache = playerFamiliar.lastestEnemyHit;
            assignOnce = false;
        }

        if (playerFamiliar.lastestEnemyHit == null || playerFamiliar.callFamiliarBack)
        {
            return FinishedAttacking();
        }
        else
        {
            attackTimer += Time.deltaTime;
        }

        if (playerFamiliar.isAggressiveFamiliar)
        {//keep attacking the enemy untill it dies player must be in range at all times
            if (attackTimer >= 3)
            {
                Debug.Log("<color=blue>Attacked Enemy</color>" + enemyCache.name);
                attackTimer = 0;

                if (!isFarFromPlayer)
                {
                    return AggressiveAttack();
                }
                else
                {
                    return FinishedAttacking();
                }
            }
        }
        else
        {//attack once and return if not aggro
            if (attackTimer >= 3)
            {
                Debug.Log("<color=blue>Attacked Enemy</color>" + enemyCache.name);
                return FinishedAttacking();
            }
        }

        if (Vector3.Distance(familiarAgent.transform.position, player.transform.position) >= 15)
        {
            isFarFromPlayer = true;
        }
        else
        {
            isFarFromPlayer = false;
        }

        return this;
    }

    FamiliarFollowState FinishedAttacking()
    {
        attackTimer = 0;
        familiarAgent.stoppingDistance = 5;
        playerFamiliar.isEnemyHit = false;
        playerFamiliar.callFamiliarBack = false;
        assignOnce = true;
        return followState;
    }

    FamiliarChaseState AggressiveAttack()
    {
        attackTimer = 0;
        playerFamiliar.isEnemyHit = false;
        playerFamiliar.callFamiliarBack = false;
        assignOnce = true;
        return chaseState;
    }
}
