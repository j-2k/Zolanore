using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNode : Node<EnemyAgent>
{
    public override NodeState Evaluate(EnemyAgent owner)
    {
        var distance = Vector3.Distance(owner.transform.position, owner.player.transform.position);
        if (distance < owner.distanceToAttack)
        {
            Debug.Log("Attacking");
            return NodeState.RUNNING;
        }
        else 
        {
            return NodeState.FAILURE;
        }
    }  
}