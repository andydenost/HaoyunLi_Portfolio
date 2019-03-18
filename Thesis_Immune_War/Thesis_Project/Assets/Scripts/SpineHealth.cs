using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineHealth : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pathogen")
        {
            GameManager.GM.BodyHealth = GameManager.GM.BodyHealth - collision.gameObject.GetComponent<PathogenScript>().HP;
            Debug.Log(GameManager.GM.BodyHealth);
        }
    }
}
