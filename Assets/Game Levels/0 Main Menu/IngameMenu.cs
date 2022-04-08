using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IngameMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] AudioSource bgm;
    [SerializeField] Slider volumeSlider;
    [SerializeField] MainMenuManager mmm;
    CameraControllerMain cam;

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

    void PauseGame()
    {
        if (gameIsPaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            menu.SetActive(true);
            AudioListener.pause = true;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            ApplyAllChanges();
            menu.SetActive(false);
            AudioListener.pause = false;
            Time.timeScale = 1;
        }

    }

    public void ApplyVolume()
    {
        bgm.volume = volumeSlider.value;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
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
}
