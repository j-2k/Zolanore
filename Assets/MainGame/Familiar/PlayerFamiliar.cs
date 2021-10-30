using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerFamiliar : MonoBehaviour
{
    public State[] states;
    public NavMeshAgent agentFamiliar;
    public GameObject player;
    public GameObject focusEnemy;
    public bool isEnemyHit;

    private void Awake()
    {
        states = GetComponentsInChildren<State>();
        player = GameObject.FindGameObjectWithTag("Player");
        agentFamiliar = GetComponent<NavMeshAgent>();
    }
}
