using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloseCheckNode : Node<EnemyAgent>
{
    public override NodeState Evaluate(EnemyAgent owner)
    {
        var distance = Vector3.Distance(owner.transform.position, owner.player.transform.position);

        if (distance < owner.proximityDistance) return NodeState.SUCCESS;
        else return NodeState.FAILURE;
    }

}
