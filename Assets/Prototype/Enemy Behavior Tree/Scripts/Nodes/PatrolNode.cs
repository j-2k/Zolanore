using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolNode : Node<EnemyAgent>
{
    EnemyState playerState = EnemyState.Walk;
    public override NodeState Evaluate(EnemyAgent owner)
    {
        owner.speed = owner.walkSpeed;
        var distance = Vector3.Distance(owner.transform.position, owner.Waypoints[owner.index].position);
        if(playerState == EnemyState.Walk)
        {
            if (distance < 0.3f)
            {
                playerState = EnemyState.PlayIdle;
                if (playerState == EnemyState.PlayIdle)
                {
                    owner.StartCoroutine(IdleAnimation(owner));
                }
            }
            else
            {
                owner.navmesh.stoppingDistance = 0;
                owner.anim.SetInteger("state", 3);
                owner.Move(owner.Waypoints[owner.index].position);
            }
        }
        return NodeState.SUCCESS;
    }

    IEnumerator IdleAnimation(EnemyAgent owner)
    {
        playerState = EnemyState.IdlePlaying;
        owner.anim.SetInteger("state", 0);
        yield return new WaitForSeconds(2);
        owner.index++; owner.index %= owner.Waypoints.Count;
        playerState = EnemyState.Walk;
        owner.StopAllCoroutines();
    }
    enum EnemyState
    {
        Walk,
        PlayIdle,
        IdlePlaying,
    }
}
