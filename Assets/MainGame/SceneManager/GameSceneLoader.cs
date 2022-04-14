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
