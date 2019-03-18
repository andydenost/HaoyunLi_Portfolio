using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCellFort : MonoBehaviour {

    //public float detectorRadius;
    public GameObject BFAPrefab;
    private GameObject BFAnti;
    //private Vector3 antiDirection;
    //public float antiSpeed;
    public List<GameObject> enemies;
    public float attackInterval;
    private float timer;
    public int immunity;

    // Use this for initialization
    void Start () {
        enemies = new List<GameObject>();
        attackInterval = 1f;
        timer = 0;
        immunity = 10;
    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        foreach (GameObject em in enemies.ToArray())//detect whether outside the range
        {
            if (em != null)
            {
                if (Vector3.Angle(transform.forward, em.transform.position - transform.position) > 45)
                {
                    enemies.Remove(em.gameObject);
                    Debug.Log(enemies.Count);
                }
            }
        }
        //.Log(timer);

        if (timer >= attackInterval && enemies.Count > 0 && immunity > 0)
        {
            Debug.Log(Time.realtimeSinceStartup);
            if (enemies[0]!=null)
            {
                attackPathogen(enemies[0].transform);
            }
            else
            {
                enemies.Remove(enemies[0]);
            }
            timer = 0;
        }else if (immunity == 0)
        {
            //enemies.Remove(gameObject);
            Destroy(gameObject);
            GameObject effect = transform.parent.Find("ReadyEffect").gameObject;
            effect.SetActive(true);
        }

        

    }

    private void OnTriggerEnter(Collider other) //detect whether inside the range
    {
        if (other.tag == "Pathogen")
        {
            if (Vector3.Angle(transform.forward, other.transform.position - transform.position) <= 45)
            {
                enemies.Add(other.gameObject);
                Debug.Log(enemies.Count);

            }
        }
    }

    private void OnTriggerExit(Collider other) //detect whether outside the range
    {
        if (other.tag == "Pathogen")
        {
            enemies.Remove(other.gameObject);
            Debug.Log(enemies.Count);
        }
    }

    void attackPathogen(Transform target)
    {
        immunity--;
        BFAnti = Instantiate(BFAPrefab, transform.position, transform.rotation);
        BFAnti.GetComponent<BFortAntigen>().Target = target;
    }

}
