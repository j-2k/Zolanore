using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public int enemyHealthTest;
    Transform player;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        enemyHealthTest = 100;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealthTest <= 0)
        {
            Destroy(gameObject);
        }
        Vector3 lookVector = player.transform.position;
        lookVector.y = transform.position.y;
        transform.LookAt(lookVector);

        if (Vector3.Distance(transform.position, player.transform.position) <= 2)
        {
            Debug.Log("enemy is attack or osmething");
        }
        else
        {
            transform.position += speed * transform.forward * Time.deltaTime;
        }
    }

    public void TakeDamageFromPlayer(int incommingDamage)
    {
        enemyHealthTest -= incommingDamage;
    }
}
