using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameSceneLoader
{

    public enum Scene
    {
        MainMenu,
        ZolanoreRealm,
        BossRealm,
        Loading
    }

    public static void LoadScene(Scene scene)
    {

        onLoaderCallBack = () =>
        {
            BGM.instance.SwitchAudioBGM((int)scene);
            if (scene == Scene.MainMenu)
            {
                PlayerManager.instance.gameObject.SetActive(false);
                CameraControllerMain.instance.gameObject.SetActive(false);
                PlayerFamiliar.instance.gameObject.SetActive(false);
                CanvasSingleton.instance.gameObject.SetActive(false);
            }
            else
            {
                PlayerManager.instance.gameObject.SetActive(true);
                CameraControllerMain.instance.gameObject.SetActive(true);
                PlayerFamiliar.instance.gameObject.SetActive(true);
                CanvasSingleton.instance.gameObject.SetActive(true);
            }
            SceneManager.LoadScene(scene.ToString());
        };

        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    static Action onLoaderCallBack;

    public static void LoadCallback()
    {
        if (onLoaderCallBack != null)
        {
            onLoaderCallBack();
            onLoaderCallBack = null;
        }
    }

    

}
