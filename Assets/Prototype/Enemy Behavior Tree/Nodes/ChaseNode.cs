using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseNode : Node<EnemyAgent>
{
    public override NodeState Evaluate(EnemyAgent owner)
    {
        float distance = Vector3.Distance(owner.transform.position, owner.player.transform.position);
        if (distance < owner.distanceToAttack)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            owner.speed = owner.chaseSpeed;
            owner.Move(owner.player.transform.position + new Vector3(0,0,1.5f));
            return NodeState.RUNNING;
        }        

    }
}
