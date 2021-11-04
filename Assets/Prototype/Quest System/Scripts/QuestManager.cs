using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private GameObject questPrefab;
    [SerializeField] private Transform questsContent;
    [SerializeField] private GameObject questHolder;

    public List<Quest> CurrentQuests;

    private void Awake()
    {
        foreach (var quest in CurrentQuests)
        {
            quest.Initialize();
            quest.QuestCompleted.AddListener(OnQuestCompleted);

            GameObject questObj = Instantiate(questPrefab, questsContent);

            questObj.GetComponent<Button>().onClick.AddListener(delegate
            {
                InitializeWindow(quest);
            });
        }
    }

    public void InitializeWindow(Quest quest)
    {
        questHolder.GetComponent<QuestWindow>().Initialize(quest);
        questHolder.SetActive(true);
    }

    public void Kill(string buildingName)
    {
        EventManager.Instance.QueueEvent(new KillEnemyGameEvent(buildingName));
    }

    private void OnQuestCompleted(Quest quest)
    {
        questsContent.GetChild(CurrentQuests.IndexOf(quest)).Find("Checkmark").gameObject.SetActive(true);
    }
}
