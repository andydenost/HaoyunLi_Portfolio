using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class GuardAntiAttack : MonoBehaviour {
    public float detectRadius;
    private float nearestDistance;
    private GameObject nearestEnemy;
    Animation T_Animation;
    NavMeshAgent agent;
    //Collider[] enemyCollider;
    GameObject guardPoint;
    //public float attackRadius;
    private bool hasEnemy;
    private bool isEffectiveAttack;
    int enemycount;
    GameObject myDetector;
    public bool hasAttack;
    public int immunity;

    // Use this for initialization
    void Start () {
        T_Animation = gameObject.GetComponentInChildren<Animation>();
       // nearestDistance = detectRadius;
        agent = GetComponent<NavMeshAgent>();
        //agent.enabled = false;
        guardPoint = GameObject.Find("TCellFortQuad");
        hasEnemy = false;
        isEffectiveAttack = false;
        gameObject.transform.GetChild(0).GetComponent<Collider>().enabled = false;
        myDetector = gameObject.transform.Find("AttackDetector").gameObject;
        hasAttack = false;
    }

    // Update is called once per frame
    void Update () {
        
        nearestDistance = detectRadius;
        enemycount = 0;
        hasEnemy = false;
        Collider[] enemyCollider = Physics.OverlapSphere(transform.position, detectRadius);
        gameObject.transform.GetChild(0).GetComponent<Collider>().enabled = false;
        foreach (Collider em in enemyCollider)
            {
                if (em.gameObject.tag == "Pathogen")
                {
                    enemycount++;
                    hasEnemy = true;
                    float newDistance = Vector3.Distance(transform.position, em.transform.position);
                    if (newDistance < nearestDistance)
                    {
                        nearestDistance = newDistance;
                        nearestEnemy = em.gameObject;
                    Debug.Log("nearest enemy "+nearestEnemy);
                    }
                }
            }
        if (hasEnemy == false)
        {
           nearestEnemy = null;
           //nearestDistance = detectRadius;

        }

        if (nearestEnemy != null)
        {
            //agent.enabled = true;
            agent.destination = nearestEnemy.transform.position;
            transform.LookAt(nearestEnemy.transform);
            if (myDetector.GetComponent<Detector>().inAttackRange == true)
            {
                T_Animation.Play("Forkattack");
            }
            if (T_Animation["Forkattack"].time < 0.68f * T_Animation["Forkattack"].clip.length && T_Animation["Forkattack"].time > 0.4f * T_Animation["Forkattack"].clip.length)
            {
                if (hasAttack != true)
                {
                    isEffectiveAttack = true;
                    gameObject.transform.GetChild(0).GetComponent<Collider>().enabled = true;
                }
                else
                {
                    gameObject.transform.GetChild(0).GetComponent<Collider>().enabled = false;
                }

            }
            else
            {
                myDetector.GetComponent<Detector>().inAttackRange = false;
                isEffectiveAttack = false;
                gameObject.transform.GetChild(0).GetComponent<Collider>().enabled = false;
                hasAttack = false;
            }

        }
        else
        {
                agent.destination = guardPoint.transform.position;
        }
        if (immunity <= 0)
        {
            Debug.Log("immunity: " + immunity);
            agent.enabled = false;
            Destroy(gameObject);
            GameObject effect = transform.parent.Find("ReadyEffect").gameObject;
            effect.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isEffectiveAttack == true && collision.gameObject.tag == "Pathogen")
        {
            
        }
    }

}
