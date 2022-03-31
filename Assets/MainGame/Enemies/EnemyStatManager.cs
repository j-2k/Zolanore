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
    [SerializeField] int enemyLevel;
    [SerializeField] int bonusHealth;
    [SerializeField] int damage;
    [SerializeField] int defence;

    [Header("Dont touch these")]
    [SerializeField] int maxHealth;
    [SerializeField] int curHealth;
    [SerializeField] int xp;
    [SerializeField] int bonusXP;

    NavMeshAgent agent;

    LevelSystem levelSystem;

    QuestManager questManager;

    public uint hitID;

    public HealthBar hpBar;
    // Start is called before the first frame update
    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();

        levelSystem = LevelSystem.instance;

        //current enemy level is set to players level
        enemyLevel = levelSystem.currentLevel;

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

        return damage;
    }

    public void TakeDamageFromPlayer(int incDmg)
    {
        incDmg -= defence;
        incDmg = Mathf.Clamp(incDmg, 0, int.MaxValue);
        curHealth -= incDmg;
        hpBar.SetHealth(curHealth);
        if (curHealth <= 0)
        {
            levelSystem.onXPGainedDelegate.Invoke(enemyLevel, xp);
            questManager.Kill("Wolf");
            Destroy(gameObject);
        }
    }

    public void TakeDamageFromFamiliar(int incDmg)
    {
        incDmg -= defence;
        incDmg = Mathf.Clamp(incDmg, 0, int.MaxValue);
        curHealth -= incDmg;
        hpBar.SetHealth(curHealth);
        if (curHealth <= 0)
        {
            levelSystem.onXPGainedDelegate.Invoke(enemyLevel, xp);
            questManager.Kill("Wolf");
            Destroy(gameObject);
        }
    }
}
