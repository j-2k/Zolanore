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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
