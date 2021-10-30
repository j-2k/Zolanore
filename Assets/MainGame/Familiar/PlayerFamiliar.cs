using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerFamiliar : MonoBehaviour
{
    NavMeshAgent familiarAgent;
    GameObject player;
    float timeToTeleport;
    float teleTimer;
    // Start is called before the first frame update
    void Start()
    {
        timeToTeleport = 5;
        player = GameObject.FindGameObjectWithTag("Player");
        familiarAgent = GetComponentInChildren<NavMeshAgent>();
        familiarAgent.SetDestination(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        familiarAgent.SetDestination(player.transform.position);

        if (Vector3.Distance(familiarAgent.transform.position, player.transform.position) >= familiarAgent.stoppingDistance + 3)
        {
            Debug.Log("farcontinuouse");
            teleTimer += Time.deltaTime;
            if (teleTimer >= timeToTeleport)
            {
                familiarAgent.gameObject.transform.position = player.transform.position;
                teleTimer = 0;
            }
        }
        else
        {
            teleTimer = 0;
        }

        
        
    }
}
