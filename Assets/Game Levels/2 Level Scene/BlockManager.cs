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
    int arrayStart = 0; //0 for forward 4 for back
    // Update is called once per frame
    void Update()
    {
        Debug.Log(arrayStart + " Array start");
        if (player.transform.position.z >= lastQuad.z + 50)
        {
            Debug.Log("Front by 50");
            //arrayStart = 0;
        }

        if (player.transform.position.z <= lastQuad.z - 50)
        {
            Debug.Log("Back by 50");
            //arrayStart = 4;
        }

        if (player.transform.position.z >= lastQuad.z + 50)
        {
            ExecuteZforward();
        }

        if (player.transform.position.z <= lastQuad.z - 50)
        {
            ExecuteZbackwards();
        }

        if (player.transform.position.x >= lastQuad.x + 50)
        {
            ExecuteXright();
        }
    }

    void ExecuteZbackwards()
    {
        if (arrayStart <= 0)
        {
            arrayStart = 4;
        }
        else
        {
            arrayStart--;
        }

        Debug.Log("Executing Z backward Shift");
        for (int i = arrayStart; i < listOfBlocks.Count;)
        {
            listOfBlocks[i].transform.position = new Vector3(listOfBlocks[i].transform.position.x, listOfBlocks[i].transform.position.y, listOfBlocks[i].transform.position.z - 500);
            i += arrayShift;
        }

        lastQuad.z -= 100;
    }

    void ExecuteZforward()
    {
        Debug.Log("Executing Z Forward Shift");
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

    void ExecuteXleft()
    {
        if (arrayStart <= 0)
        {
            arrayStart = 4;
        }
        else
        {
            arrayStart--;
        }

        Debug.Log("Executing Z backward Shift");
        for (int i = arrayStart; i < listOfBlocks.Count;)
        {
            listOfBlocks[i].transform.position = new Vector3(listOfBlocks[i].transform.position.x, listOfBlocks[i].transform.position.y, listOfBlocks[i].transform.position.z - 500);
            i += arrayShift;
        }

        lastQuad.z -= 100;
    }

    void ExecuteXright()
    {
        Debug.Log("Executing X Right Shift");
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
