using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAgent : MonoBehaviour
{
    [HideInInspector] public GameObject player;
    [HideInInspector] public int index;
    [HideInInspector] public float speed;
    [HideInInspector] public bool playerDetected = false;
    [HideInInspector] public bool isShooting = false;
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

    [Header("Shooting")]
    public Transform shootPos;
    public GameObject bullet;
    public float bulletSpeed, fireRate;

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
        if (playerDetected) transform.LookAt(player.transform.position);
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


    public IEnumerator shootRoutine()
    {
        if (isShooting) yield break;

        isShooting = true;

        GameObject tempBullet = Instantiate(bullet, shootPos.position, Quaternion.identity);
        Rigidbody bulletRB = tempBullet.GetComponent<Rigidbody>();
        Vector3 targetDir = (player.transform.position - transform.position).normalized;
        bulletRB.AddForce(targetDir * bulletSpeed);
        yield return new WaitForSeconds(fireRate);

        isShooting = false;
    }
}
