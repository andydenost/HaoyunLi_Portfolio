using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {//should be a singleton

    public static WaveManager WM;
    public GameObject pathogenSpawnerA;
    public GameObject pathogenSpawnerB;
    bool hasStartB;
    bool hasStartA;
    //int waveALength;
    public bool finallevel;
    public float timer;
    public float timeStartGame;
    


    //public int PathogenNum;

    private void Awake()
    {
        if (WM == null)
        {
            WM = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        pathogenSpawnerA.GetComponent<PathogenSpawner>().isStart = false;
        pathogenSpawnerB.GetComponent<PathogenSpawner>().isStart = false;
        hasStartB = false;
        hasStartA = false;
        finallevel = false;
        timer = 0;
        //waveALength = pathogenSpawnerA.GetComponent<PathogenSpawner>().waves.Length;
    }

    // Update is called once per frame
    void Update () {
        if (GameManager.GM.isGameStart == true && hasStartA == false)
        {
            
            timer += Time.deltaTime;
            if (timer >= timeStartGame )
            {
                pathogenSpawnerA.GetComponent<PathogenSpawner>().isStart = true;
                hasStartA = true;

            }
        }

        if (pathogenSpawnerA.GetComponent<PathogenSpawner>().AWavefinsh == true && hasStartB == false)
        {  
            StartSpawnerB();
            hasStartB = true;
        }

        if (finallevel == true)
        {
            StartSpawnerFinal();
            finallevel = false;
        }
    }

    void StartSpawnerB()
    {
        Debug.Log("come on B");
        pathogenSpawnerB.GetComponent<PathogenSpawner>().isStart = true;
    }

    void StartSpawnerFinal()
    {
        Debug.Log("come on final");
        pathogenSpawnerA.GetComponent<PathogenSpawner>().finalStart = true;
        pathogenSpawnerB.GetComponent<PathogenSpawner>().finalStart = true;
    }
}
