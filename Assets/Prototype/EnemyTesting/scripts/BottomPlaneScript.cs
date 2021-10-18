using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomPlaneScript : MonoBehaviour
{
    RootMotionMovement playerScript;
    GameObject playerGO;
    [SerializeField] Transform spawnLoc;

    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerGO.GetComponent<RootMotionMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Debug.Log("Hit Player");
            StartCoroutine(NextFrameTP());
        }
    }

    IEnumerator NextFrameTP()
    {
        playerScript.enabled = false;
        playerGO.transform.position = spawnLoc.transform.position;
        yield return new WaitForSeconds(0.1f);
        playerScript.enabled = true;
    }
}
