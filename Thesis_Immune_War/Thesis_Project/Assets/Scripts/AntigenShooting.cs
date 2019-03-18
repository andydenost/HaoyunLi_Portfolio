using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntigenShooting : MonoBehaviour {

        private Vector3 shootingDirection;
        public float antiSpeed;
        private GameObject player;
        public float rotateSpeed;
        public Vector3 playerSpeed;
        private Ray ray;
        private RaycastHit hit;
        public GameObject explosionEffect;
        
        // Use this for initialization
        void Start()
        {
            player = GameObject.Find("Player");
            ray = SightRayManager.Sight.ray;
            if (Physics.Raycast(ray, out hit))
            {
                shootingDirection = Vector3.Normalize(hit.point - transform.position);
            }
            else
            {
                shootingDirection = player.transform.forward;
            }
            playerSpeed = player.GetComponent<PlayerControler>().playerVelocity;
           // Debug.Log("ps"+playerSpeed);
            gameObject.GetComponent<Rigidbody>().velocity = playerSpeed;
        // Debug.Log(gameObject.GetComponent<Rigidbody>().velocity);
            gameObject.transform.rotation = player.transform.rotation;
            gameObject.GetComponent<Rigidbody>().AddForce(shootingDirection * antiSpeed * 100);
           // Debug.Log(gameObject.GetComponent<Rigidbody>().velocity);
         }

    // Update is called once per frame
    void Update()
        {
           // antiRotate();
        }

       void antiRotate()
        {
        gameObject.transform.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.Self);

        }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        GameObject effect = Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(effect, 0.4f);

    }
}



