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

    public List<AbilityExecuter.AbilityState> allAbilityStates;

    /*
    public List<AbilityExecuter> meleeAbilityExecs;

    public List<AbilityExecuter> rangedAbilityExecs;

    public List<AbilityExecuter> magicAbilityExecs;

    public List<AbilityExecuter> familiarAbilityExecs;
    */

    public delegate void AbilityDelegate();
    public static event AbilityDelegate OnActivateAbility;
    

    private void Start()
    {
        allAbilities = GetComponents<AbilityExecuter>();   //universal abilites maybe
        for (int i = 0; i < allAbilities.Length; i++)
        {
            allAbilityStates.Add(allAbilities[i].abilityState);
        }

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

    public void Update()
    {
        
    }

    public void Activated(AbilityExecuter executer)
    {
        //setting other abilities to on gcd if lower than gcd cd
        for (int i = 0; i < allAbilities.Length; i++)
        {
            if (allAbilities[i].abilityState == AbilityExecuter.AbilityState.cooldown && allAbilities[i].cooldownTime <= 3)
            {
                allAbilities[i].abilityState = allAbilities[i].abilityState = AbilityExecuter.AbilityState.gcd;
            }
        }

        //now handle how to mash up mutliple abilites

    }
}
