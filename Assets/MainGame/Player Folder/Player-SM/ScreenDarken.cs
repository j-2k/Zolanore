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
    PlayerManager pm;

    private void Start()
    {
        alpha = 0;
        pm = cm.gameObject.GetComponent<PlayerManager>();
    }

    void Update()
    {
        if (CharacterManager.isDead)
        {
            if (GameSceneLoader.GetCurrentSceneName() == GameSceneLoader.SceneEnum.BossRealm.ToString())
            {
                BossRespawnVoid();
            }
            else
            {
                StartCoroutine(Respawn());
            }
            
            colToChange = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            fadeImage.color = colToChange;
            if (alpha > 1)
            {
                alpha = 1;
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
        pm.DeadAirUpdate();
        if (alpha>1)
        {
            cm.gameObject.transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
            yield return new WaitForSeconds(1);
            cm.RespawnPlayer();
        }
    }

    IEnumerator BossRespawn()
    {
        pm.DeadAirUpdate();
        if (alpha > 1)
        {
            yield return new WaitForEndOfFrame();
            cm.RespawnPlayer();
            yield return new WaitForEndOfFrame();
            GameSceneLoader.LoadScene(GameSceneLoader.SceneEnum.BossRealm);
            GameSceneLoader.LoadScene(GameSceneLoader.SceneEnum.BossRealm);
        }
    }

    void BossRespawnVoid()
    {
        pm.DeadAirUpdate();
        if (alpha > 1)
        {
            cm.gameObject.transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
            cm.RespawnPlayer();
            GameSceneLoader.LoadScene(GameSceneLoader.SceneEnum.BossRealm);
        }
    }
}
