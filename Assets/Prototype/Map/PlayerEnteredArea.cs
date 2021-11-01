using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnteredArea : MonoBehaviour
{
    Transform player;
    bool enteredArea = false;
    public Image sprite;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, this.transform.position);
        if(distance< 5)
        {
            sprite.color = Color.green;
            enteredArea = true;
        }
    }
}
