using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPanel : MonoBehaviour
{
    //https://youtu.be/ez-YTf64Jn4?t=578
    [SerializeField] StatDisplay[] statDisplays;
    [SerializeField] string[] statNames;

    //private CharacterStat[] stats;

    private void OnValidate()
    {
        statDisplays = GetComponentsInChildren<StatDisplay>();
        UpdateStatNames();
    }

    /*
    public void SetStats(params CharacterStat[] charStats)
    {
        stats = charStats;

        if (stats.Length > statDisplays.Length)
        {
            Debug.LogError("Too many stats, not enough displays ");
            return;
        }

        for (int i = 0; i < statDisplays.Length; i++)
        {
            statDisplays[i].gameObject.SetActive(i < stats.Length);
        }
    }
    

    public void UpdateStatValue()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            statDisplays[i].ValueText.text = stats[i].Value.ToString();
        }
    }
    */

    public void UpdateStatNames()
    {
        for (int i = 0; i < statNames.Length; i++)
        {
            statDisplays[i].NameText.text = statNames[i];
        }
    }    
}
