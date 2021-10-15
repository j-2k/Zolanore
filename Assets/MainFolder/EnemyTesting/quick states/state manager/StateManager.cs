using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] State currentState;


    // Update is called once per frame
    void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        //? = if var is null dont run  if !null run
        State nextState = currentState?.runCurrentState();

        if (nextState != null)
        {
            SwitchNextState(nextState);
        }
    }

    private void SwitchNextState(State nextState)
    {
        currentState = nextState;
    }
}
