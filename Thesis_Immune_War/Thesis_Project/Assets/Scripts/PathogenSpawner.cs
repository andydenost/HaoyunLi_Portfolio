using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathogenSpawner : MonoBehaviour {

    public int waveNum;
    public Wave[] waves;
    public Wave[] finalWave;
    public Transform StartPoint;
    private List<Transform> targetList;
    public float interval;
    public bool isStart;
    public bool finalStart;
    public bool primWavefinish;
    public bool AWavefinsh;
    public bool finalhasDone;
    // Use this for initialization
    void Start() {
        StartPoint = transform;
        waveNum = 0;
        isStart = false;
        finalStart = false;
        primWavefinish = false;
        AWavefinsh = false;
        finalhasDone = false;
        //targetList = new List<Transform>();

    }

    // Update is called once per frame
    void Update() {
        if (isStart == true)
        {
            StartCoroutine(SpawnPathogen());
            isStart = false;
        }
        if (finalStart == true)
        {
            StartCoroutine(SpawnPathogenFinal());
            finalStart = false;
        }
    }

    IEnumerator SpawnPathogen()
    {
        foreach (Wave wave in waves)
        {
            GameManager.GM.livePathoNum += wave.count;
            targetList = new List<Transform>();

            foreach (Transform t in wave.routePoints)
            {
                targetList.Add(t);
            }
            Debug.Log(targetList);
            for (int i = 0; i<wave.count;i++)
            {

                GameObject go = Instantiate(wave.pathogenInfo.pathoPrefab, StartPoint.position, Quaternion.Euler(90f,90f,90f));
                go.GetComponent<PathogenScript>().HP = wave.pathogenInfo.HP;
                go.GetComponent<PathogenScript>().ID = wave.pathogenInfo.ID;

                go.transform.localScale =(1 + (wave.pathogenInfo.HP/4)) * go.transform.localScale;
                go.GetComponent<PathogenScript>().targetlist = targetList;
                go.GetComponent<PathogenScript>().pathoSpeed = wave.speed;
                yield return new WaitForSeconds(wave.rate);
            }

            Debug.Log("!!!!!!!!!wo zhi xing le ma?????????????");

            while (GameManager.GM.livePathoNum > 0)
            {
                //Debug.Log("wo zhi xing le ma");

                yield return 0;
            }
                Debug.Log("wo zhi xing le ma?????????????");
                yield return new WaitForSeconds(interval);
        }

        if (GameManager.GM.livePathoNum == 0)
        {
            Debug.Log(gameObject.name);
            if (gameObject.name == "PathogenSpawnerA")
            {
                AWavefinsh = true;
            }
            else if (gameObject.name == "PathogenSpawnerB")
            {
                WaveManager.WM.finallevel = true;
            }
        }
            
    }


    IEnumerator SpawnPathogenFinal()
    {
        foreach (Wave wave in finalWave)
        {
            // waveNum++;
            GameManager.GM.livePathoNum += wave.count;
            targetList = new List<Transform>();

            foreach (Transform t in wave.routePoints)
            {
                targetList.Add(t);
            }
            Debug.Log(targetList);
            for (int i = 0; i < wave.count; i++)
            {

                GameObject go = Instantiate(wave.pathogenInfo.pathoPrefab, StartPoint.position, Quaternion.Euler(90f, 90f, 90f));
                go.GetComponent<PathogenScript>().HP = wave.pathogenInfo.HP;
                go.transform.localScale = (1 + (wave.pathogenInfo.HP / 4)) * go.transform.localScale;
                go.GetComponent<PathogenScript>().targetlist = targetList;
                go.GetComponent<PathogenScript>().pathoSpeed = wave.speed;
                yield return new WaitForSeconds(wave.rate);
            }
            if (GameManager.GM.livePathoNum == 0)
            {
                yield return new WaitForSeconds(interval);
            }
        }
        GameManager.GM.finalHasDone = true;
    }
}
