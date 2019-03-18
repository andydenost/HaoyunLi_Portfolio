using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fork : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pathogen")
        {
            gameObject.GetComponentInParent<GuardAntiAttack>().hasAttack = true;
            gameObject.GetComponentInParent<GuardAntiAttack>().immunity = gameObject.GetComponentInParent<GuardAntiAttack>().immunity - 2;
        }
    }
}
