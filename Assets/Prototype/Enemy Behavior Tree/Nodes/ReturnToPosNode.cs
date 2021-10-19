using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPosNode : Node<EnemyAgent>
{
    public override NodeState Evaluate(EnemyAgent owner)
    {
        var distance = Vector3.Distance(owner.transform.position, owner.initialPos);
        if (distance < 1)
        {
            owner.transform.position = owner.initialPos;
            return NodeState.SUCCESS;
        }
        else
        {
            owner.speed = owner.walkSpeed;
            owner.Move(owner.initialPos);
            return NodeState.RUNNING;
        }
    }

}
