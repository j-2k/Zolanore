using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerFamiliar : MonoBehaviour
{
    public NavMeshAgent agentFamiliar;
    public GameObject player;
    public GameObject lastestEnemyHit;
    public bool isEnemyHit;
    public bool isAggressiveFamiliar;
    public bool callFamiliarBack;
    public bool abilityTrigger;
    public EnemyStatManager enemyAbilityFocus;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agentFamiliar = GetComponent<NavMeshAgent>();
    }
}
