using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{

    #region Singleton LevelSystem Instance
    public static LevelSystem instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Another Level System Script?? bug check");
            return;
        }

        instance = this;
    }
    #endregion

    public delegate void OnXPGained(int enemyLevel, int xp);
    public OnXPGained onXPGainedDelegate;

    public int currentLevel;
    public int currentXP;
    [SerializeField] int targetXP;

    int skillPointsTotal;
    public int skillPointsToSpend;

    private int skillPointsGainedPerLevel = 2;

    [SerializeField] SkillPointSpend skillPointSpend;
    [SerializeField] Text levelValueText;

    CharacterManager character;

    // Start is called before the first frame update
    void Start()
    {
        levelValueText.text = currentLevel.ToString();
        character = GetComponentInParent<CharacterManager>();
        currentXP = 0;
        targetXP = 100;
        skillPointsTotal = currentLevel * skillPointsGainedPerLevel;
        onXPGainedDelegate += XPGainedFunction;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ResetAllSkillPoints();
        }
    }

    
    void XPGainedFunction(int incEnemyLevel,int incXP)
    {
        Debug.Log("An Enemy of Level of " + incEnemyLevel + " Died. It Dropped " + incXP + "XP! Now we will add XP based on ur level which is level " + currentLevel);

        float j = 1;
        float maxIterations = 6;
        //ENEMY LEVEL 5 CURRENT LEVEL 10 //i = 0. 5 = 10 X //i=1. 6 = 10 X // i=2. 7 = 10X// i=3. 8 =10X// i=4. 9=10X//   i = 5. 10 = 10 YES
        for (int i = 0; i < 6; i++)
        {
            if (incEnemyLevel == currentLevel + i || incEnemyLevel == currentLevel - i || i == maxIterations - 1)
            {
                float newXP = incXP * j;
                newXP = Mathf.RoundToInt(newXP);
                currentXP += (int)newXP;

                while (currentXP >= targetXP)
                {
                    LevelUp();
                    LevelSkillPoint();
                }

                Debug.Log(" new xp =" + newXP + " enemylvl is = " + incEnemyLevel + " currlevel is " + currentLevel);
                return;
            }
            //maybe be nicer here & dont penalize for first iterator / if lvl diff is +1/-1
            j -= 0.1f;

            /*
            if (incEnemyLevel == currentLevel - i)
            {
                float newXP = incXP * j;
                newXP = Mathf.RoundToInt(newXP);
                currentXP += (int)newXP;

                while (currentXP >= targetXP)
                {
                    currentXP = currentXP - targetXP;
                    currentLevel++;
                    targetXP += targetXP / 20;
                }

                Debug.Log(" new xp =" + newXP + " enemylvl is = " + incEnemyLevel + " currlevel I is " + (currentLevel - i));
                return;//< kinda useless
            }

            if (i == 5)
            {
                float newXP = incXP * j;
                newXP = Mathf.RoundToInt(newXP);
                currentXP += (int)newXP;

                while (currentXP >= targetXP)
                {
                    currentXP = currentXP - targetXP;
                    currentLevel++;
                    targetXP += targetXP / 20;
                }
            }*/

        }

        /*
        if (incEnemyLevel == currentLevel || incEnemyLevel == currentLevel - 1 || incEnemyLevel == currentLevel + 1)
        {
            Debug.Log("Ran EQUAL");
            currentXP += incXP;

            while (currentXP >= targetXP)
            {
                currentXP = currentXP - targetXP;
                currentLevel++;
                targetXP += targetXP / 20;
            }


        }
        else if (incEnemyLevel <= currentLevel - 2)
        {
            Debug.Log("Ran DOWN");
            float newXP = incXP * 0.8f;
            newXP = Mathf.RoundToInt(newXP);
            currentXP += (int)newXP;
        } 
        else if (incEnemyLevel >= currentLevel + 2)
        {
            Debug.Log("Ran UP");
            float newXP = incXP * 0.5f;
            newXP = Mathf.RoundToInt(newXP);
            currentXP += (int)newXP;
        }
        */
    }

    private void LevelUp()
    {
        currentXP = currentXP - targetXP;
        currentLevel++;
        levelValueText.text = currentLevel.ToString();
        targetXP += targetXP * 1; //*1 will double target everytime // it was /20 | *1 = 100 , 200, 400, 800 | /2 = 100, 150,300,600,
    }

    void LevelSkillPoint()
    {
        skillPointsTotal = currentLevel * skillPointsGainedPerLevel;
        skillPointsToSpend += 2;
        CheckSkillPoints();
    }

    public void CheckSkillPoints()
    {
        if (skillPointsToSpend > 0)
        {
            skillPointSpend.gameObject.SetActive(true);
        }
        else
        {
            skillPointSpend.gameObject.SetActive(false);
        }
    }

    public void ResetAllSkillPoints()
    {
        skillPointsToSpend = skillPointsTotal;

        character.Strength.BaseValue = 1;
        character.Dexterity.BaseValue = 1;
        character.Intelligence.BaseValue = 1;
        character.Defence.BaseValue = 1;

        character.UpdateStatSkillPoint();
    }
}
