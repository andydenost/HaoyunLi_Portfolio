using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicatePathogen : MonoBehaviour {
    public int HP;
    public int counter;
    public GameObject duplicatePathogen;
    public List<Transform> targetList;
    public int speed;
    public int mycounter;
    // Use this for initialization
    void Start () {
        counter = 0;
        mycounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pathogen")
        {
            mycounter++;
            counter++;
            if (counter == 4)
            {
                GameManager.GM.livePathoNum++;
                GameObject go = Instantiate(duplicatePathogen, transform.position, Quaternion.identity);
                go.GetComponent<PathogenScript>().HP = HP;
                go.transform.localScale = (1 + (HP / 4)) * go.transform.localScale;
                go.GetComponent<PathogenScript>().targetlist = targetList;
                go.GetComponent<PathogenScript>().pathoSpeed = speed;
                go.layer = 19;
                counter = 0;
                Debug.Log("get one");

                //go.GetComponent<PathogenScript>().ID = 2;
            }
            Debug.Log("mycounter： " + mycounter);
            Debug.Log("counter： " + counter);
        }
    }

}
