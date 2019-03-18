using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {//should be a singleton

    // public GameObject player;
    //
    // Use this for initialization

    public static GameManager GM { get; private set; }
    public bool isGameStart;
    public int livePathoNum;
    public int BcellDamge;
    public int BodyImmunity;
    public int BodyHealth;
    public Slider healthSlider;
    public Text immunityText;
    public bool finalHasDone;

    private void Awake()
    {
        if (GM == null)
        {
            GM = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start () {
        //Vector3 startPoint = new Vector3(0, 1, 0);
        //Instantiate(player, startPoint, Quaternion.identity);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        BcellDamge = 1;
        isGameStart = true;
        healthSlider.value = BodyHealth;
        
    }

    // Update is called once per frame
    void Update () {
        /*if (livePathoNum<0)
        {
            livePathoNum = 0;
        }*/
        healthSlider.value = BodyHealth;
        immunityText.text = "Immunity: "+BodyImmunity.ToString();
        Debug.Log("live: "+livePathoNum);
        //Debug.Log(BcellDamge);
      //  Debug.Log("health: "+BodyHealth);

        if (finalHasDone == true && livePathoNum == 0 && BodyHealth>0)
        {
            SceneManager.LoadScene("VictoryPage", LoadSceneMode.Single);
            Destroy(GameObject.Find("GameManager"));
            Destroy(GameObject.Find("WaveManager"));
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (BodyHealth<=0)
        {
            SceneManager.LoadScene("FailurePage", LoadSceneMode.Single);
            Destroy(GameObject.Find("GameManager"));
            Destroy(GameObject.Find("WaveManager"));
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
