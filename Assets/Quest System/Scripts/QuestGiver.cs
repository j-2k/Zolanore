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
    
    private void Awake()
    {
        questSystem = FindObjectOfType<QuestSystem>();
        marker = transform.GetChild(0).Find("Marker").gameObject;
    }
    private void Update()
    {
        if(questSystem.closestQuestGiver == this.gameObject)
        {

        }
        if (claimedQuest)
        {
            marker.SetActive(false);
            questActive = false;
            gameObject.tag = "Untagged";
            GetComponent<QuestGiver>().enabled = false;
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
}