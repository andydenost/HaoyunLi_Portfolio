using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAttack : MonoBehaviour {
    private bool isEffectiveAttack;
    Animation T_Animation;

    // Use this for initialization
    void Start () {
        T_Animation = gameObject.GetComponent<Animation>();
        gameObject.GetComponent<Collider>().enabled = false;
        isEffectiveAttack = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && GameManager.GM.BodyImmunity >=2 )
        {
            GameManager.GM.BodyImmunity -= 2;
            T_Animation.Play("Forkattack");
            T_Animation["Forkattack"].speed = 1;
        }
        if (T_Animation["Forkattack"].time < 0.68f * T_Animation["Forkattack"].clip.length && T_Animation["Forkattack"].time > 0.4f * T_Animation["Forkattack"].clip.length)
        {
            isEffectiveAttack = true;
            gameObject.GetComponent<Collider>().enabled = true;
        }
        else
        {
            isEffectiveAttack = false;
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    { 
        if (isEffectiveAttack == true && collision.gameObject.tag == "Pathogen")
        {
            Debug.Log("Damage!!!!!!!!!");

        }
    }
}

