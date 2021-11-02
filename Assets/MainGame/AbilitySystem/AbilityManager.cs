using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    #region Singleton LevelSystem Instance
    public static AbilityManager instance;

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

    //change ability styles here i think

    public AbilityExecuter[] allAbilities;

    /*
    public List<AbilityExecuter> meleeAbilityExecs;

    public List<AbilityExecuter> rangedAbilityExecs;

    public List<AbilityExecuter> magicAbilityExecs;

    public List<AbilityExecuter> familiarAbilityExecs;
    */

    

    private void Start()
    {
        allAbilities = GetComponents<AbilityExecuter>();   //universal abilites maybe

        /*
        for (int i = 0; i < allAbilities.Length; i++)
        {
            if (allAbilities[i].abilityType == CombatType.Melee)
            {
                meleeAbilityExecs.Add(allAbilities[i]);
            }
            else if (allAbilities[i].abilityType == CombatType.Ranged)
            {
                rangedAbilityExecs.Add(allAbilities[i]);
            }
            else if(allAbilities[i].abilityType == CombatType.Magic)
            {
                magicAbilityExecs.Add(allAbilities[i]);
            }
            else if(allAbilities[i].abilityType == CombatType.Familiar)
            {
                familiarAbilityExecs.Add(allAbilities[i]);
            }
        }
        */
    }

    AbilityExecuter pastExecuter;
    public void Activated(AbilityExecuter executer)
    {
        //CACHING
        //FIRST SEND ALL ABILITIES ON CD TO BE ON GCD IF THEY ARE LOWER THAN GCD VALUE
        for (int i = 0; i < allAbilities.Length; i++)
        {
            if (allAbilities[i].abilityState == AbilityExecuter.AbilityState.cooldown && allAbilities[i].cooldownTime <= 3)
            {
                allAbilities[i].abilityState = allAbilities[i].abilityState = AbilityExecuter.AbilityState.gcd;
            }
        }

        if (!executer.ability.bypassCancel)
        {
            for (int i = 0; i < allAbilities.Length; i++)
            {
                if (allAbilities[i].abilityState == AbilityExecuter.AbilityState.active && allAbilities[i] != executer)
                {
                    allAbilities[i].SendToCooldown();
                }
            }
        }
        else
        {
            //bypass cancel and run ability normally ez
        }

        //now handle how to mash up mutliple abilites
        //BUFF TO AOE WILL CANCEL BUFF
        //AOE TO BUFF WONT CANCEL AOE
        //CHECK IF INCOMING ABILITY IS BUFF?
        pastExecuter = executer;
    }
}
