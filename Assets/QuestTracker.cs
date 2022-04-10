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
        questGiver = questSystem.closestQuestGiver.GetComponent<QuestGiver>();
    }

    public void IncrementCount(int goalIndex)
    {

        GameObject goalToIncrement = transform.GetChild(goalIndex).gameObject;
        GameObject goalCounter = goalToIncrement.transform.Find("Counter").gameObject;

        for (int i = 0; i < questGiver.quest.Goals.Count; i++)
        {
            if (questGiver.quest.Goals[i].GetDescription() == transform.GetChild(goalIndex).gameObject.name)
            {
                count[i]++;
                goalCounter.GetComponent<Text>().text = count[i] + " / " + questGiver.quest.Goals[i].RequiredAmount;
                Debug.Log(questGiver.quest.Goals[i].CurrentAmount);
                if (count[i] >= questGiver.quest.Goals[i].RequiredAmount)
                {
                    goalToIncrement.transform.GetChild(2).gameObject.SetActive(true);
                }
            }
        }
    }
}
