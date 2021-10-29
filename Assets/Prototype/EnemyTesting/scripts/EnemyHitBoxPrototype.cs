using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBoxPrototype : MonoBehaviour
{
    //RootMotionMovement rmPlayer;
    Player rmPlayer;
    PlayerScript smPlayer;

    [SerializeField] bool isPlayerRootMotion;
    // Start is called before the first frame update
    void Start()
    {   
        if (rmPlayer == null)
        {
            //rmPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<RootMotionMovement>();
            rmPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            if (rmPlayer !=null)
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
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Debug.Log("Enemy Hit Player Successfully");
            if (isPlayerRootMotion)
            {
                Debug.Log("Making player take 10 dmg");
                //rmPlayer.TakeDamageFromEnemy(10);
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Making player take 10 dmg");
                //smPlayer.TakeDamageFromEnemy(10);
                gameObject.SetActive(false);
            }
        }
    }
}
