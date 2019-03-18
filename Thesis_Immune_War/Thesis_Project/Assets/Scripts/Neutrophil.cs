using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Neutrophil : MonoBehaviour {

    public float detectRadius;
    private float nearestDistance;
    private GameObject nearestEnemy;
    NavMeshAgent agent;
    public GameObject neuExpEffect;

    // Use this for initialization
    void Start () {
        nearestDistance = detectRadius;
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        Collider[] enemyCollider = Physics.OverlapSphere(transform.position,detectRadius);
        foreach(Collider em in enemyCollider)
        {
            if (em.gameObject.tag == "Pathogen")
            {
                float newDistance = Vector3.Distance(transform.position, em.transform.position);
                if (newDistance < nearestDistance)
                {
                    nearestDistance = newDistance;
                    nearestEnemy = em.gameObject;
                }
            }
        }
        if (nearestEnemy != null)
        {
            agent.enabled = true;
            agent.destination = nearestEnemy.transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pathogen")
        {
            //damage pathogen
            Destroy(gameObject);
            GameObject effect = Instantiate(neuExpEffect, transform.position, transform.rotation);
            Destroy(effect, 0.4f);
        }
    }
}
