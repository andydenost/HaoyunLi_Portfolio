using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFortAntigen : MonoBehaviour {

    public float antiSpeed;
    public GameObject explosionEffect;
    private Transform target;
    

    public Transform Target
    {
        set
        {
            target = value;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            transform.LookAt(target.position);
            transform.Translate(Vector3.forward * antiSpeed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
            GameObject effect = Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(effect, 0.4f);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        GameObject effect = Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(effect, 0.4f);
    }

}
