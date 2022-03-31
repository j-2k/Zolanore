using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_StateMachine : MonoBehaviour
{
    public CharacterManager player;
    public NavMeshAgent agent;

    Boss_State currentState;
    public B_Awoken awokenState;// = new B_Awoken();
    public B_Chase chaseState;// = new B_Chase();
    public B_Attack1 attack1State;// = new B_Attack1();
    public B_Attack2 attack2State;// = new B_Attack2();
    public B_Summon summonState;// = new B_Summon();
    public B_Death deathState;// = new B_Death();

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterManager>();
        agent = GetComponent<NavMeshAgent>();

        currentState = awokenState;

        currentState.StartState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void BossSwitchState(Boss_State newState)
    {
        currentState = newState;
        newState.StartState(this);
    }
}
