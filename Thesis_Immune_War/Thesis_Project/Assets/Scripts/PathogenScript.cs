using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PathogenScript : MonoBehaviour {

    //HP, ATK, ID
    public int HP;
    public int money;
    public int ID;
    //Transform endPoint;
    NavMeshAgent agent;
    public List<Transform> targetlist;
    List<Transform> mylist;
    public float pathoSpeed;
    Animation VarusAnima;

    void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = pathoSpeed;
        mylist = new List<Transform>();
        foreach (Transform t in targetlist)
        {
            mylist.Add(t);
        }
        money = HP;
        //endPoint = GameObject.Find("SpineStage").transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (mylist.Count == 0)
        {
            //Debug.Log("I am in the end");
            Destroy(gameObject);
            GameManager.GM.livePathoNum--;
        }
        else
        {
            agent.destination = mylist[0].position;
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       if( collision.gameObject.tag == "Organs")
        {
            mylist.Remove(collision.gameObject.transform);
        }
        if (collision.gameObject.tag == "Antigen")
        {
            HP = HP-GameManager.GM.BcellDamge;
            if (HP>0)
            {
                gameObject.transform.localScale = 0.8f * transform.localScale;
            }
            else
            {
                Debug.Log("!!!");
                GameManager.GM.BodyImmunity += money;
                Destroy(gameObject);
                GameManager.GM.livePathoNum--;

            }
        }

        if (collision.gameObject.tag == "Neutrophil")
        {
            HP--;
            if (HP>0)
            {
                gameObject.transform.localScale = 0.8f * transform.localScale;
            }
            else
            {
                GameManager.GM.BodyImmunity += money;
                Destroy(gameObject);
                GameManager.GM.livePathoNum--;
            }
        }

        if (collision.gameObject.tag == "SuperAntigen")
        {
            HP = HP-2;
            if (HP > 0)
            {
                gameObject.transform.localScale = 0.8f * transform.localScale;
            }
            else
            {
                GameManager.GM.BodyImmunity += money;
                Destroy(gameObject);
                GameManager.GM.livePathoNum--;
            }
        }
    }
}
