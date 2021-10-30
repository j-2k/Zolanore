using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamiliarFollowState : State
{
    [SerializeField] FamiliarAttackState attackState;


    private void Start()
    {

    }

    public override State runCurrentState()
    {
        //return attackState;
        return this;
    }
}
