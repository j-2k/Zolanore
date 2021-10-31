using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamiliarAttackState : State
{

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    public override State runCurrentState()
    {
        return this;
    }
}
