using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootNode : Node<EnemyAgent>
{
    public override NodeState Evaluate(EnemyAgent owner)
    {
        if (owner.isShooting) return NodeState.FAILURE;
        else
        {
            owner.StartCoroutine(owner.shootRoutine());
            return NodeState.SUCCESS;
        }      
    }
}