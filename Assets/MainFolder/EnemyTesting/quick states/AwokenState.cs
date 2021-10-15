using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwokenState : State
{
    IEnumerator currentCouroutine;
    [SerializeField] EnrageState enrageState;
    BossTesting bTest;
    float timer;
    float timer2;
    float timer3;
    [SerializeField] int rand;
    int rand2;

    private void Start()
    {
        timer = 12;
        timer2 = 0;
        timer3 = 0;
        bTest = GetComponentInParent<BossTesting>();
    }



    public override State runCurrentState()
    {
        if (bTest.curHealth <= bTest.maxHealth * 0.2)
        {
            return enrageState;
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= 15)
            {
                rand = Random.Range(1, 3);
                timer = 0;
            }

            timer2 += Time.deltaTime;
            if (timer2 >= 3)
            {
                rand2 = Random.Range(-4, 4);
                timer2 = 0;
            }

            if (rand == 1)
            {
                bTest.transform.Rotate(0, rand2 * 10 * Time.deltaTime, 0);
                timer3 += Time.deltaTime;
                if (timer3 >= 5)
                {
                    Rand1();
                    timer3 = 0;
                }
            }
            else if (rand == 2)
            {
                Vector3 lookVector = bTest.player.transform.position;
                lookVector.y = bTest.transform.position.y;
                bTest.transform.LookAt(lookVector);
                timer3 += Time.deltaTime;
                if (timer3 >= 5)
                {
                    Rand2();
                    timer3 = 0;
                }
            }
            Debug.Log("BOSS: Awoken State...");
            return this;
        }
    }

    void Rand1()
    {
        Instantiate(bTest.sphereObjEnemySpawn, bTest.outPos.transform.position, bTest.outPos.transform.rotation);
    }

    void Rand2()
    {
        Instantiate(bTest.sphereObjBossBullet, bTest.outPos.transform.position, bTest.outPos.transform.rotation);
    }
}
