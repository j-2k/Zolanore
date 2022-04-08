using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenDarken : MonoBehaviour
{
    [SerializeField] Image fadeImage;

    [SerializeField] Color colToChange;
    public float alpha;

    [SerializeField] CharacterManager cm;

    void Update()
    {
        if (CharacterManager.isDead)
        {
            colToChange = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            fadeImage.color = colToChange;
            if (alpha > 1)
            {
                alpha = 1;
                cm.gameObject.transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
                StartCoroutine(Respawn());
            }
            else
            {
                alpha += Time.deltaTime * 0.25f;
            }
        }
        else
        {
            colToChange = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            fadeImage.color = colToChange;
            if (alpha > 0)
            {
                alpha -= Time.deltaTime * 0.25f;
            }
            else
            {
                alpha = 0;
                gameObject.SetActive(false);
            }
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1);
        cm.RespawnPlayer();
    }
}
