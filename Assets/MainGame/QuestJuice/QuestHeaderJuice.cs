using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestHeaderJuice : MonoBehaviour
{
    public enum QuestState
    {
        Available,
        Active,
        Completed
    }

    public QuestState currentQuestState;

    [SerializeField] GameObject[] headerVFXs;

    [SerializeField] bool questTriggerTest = false;

    // Update is called once per frame
    void Update()
    {
        if (questTriggerTest)
        {
            QuestStatusCheck();
            questTriggerTest = false;
        }
    }

    void QuestStatusCheck()
    {
        if (currentQuestState == QuestState.Available)
        {
            foreach (GameObject vfx in headerVFXs)
            {
                vfx.SetActive(false);
                if (vfx.name == "AvailableQuestVFX")
                {
                    vfx.SetActive(true);
                }
            }
        }

        if (currentQuestState == QuestState.Active)
        {
            foreach (GameObject vfx in headerVFXs)
            {
                vfx.SetActive(false);
                Debug.Log("Active");
                if (vfx.name == "ActiveQuestVFX")
                {
                    vfx.SetActive(true);
                }
            }
        }

        if (currentQuestState == QuestState.Completed)
        {
            foreach (GameObject vfx in headerVFXs)
            {
                vfx.SetActive(false);
                if (vfx.name == "CompletedQuestVFX")
                {
                    vfx.SetActive(true);
                }
            }
        }
    }
}
