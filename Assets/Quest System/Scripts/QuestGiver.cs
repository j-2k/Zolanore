using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public GameObject goalsPrefab;

    public bool acceptedQuest = false;
    public bool completedQuest = false;
    public bool questActive = false;
    public bool claimedQuest = false;

    GameObject marker;

    QuestSystem questSystem;
    QuestGiver questGiver;

    
    private void Awake()
    {
        questSystem = FindObjectOfType<QuestSystem>();
        questGiver = GetComponent<QuestGiver>();
        marker = transform.GetChild(0).Find("Marker").gameObject;
    }
    private void Update()
    {
        if(acceptedQuest)
        {
            questActive = true;
        }
        if (claimedQuest)
        {
            questSystem.completedQuests++;
            marker.SetActive(false);
            questActive = false;
            gameObject.tag = "Untagged";
            questGiver.enabled = false;
            Invoke("DestroyQuestGiver", 15);
        }
        else
        {
            if (quest.Completed) completedQuest = true;
            if (completedQuest || !acceptedQuest)
            {
                marker.SetActive(true);
            }
            else
            {
                marker.SetActive(false);
            }
        }
    }

    void DestroyQuestGiver()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}