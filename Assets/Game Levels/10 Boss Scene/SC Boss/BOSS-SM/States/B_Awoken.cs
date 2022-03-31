using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class B_Awoken : Boss_State
{
    [SerializeField] Transform center;
    [SerializeField] Transform[] waypoints;
    int rand;

    public override void BossOnCollisionEnter(Boss_StateMachine bsm, Collider collider)
    {
        //throw new System.NotImplementedException();
    }

    public override void StartState(Boss_StateMachine bsm)
    {
        bsm.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        bsm.transform.position = center.transform.position;
        rand = Random.Range(0, waypoints.Length);
    }

    public override void UpdateState(Boss_StateMachine bsm)
    {
        
        Debug.Log("Awoken State");
    }
}
