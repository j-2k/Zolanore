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

    public AbilityExecuter[] meleeAbilityExecs;

    public delegate void GCDSTART();
    public static event GCDSTART onGCD;

    public void StartGCD()
    {
        onGCD.Invoke();
    }
}
