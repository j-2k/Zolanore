using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AbilityCooldownUI : MonoBehaviour
{
    AbilityManager abilityManager;
    Image image;
    [SerializeField] int indexAbility;
    // Start is called before the first frame update
    void Start()
    {
        abilityManager = AbilityManager.instance;
        image = GetComponent<Image>();
        image.fillAmount = 0;
        //AbilityManager.onGCD += GCDCooldown;
    }
    bool oneRun = true;
    // Update is called once per frame
    void Update()
    {

        if (abilityManager.meleeAbilityExecs[indexAbility].abilityState == AbilityExecuter.AbilityState.cooldown)
        {
            if (oneRun)
            {
                image.fillAmount = 1;
                oneRun = false;
            }
            else
            {
                image.fillAmount -= 1 / abilityManager.meleeAbilityExecs[indexAbility].cooldownTimeMax * Time.deltaTime;
            }
            
        }
        else
        {
            image.fillAmount = 0;
            oneRun = true;
        }
    }

    /*
    public void GCDCooldown()
    {
        if (!gcdTrigger)
        {
            Debug.Log("trigger gcd func");
            abilityState = AbilityState.cooldown;
            cooldownTime = gcd;
        }
        if (cooldownTime <= gcd)
        {
            cooldownTime = gcd;
        }
    }*/
}
