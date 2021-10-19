using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAgent : MonoBehaviour
{
    [HideInInspector] public GameObject player;
    [HideInInspector] public int index;
    [HideInInspector] public float speed;
    [HideInInspector] public Vector3 initialPos;

    [Header("Movement")]
    public List<Transform> Waypoints;
    public float walkSpeed;
    public float chaseSpeed;

    [Header("Distance Check")]
    public float distanceToDetect;      // Distance to start Checking for player
    public float proximityDistance;     // Distance to detech player without vision
    public float distanceToAttack;      // Distance to switch to attack behavior
    public float visionRange;           // Distance to see player

    private NavMeshAgent navmesh;
    public Selector<EnemyAgent> root;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navmesh = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        initialPos = transform.position;
        BuildBehaviorTree();
    }

    private void Update()
    {
        navmesh.speed = speed;
        root.Evaluate(this);
    }

    public virtual void BuildBehaviorTree()
    {

    }

    public void Move(Vector3 destination)
    {
        navmesh.destination = destination;
    }
}
