using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCodes : MonoBehaviour
{
    public static bool godMode = false;
    string[] godString = new string[] { "j","u", "m", "a", "i", "s", "h", "e", "r", "e"};
    int godIndex = 0;
    // Update is called once per frame
    void Update()
    {
        if (IngameMenu.gameIsPaused)
        {
            if(Input.anyKeyDown)
            {
                Debug.Log(godString.Length);
                if (Input.GetKeyDown(godString[godIndex]))
                {
                    godIndex++;
                }
                else
                {
                    godIndex = 0;
                }
            }

            if (godIndex == godString.Length)
            {
                godMode = !godMode;
                godIndex = 0;
            }
        }
    }
}
