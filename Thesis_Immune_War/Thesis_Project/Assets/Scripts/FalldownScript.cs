using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalldownScript : MonoBehaviour {

    // Use this for initialization
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Body")
        {
            transform.position = new Vector3(39.51f, 2f -0.9f);
        }
    }
}
