using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCell : MonoBehaviour {

    public GameObject goBullet;
    private GameObject bullet;
    private float timmer;
    public float shootingInterval;
    // Use this for initialization
    void Start()
    {
        timmer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timmer += Time.deltaTime;
        
        if (Input.GetButtonDown("Fire1"))
        {
            if (timmer > shootingInterval)
            {
                if (GameManager.GM.BodyImmunity >= 1)
                {
                    bullet = Instantiate(goBullet);
                    bullet.transform.position = transform.position;
                    GameManager.GM.BodyImmunity--;
                }

                //bullet.transform.parent = this.transform;
                //bullet.transform.SetParent(this.transform);
                timmer = 0;
            }
            
        }
        
    }
}
