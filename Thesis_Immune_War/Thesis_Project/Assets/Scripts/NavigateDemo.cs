using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NavigateDemo : MonoBehaviour {

    // Use this for initialization

    public GameObject destinationPoint;


	void Start () {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = destinationPoint.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        
    }
}
