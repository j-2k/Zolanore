using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        currentXP = 0;
        onXPGainedDelegate += XPGainedFunction;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    void XPGainedFunction(int incEnemyLevel,int incXP)
    {
        Debug.Log("An Enemy of Level of " + incEnemyLevel + " Died. It Dropped " + incXP + "XP! Now we will add XP based on ur level which is level " + currentLevel);

        if (incEnemyLevel == currentLevel || incEnemyLevel == currentLevel - 1 || incEnemyLevel == currentLevel + 1)
        {
            Debug.Log("Ran EQUAL");
            currentXP += incXP;
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
    }
    
}
