using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    void Start()
    {
        Invoke("DestroyObj", 8);
    }
    void DestroyObj()
    {
        Destroy(this);
    }
}