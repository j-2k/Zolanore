using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public CharacterManager statManager;

    public void BuffStrength(int statIncrease, float seconds)
    {
        StartCoroutine(StatBuff(statIncrease, seconds));
    }

    IEnumerator StatBuff(int statBuff,float seconds)
    {
        statManager.Strength.BaseValue += statBuff;
        statManager.UpdateStatSkillPoint();
        yield return new WaitForSeconds(seconds);
        statManager.Strength.BaseValue -= statBuff;
        statManager.UpdateStatSkillPoint();
    }

    public void BuffDefence(int statIncrease, float seconds)
    {
        statManager.Defence.BaseValue += statIncrease;
    }

    public void BuffIntelligence(int statIncrease, float seconds)
    {
        statManager.Intelligence.BaseValue += statIncrease;
    }

    public void BuffDexterity(int statIncrease, float seconds)
    {
        statManager.Dexterity.BaseValue += statIncrease;
    }

}
