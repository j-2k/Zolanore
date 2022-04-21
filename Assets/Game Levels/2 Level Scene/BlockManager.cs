using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] GameObject block;
    [SerializeField] GameObject player;
    [SerializeField] List<GameObject> listOfBlocks;

    // Start is called before the first frame update
    void Start()
    {   //-2 -1 0 1 2 (5x5) | -1 0 1 (3x3)
        for (int x = -2; x < 3; x++)
        {
            for (int y = -2; y < 3; y++)
            {
                listOfBlocks.Add(Instantiate(block, new Vector3(x * 100, 0, y * 100), Quaternion.identity));
            }
        }
        lastQuad = new Vector3(0, 0, 0);
    }

    Vector3 lastQuad;
    int arrayShift = 5;
    int arrayStart = 0;
    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z >= lastQuad.z + 50)
        {
            Debug.Log("Front by 50");
        }

        if (player.transform.position.z <= lastQuad.z - 50)
        {
            Debug.Log("Back by 50");
        }

        if (player.transform.position.z >= lastQuad.z + 50 || player.transform.position.z <= lastQuad.z - 50)
        {
            //Debug.Log("Front by 50"); //entered a new chunk on z forward

            for (int i = arrayStart; i < listOfBlocks.Count;)
            {
                listOfBlocks[i].transform.position = new Vector3(listOfBlocks[i].transform.position.x, listOfBlocks[i].transform.position.y, listOfBlocks[i].transform.position.z + 500);
                i += arrayShift;
            }

            if (arrayStart >= 4)
            {
                arrayStart = 0;
            }
            else
            {
                arrayStart++;
            }

            lastQuad.z += 100;
        }
    }
}
