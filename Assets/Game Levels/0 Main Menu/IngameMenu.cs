using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class IngameMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] AudioSource bgm;
    [SerializeField] MainMenuManager mmm;
    CameraControllerMain cam;

    [SerializeField] TMP_InputField mouseInputField;
    [SerializeField] Slider mouseSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;

    public static bool gameIsPaused;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CameraControllerMain>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }

    public void PauseGameButton()
    {
        gameIsPaused = !gameIsPaused;
        PauseGame();
    }

    void PauseGame()
    {
        if (gameIsPaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            menu.SetActive(true);
            AudioListener.pause = true;
            mouseInputField.text = mmm.mouseSensitivity.ToString();
            mouseSlider.value = mmm.mouseSensitivity;
            musicSlider.value = (mmm.musicVolume * 100);
            SFXSlider.value = (mmm.SFXVolume * 100);
            Time.timeScale = 0;
        }
        else
        {
            StartCoroutine(HideCursorDelay());
            ApplyAllChanges();
            menu.SetActive(false);
            AudioListener.pause = false;
            Time.timeScale = 1;
        }

    }

    public void BackToMainMenu()
    {
        CloseMenu();
        Time.timeScale = 1;
        AudioListener.pause = false;
        GameSceneLoader.LoadScene(GameSceneLoader.Scene.MainMenu);
    }

    public void CloseMenu()
    {
        ApplyAllChanges();
        menu.SetActive(false);
    }

    public void ApplyAllChanges()
    {
        cam.SetSens(mmm.mouseSensitivity);
        bgm.volume = mmm.musicVolume;
    }

    IEnumerator HideCursorDelay()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForEndOfFrame();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
