using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class B_Awoken : Boss_State
{
    [SerializeField] Transform center;
    [SerializeField] Transform[] waypoints;
    [SerializeField] int randWP;
    [SerializeField] float speed = 5;
    Vector3 movementVec,player;
    public override void BossOnCollisionEnter(Boss_StateMachine bsm, Collider collider)
    {
        //throw new System.NotImplementedException();
    }

    public override void StartState(Boss_StateMachine bsm)
    {
        bsm.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        bsm.transform.position = center.transform.position;
        randWP = Random.Range(0, waypoints.Length);
        movementVec = new Vector3(waypoints[randWP].position.x, 0, waypoints[randWP].position.z);
    }

    public override void UpdateState(Boss_StateMachine bsm)
    {
        player = new Vector3(bsm.player.transform.position.x, bsm.transform.position.y, bsm.player.transform.position.z);
        bsm.transform.LookAt(player);
        WaypointDistanceCheck(bsm.transform);
        bsm.transform.position += movementVec.normalized * speed * Time.deltaTime;
        //bsm.transform.LookAt(waypoints[randWP].position);
        //bsm.transform.position += transform.forward * speed * Time.deltaTime;
        Debug.Log("Awoken State");
        Debug.DrawRay(bsm.transform.position, movementVec);
    }

    void WaypointDistanceCheck(Transform bsm)
    {
        if (Vector3.Distance(bsm.transform.position, waypoints[randWP].position) <= 2)
        {
            randWP = Random.Range(0, waypoints.Length);
        }
        else
        {
            movementVec = waypoints[randWP].position - bsm.transform.position;
            movementVec.y = 0;
        }
    }

    
}
