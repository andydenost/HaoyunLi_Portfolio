using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour {

    public GameObject instruPanel;
    public GameObject BackgroundPanel;
    public GameObject CreditsPanel;



    public void StartGame()
    {
        SceneManager.LoadScene("MainGame",LoadSceneMode.Single);

        Destroy(GameObject.Find("GameManager"));

        /*GameManager.GM.isGameStart = false;
        GameManager.GM.livePathoNum = 0;
        GameManager.GM.BcellDamge = 1;
        GameManager.GM.BodyImmunity = 50;
        GameManager.GM.BodyHealth = 100;*/

    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void InstructionShow()
    {
        gameObject.SetActive(false);
        instruPanel.SetActive(true);

    }
    public void BackgroundShow()
    {
        gameObject.SetActive(false);
        BackgroundPanel.SetActive(true);

    }

    public void CreditsShow()
    {
        gameObject.SetActive(false);
        CreditsPanel.SetActive(true);

    }

    public void InsBack()
    {
        gameObject.SetActive(true);
        instruPanel.SetActive(false);

    }

    public void BackgroundBack()
    {
        gameObject.SetActive(true);
        BackgroundPanel.SetActive(false);

    }

    public void CreditsBack()
    {
        gameObject.SetActive(true);
        CreditsPanel.SetActive(false);

    }

}
