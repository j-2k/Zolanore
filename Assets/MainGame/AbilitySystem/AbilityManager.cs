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

    public delegate void AbilityDelegate();
    public static event AbilityDelegate OnGCD;
    public static event AbilityDelegate OnCancelAbility;
    

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

    public void StartGCD()
    {
        OnGCD.Invoke();
    }

    public void CancelAbilites()
    {
        OnCancelAbility.Invoke();
    }
}
