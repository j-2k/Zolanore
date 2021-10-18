using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTesting : MonoBehaviour
{
    public GameObject player;
    public int maxHealth;
    public int curHealth;
    public Transform outPos;

    public GameObject sphereObjBossBullet;
    public GameObject sphereObjEnemySpawn;

    public GameObject[] orbs;

    float timer;

    [SerializeField] HPBar bossHPbar;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        maxHealth = 300;
        curHealth = maxHealth;
        bossHPbar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 10)
        {
            int rand = Random.Range(0, 4);
            for (int i = 0; i < orbs.Length; i++)
            {
                orbs[i].SetActive(false);
            }
            orbs[rand].SetActive(true);
            timer = 0;
        }
    }

    public void TakeDamageFromOrb(int incomingDamage)
    {
        curHealth -= incomingDamage;
        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }
        bossHPbar.SetHealth(curHealth);
    }
}
