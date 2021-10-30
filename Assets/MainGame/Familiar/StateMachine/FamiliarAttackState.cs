using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamiliarAttackState : State
{
    [SerializeField] FamiliarFollowState followState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override State runCurrentState()
    {
        //return followState;
        return this;
    }
}
