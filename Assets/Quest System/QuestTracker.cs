using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTracker : MonoBehaviour
{
    QuestGiver questGiver;
    QuestSystem questSystem;

    List<int> count = new List<int> { 0, 0, 0, 0, 0 };

    private void Awake()
    {
        questSystem = FindObjectOfType<QuestSystem>();
    }

    private void Update()
    {
    }

    public void IncrementCount(int goalIndex)
    {
        GameObject goalToIncrement = transform.GetChild(goalIndex).gameObject;
        GameObject goalCounter = goalToIncrement.transform.Find("Counter").gameObject;

        for (int i = 0; i < questSystem.questGivers.Count; i++)
        {
            for (int j = 0; j < questSystem.questGivers[i].GetComponent<QuestGiver>().quest.Goals.Count; j++)
            {
                if (questSystem.questGivers[i].GetComponent<QuestGiver>().quest.Goals[j].GetDescription() == transform.GetChild(goalIndex).gameObject.name)
                {
                    count[goalIndex]++;
                    goalCounter.GetComponent<Text>().text = count[goalIndex] + " / " + questSystem.questGivers[i].GetComponent<QuestGiver>().quest.Goals[j].RequiredAmount;
                    if (count[goalIndex] >= questSystem.questGivers[i].GetComponent<QuestGiver>().quest.Goals[j].RequiredAmount)
                    {
                        goalToIncrement.transform.GetChild(2).gameObject.SetActive(true);
                    }
                    return;
                }
            }

        }


    }
}
