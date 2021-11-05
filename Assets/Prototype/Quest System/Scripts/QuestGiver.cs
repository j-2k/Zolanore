using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] Quest quest;
    [SerializeField] GameObject marker;

    GameObject player;
    GameObject interact;

    GameObject questJournal;
    GameObject questInformation;
    GameObject acceptButton;

    CameraControllerMain cameraController;

    TestMovement testMovement;
    QuestManager questManager;
    QuestWindow questWindow;

    bool acceptedQuest = false;
    bool isClose = false;

    private void Awake()
    {
        cameraController = FindObjectOfType<CameraControllerMain>();
        testMovement = FindObjectOfType<TestMovement>();
        questManager = FindObjectOfType<QuestManager>();
        questWindow = FindObjectOfType<QuestWindow>();

        marker = transform.GetChild(0).Find("Image").gameObject;
        player       = GameObject.FindGameObjectWithTag("Player");
        acceptButton = GameObject.FindGameObjectWithTag("AcceptButton");
        interact     = GameObject.FindGameObjectWithTag("InteractPanel");
        questInformation = GameObject.FindGameObjectWithTag("QuestInformation");
        questJournal = GameObject.FindGameObjectWithTag("questContainer");
    }

    private void Start()
    {
        CloseQuestInfo();
        interact.SetActive(false);
        questJournal.SetActive(false);

    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < 5)
        {
            isClose = true;
            if (!acceptedQuest)
            {
                interact.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    DisableCharacterRotation();
                    Cursor.visible = true;
                    questManager.InitializeWindow(quest);
                }
            }
        }
        else
        {
            interact.SetActive(false);
        }

        if (acceptedQuest)
        {
            interact.SetActive(false);

            if (Input.GetKeyDown(KeyCode.J))
            {
                ActivateOrDisactivateQuestContainer();
            }
        }

        if(distance > 5 && !acceptedQuest)
        {
            CloseQuestInfo();
        }
    }

    void ActivateOrDisactivateQuestContainer()
    {
        if (questJournal.activeSelf == true)
        {
            EnableCharacterRotation();
            questInformation.SetActive(false);
            questJournal.SetActive(false);
        }
        else
        {
            DisableCharacterRotation();
            questJournal.SetActive(true);
        }
        
    }

    public void DisableCharacterRotation()
    {
        cameraController.enabled = false;
        testMovement.enabled = false;
    }
    public void EnableCharacterRotation()
    {
        cameraController.enabled = true;
        testMovement.enabled = true;
    }
    public void AcceptQuest()
    {
        acceptedQuest = true;

        marker.SetActive(false);

        EnableCharacterRotation();

        questWindow.CloseWindow();

        questInformation.SetActive(false);

        Destroy(acceptButton);
    }

    public void CloseQuestInfo()
    {
        if (questJournal.activeSelf)
        {
            questJournal.SetActive(false);
        }
        if (!acceptedQuest && isClose)
        {
            questWindow.CloseWindow();
            EnableCharacterRotation();
        }
        else
        {
            questWindow.CloseWindow();
            EnableCharacterRotation();
        }
        
    }
}