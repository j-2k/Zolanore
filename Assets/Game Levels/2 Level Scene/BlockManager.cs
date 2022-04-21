using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] GameObject block;
    [SerializeField] GameObject player;
    [SerializeField] List<GameObject> listOfBlocks;

    [SerializeField] bool buildCustomSize;
    [SerializeField] int customSizeX;
    [SerializeField] int customSizeY;

    // Start is called before the first frame update
    void Start()
    {   //-2 -1 0 1 2 (5x5) | -1 0 1 (3x3) | -3 -2 -1 0 1 2 3(7x7)? 7/2 = 3.5
        if (!buildCustomSize)
        {
            for (int x = -2; x < 3; x++)
            {
                for (int y = -2; y < 3; y++)
                {
                    listOfBlocks.Add(Instantiate(block, new Vector3(x * 100, 0, y * 100), Quaternion.identity));
                }
            }
        }
        else
        {
            for (int x = -customSizeX/2; x < (customSizeX/2) + 1; x++)
            {
                for (int y = -customSizeY/2; y < (customSizeY/ 2) + 1; y++)
                {
                    listOfBlocks.Add(Instantiate(block, new Vector3(x * 100, 0, y * 100), Quaternion.identity));
                }
            }
        }
        lastQuad = new Vector3(0, 0, 0);
    }

    Vector3 lastQuad;
    int arrayShift = 5;
    int arrayZShift = 0; //0 for forward 4 for back
    int arrayXShift = 0;
    int increment = 0;
    // Update is called once per frame
    void Update()
    {
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

        if (player.transform.position.x <= lastQuad.x - 50)
        {
            ExecuteXleft();
        }
    }

    void ExecuteZbackwards()
    {
        if (arrayZShift <= 0)
        {
            arrayZShift = 4;
        }
        else
        {
            arrayZShift--;
        }

        Debug.Log("Executing Z backward Shift");
        for (int i = arrayZShift; i < listOfBlocks.Count;)
        {
            listOfBlocks[i].transform.position = new Vector3(listOfBlocks[i].transform.position.x, listOfBlocks[i].transform.position.y, listOfBlocks[i].transform.position.z - 500);
            i += arrayShift;
        }

        lastQuad.z -= 100;
    }

    void ExecuteZforward()
    {
        Debug.Log("Executing Z Forward Shift");
        for (int i = arrayZShift; i < listOfBlocks.Count;)
        {
            listOfBlocks[i].transform.position = new Vector3(listOfBlocks[i].transform.position.x, listOfBlocks[i].transform.position.y, listOfBlocks[i].transform.position.z + 500);
            i += arrayShift;
        }
        
        if (arrayZShift >= 4)
        {
            arrayZShift = 0;
        }
        else
        {
            arrayZShift++;
        }

        lastQuad.z += 100;
    }

    void ExecuteXleft()
    {
        if (arrayXShift <= 0)
        {
            arrayXShift = 20;
        }
        else
        {
            arrayXShift -= 5;
        }

        Debug.Log("Executing X Left Shift");
        for (int i = arrayXShift; i < listOfBlocks.Count;)
        {
            listOfBlocks[i].transform.position = new Vector3(listOfBlocks[i].transform.position.x - 500, listOfBlocks[i].transform.position.y, listOfBlocks[i].transform.position.z);
            i++;

            increment++;
            if (increment >= 5)
            {
                increment = 0;
                break;
            }
        }

        lastQuad.x -= 100;
    }

    void ExecuteXright()
    {
        Debug.Log("Executing X Right Shift");
        for (int i = arrayXShift; i < listOfBlocks.Count;)
        {
            listOfBlocks[i].transform.position = new Vector3(listOfBlocks[i].transform.position.x + 500, listOfBlocks[i].transform.position.y, listOfBlocks[i].transform.position.z);
            i++;

            increment++;
            if (increment >= 5)
            {
                increment = 0;
                break;
            }
        }

        if (arrayXShift >= 20)
        {
            arrayXShift = 0;
        }
        else
        {
            arrayXShift += 5;
        }

        lastQuad.x += 100;
    }
}
