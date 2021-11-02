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

    public void Activated(AbilityExecuter executer)
    {
        for (int i = 0; i < allAbilities.Length; i++)
        {
            //FIRST SEND ALL ABILITIES ON CD TO BE ON GCD IF THEY ARE LOWER THAN GCD VALUE
            if (allAbilities[i].abilityState == AbilityExecuter.AbilityState.cooldown && allAbilities[i].cooldownTime <= 3)
            {
                allAbilities[i].abilityState = allAbilities[i].abilityState = AbilityExecuter.AbilityState.gcd;
            }

            //SECONDLY CANCEL ANY ABILITIES THAT DONT HAVE A BYPASS DURING AN ACTIVE ABILITY | EG. IF AOE ON THEN BUFF ON SHOULD NOT CANCEL AOE | EG IF AOE IS ON AND DASH IS TRUE AOE SHOULD TURN OFF & DASH
            if (allAbilities[i].abilityState == AbilityExecuter.AbilityState.active && allAbilities[i] != executer)
            {
                if (!allAbilities[i].ability.bypassCancel && !executer.ability.bypassCancel)  //finally found solution lmao
                {
                    Debug.Log("cooling down");
                    allAbilities[i].SendToCooldown();
                }
            }
        }
    }
}
