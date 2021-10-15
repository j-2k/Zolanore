using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnrageState : State
{
    IEnumerator currentCouroutine;
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
        if (bTest.curHealth <= 0)
        {
            Debug.Log("you win");
            gameObject.SetActive(false);
            return this;
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= 10)
            {
                rand = Random.Range(1, 3);
                timer = 0;
            }

            timer2 += Time.deltaTime;
            if (timer2 >= 1)
            {
                rand2 = Random.Range(-4, 4);
                timer2 = 0;
            }

            if (rand == 1)
            {
                bTest.transform.Rotate(0, rand2 * 10 * Time.deltaTime, 0);
                timer3 += Time.deltaTime;
                if (timer3 >= 2)
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
                if (timer3 >= 2)
                {
                    Rand2();
                    timer3 = 0;
                }
            }
            Debug.Log("BOSS: Enrage State...");
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
