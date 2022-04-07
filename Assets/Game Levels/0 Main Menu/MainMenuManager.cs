using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class MainMenuManager : MonoBehaviour
{
    public float mouseSensitivity;
    public float musicVolume;
    public float SFXVolume;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        Debug.Log("New Game");
    }

    public void LoadGame()
    {
        Debug.Log("Load Game");
    }

    public void QuitGame()
    {
        Debug.Log("Closing Application");
        //Application.Quit();
    }

    List<int> widths = new List<int>() { 1920, 1600, 1280, 2560 };
    List<int> heights = new List<int>() { 1080, 900, 720, 1440 };

    public void SetScreenResolution(int index)
    {
        bool fullscreen = Screen.fullScreen;
        int width = widths[index];
        int height = heights[index];
        Screen.SetResolution(width,height,fullscreen);
    }

    public void WindowToggle(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetMouseSensitivity(TMP_InputField mouseInputField)
    {
        mouseSensitivity = Mathf.Abs(float.Parse(mouseInputField.text));
    }

    public void SetMusicVolume(Slider sliderValue)
    {
        musicVolume = sliderValue.value/sliderValue.maxValue;
    }

    public void SetSFXVolume(Slider sliderValue)
    {
        SFXVolume = sliderValue.value/sliderValue.maxValue;
    }
}
