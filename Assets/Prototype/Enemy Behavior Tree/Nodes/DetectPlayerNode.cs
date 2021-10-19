using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerNode : Node<EnemyAgent>
{
    public override NodeState Evaluate(EnemyAgent owner)
    {
        var distance = Vector3.Distance(owner.player.transform.position, owner.transform.position);
        if (distance < owner.distanceToDetect)
        {
            return NodeState.SUCCESS;
        }
        else return NodeState.FAILURE;
    }
}
