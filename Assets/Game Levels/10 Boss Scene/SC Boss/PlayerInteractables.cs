using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractables : MonoBehaviour
{
    public bool isFocused = false;
    [SerializeField] bool isBossPortal = false;
    [SerializeField] KeyCode interact;
    [SerializeField] GameObject bossObject;
    [SerializeField] GameObject teleportLocation;

    // Update is called once per frame
    void Update()
    {

        if (isFocused && Input.GetKeyDown(interact))
        {
            if (isBossPortal)
            {
                StartCoroutine(BossCor());
                return;
            }
            else if (isBossAltar() == true)
            {
                return;
            }
            else
            {
                PortalsTP();
            }
        }
    }

    bool isBossAltar()
    {
        if (this.gameObject.name == "BOSSALTAR")
        {
            bossObject.SetActive(true);
            return true;
        }
        return false;
    }

    void PortalsTP()
    {
        PlayerManager.instance.gameObject.transform.position = teleportLocation.transform.position;
    }

    void BossPortal()
    {
        isFocused = false;
        CameraControllerMain.instance.interactUIPrompt.SetActive(false);
        GameSceneLoader.LoadScene(GameSceneLoader.SceneEnum.BossRealm);
    }

    IEnumerator BossCor()
    {
        GameSceneLoader.LoadScene(GameSceneLoader.SceneEnum.BossRealm);
        yield return new WaitForEndOfFrame();
        isFocused = false;
        CameraControllerMain.instance.interactUIPrompt.SetActive(false);
    }
}
