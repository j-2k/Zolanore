using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFast : MonoBehaviour
{
    public bool isHit;
    [SerializeField] BossTesting bt;
    public int damageComingFromPlayer;
    // Start is called before the first frame update
    void Start()
    {
        isHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(20 * Time.deltaTime, 2 * Time.deltaTime, 3* Time.deltaTime);
        if (isHit)
        {
            HitOneTime();
        }
    }

    void HitOneTime()
    {
        isHit = false;
        bt.TakeDamageFromOrb(damageComingFromPlayer);
    }


}
