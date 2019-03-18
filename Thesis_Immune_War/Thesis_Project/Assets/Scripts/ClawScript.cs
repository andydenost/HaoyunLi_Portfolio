using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ClawScript : MonoBehaviour {

    public GameObject macrophage;

    public bool pathogenCathing = false;

    private float distance;

    public List<GameObject> pathogen;

    private int eatNum;

    // Use this for initialization

    private void Start()
    {
        eatNum = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pathogen")
        {
            if (collision.gameObject.GetComponent<PathogenScript>().HP == 1)
            {
                collision.gameObject.GetComponent<PathogenScript>().enabled = false;
                collision.gameObject.GetComponent<NavMeshAgent>().enabled = false;
                pathogen.Add(collision.gameObject);
                pathogenCathing = true;
                collision.gameObject.transform.parent = transform;
                collision.gameObject.layer = 12;
            }
        }
        macrophage.GetComponent<HookCatch>().clawMoveState = 1;

    }

    private void Update()
    {
        distance = Vector3.Distance(transform.position, macrophage.transform.position);
        //Debug.Log("d "+distance);

        if (distance < 0.01 && pathogenCathing==true)
        {
            foreach (GameObject p in pathogen.ToArray())
            {
                pathogen.Remove(p);
                Destroy(p);
                GameManager.GM.livePathoNum--;
                eatNum++;

            }

            pathogenCathing = false;
        }
        if (eatNum == 10)
        {
            GameManager.GM.BcellDamge = 2;
        }
        if (eatNum == 30)
        {
            GameManager.GM.BcellDamge = 3;
        }


    }
}
