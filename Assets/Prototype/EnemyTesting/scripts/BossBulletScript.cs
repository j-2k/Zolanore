using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletScript : MonoBehaviour
{
    [SerializeField] float speed;
    Transform player;
    [SerializeField] bool isPlayerRootMotion;

    RootMotionMovement rmPlayer;
    PlayerScript smPlayer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.LookAt(player.transform.position);
        // Start is called before the first frame update

        if (rmPlayer == null)
        {
            rmPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<RootMotionMovement>();
            if (rmPlayer != null)
            {
                isPlayerRootMotion = true;
            }
        }

        if (smPlayer == null)
        {
            smPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
            if (smPlayer != null)
            {
                isPlayerRootMotion = false;
            }
        }
        /*
        if (isPlayerRootMotion)
        {
            rmPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<RootMotionMovement>();
        }
        else
        {
            smPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        }
        */

    }
    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 5);
        transform.position +=  transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Debug.Log("Bullet Hit Player Successfully");
            if (isPlayerRootMotion)
            {
                Debug.Log("Making player take 10 dmg");
                rmPlayer.TakeDamageFromEnemy(20);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Making player take 10 dmg");
                smPlayer.TakeDamageFromEnemy(20);
                Destroy(gameObject);
            }
        }
    }
}
