using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStatManager : MonoBehaviour
{
    //https://docs.google.com/spreadsheets/d/19eI5ft2jUsELaEdoNECQKZm7XY9agH4JqVokp11H8Oc/edit#gid=583637899
    //dont change anything here with xp handling more info in the doc
    //a very simple enemy stat manager need to expand on this more later on
    [Header("Assign Level & extra stats")]
    [Header("ENEMY LEVEL IS PRE-DEF TO PLAYER LVL CHECK CODE")]
    [SerializeField] string questEnemyName;
    [SerializeField] GameObject questTracker;
    [SerializeField] int enemyLevel;
    [SerializeField] int bonusLevel;
    [SerializeField] int bonusDamage;
    [SerializeField] int bonusHealth;
    [SerializeField] int damage;
    [SerializeField] int defence;
    public bool invulnerable = false;

    [Header("Dont touch these")]
    [SerializeField] bool isBoss;
    [SerializeField] Boss_StateMachine boss;
    [SerializeField] int maxHealth;
    [SerializeField] int curHealth;
    [SerializeField] int xp;
    [SerializeField] int bonusXP;

    NavMeshAgent agent;

    LevelSystem levelSystem;

    QuestManager questManager;

    [SerializeField] GameObject drop;
    public uint hitID;

    public HealthBar hpBar;

    private void Awake()
    {
        questTracker = GameObject.Find("QuestTracker");

    }
    void Start()
    {
        questTracker.SetActive(false);
        drop = GameObject.Find("ItemDrop");
        questManager = FindObjectOfType<QuestManager>();

        levelSystem = LevelSystem.instance;

        //current enemy level is set to players level
        enemyLevel = levelSystem.currentLevel + bonusLevel;

        //
        agent = GetComponent<NavMeshAgent>();
        maxHealth = enemyLevel * (int)(100 * Random.Range(0.75f, 1.25f));
        maxHealth += bonusHealth;
        curHealth = maxHealth;

        if (hpBar != null)
        {
            hpBar.SetMaxHealth(maxHealth);
        }

        //think of formula here based on level this is placeholder for defence & xp
        defence = (enemyLevel * 2) - 1;
        xp = (enemyLevel * 2) + 50;
        xp += bonusXP;
    }

    public int DamageCalculation()
    {
        damage = (int)((enemyLevel + Random.Range(6f, 10f)) * Random.Range(0.8f, 1.2f));

        Debug.Log("Damaging Player for " + damage + "Dmg");

        return damage + bonusDamage;
    }

    bool bossTrigger = false;

    public void TakeDamageFromPlayer(int incDmg)
    {
        if (!invulnerable)
        {
            incDmg -= defence;
            incDmg = Mathf.Clamp(incDmg, 0, int.MaxValue);
            curHealth -= incDmg;
            //disabled for testing
            hpBar.SetHealth(curHealth);

            if (isBoss)
            {
                if (curHealth <= maxHealth / 2 && !bossTrigger)
                {
                    bossTrigger = true;
                    boss.bossPhase = 2;
                    boss.BossSwitchState(boss.awokenState);
                }
                else if (curHealth <= 0)
                {
                    boss.BossSwitchState(boss.deathState);
                }
            }
            else
            {
                if (curHealth <= 0)
                {
                    levelSystem.onXPGainedDelegate.Invoke(enemyLevel, xp);
                    if (questTracker.transform.childCount != 0)
                    {
                        for (int i = 0; i < questTracker.transform.childCount; i++)
                        {
                            string tempString = "Kill " + questEnemyName;
                            if (tempString == questTracker.transform.GetChild(i).name)
                            {
                                questTracker.GetComponent<QuestTracker>().IncrementCount(i);
                            }
                        }
                    }
                    /*
                    questManager.Kill(questEnemyName);
                    int dropPerc = Random.Range(1, 100);
                    Debug.Log(dropPerc);
                    if (dropPerc <= 20)
                    {
                        Instantiate(drop, transform.position + Vector3.up * 2, Quaternion.identity);
                    }
                    */
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            return;
        }
    }
}
