using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerFamiliar : MonoBehaviour
{
    NavMeshAgent familiarAgent;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        familiarAgent.GetComponentInChildren<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (familiarAgent.remainingDistance  < 1)
        familiarAgent.SetDestination(player.transform.position);
        */
        
    }
}
