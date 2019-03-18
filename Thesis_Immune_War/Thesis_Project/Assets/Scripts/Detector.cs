using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour {

    public bool inAttackRange;
	// Use this for initialization
	void Start () {
        inAttackRange = false;
	}
	
	// Update is called once per frame
	void Update () {
        //inAttackRange = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        inAttackRange = true;
    }
}
