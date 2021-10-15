using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBossEnemiesScript : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] GameObject meleeEnemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            Debug.Log("Touched a plane");
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 position = contact.point;
            Instantiate(meleeEnemy, position + (Vector3.up * 1), rotation);
            Destroy(gameObject);
        }
    }
}
