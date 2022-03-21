using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] AudioSource bgm;
    [SerializeField] Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeSelf)
            {
                menu.SetActive(false);
            }
            else
            {
                menu.SetActive(true);
            }
        }
    }

    public void ApplyVolume()
    {
        bgm.volume = volumeSlider.value;
    }
}
