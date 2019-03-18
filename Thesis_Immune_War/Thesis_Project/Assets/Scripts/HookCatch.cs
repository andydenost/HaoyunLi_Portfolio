using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookCatch : MonoBehaviour {

    public GameObject player;
    public GameObject claw;
    public GameObject arm;
    private Vector3 catchDirection;
    public float clawSpeed;
    private float armLength;
    public float MaxLength;
    public int clawMoveState;//move forward or backward, 0 for forward, 1 for backward.
    private bool catchReady;
    private Vector3 curClawDirection;
    private float maxZlengthtoPlayer;
    private Camera cam;
    Ray ray;
    RaycastHit hit;
    
	// Use this for initialization
	void Start () {
        catchReady = true;
        arm.SetActive(false);//make it without blackhole if the arm length is 0, so just make it disappear
        cam = GameObject.Find("CharacterCamera").GetComponent<Camera>();
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        ray = SightRayManager.Sight.ray;
        //catchDirection = Vector3.forward;//get players front direction.
        if (Input.GetMouseButtonDown(0) && catchReady == true)
        {
            //lock moving
            player.GetComponent<PlayerControler>().enabled = false;
            ray = SightRayManager.Sight.ray;
            if (Physics.Raycast(ray, out hit))
            {
                catchDirection = Vector3.Normalize(hit.point - transform.position);
            }
            else
            {
                float camY = cam.transform.localPosition.y;
                Debug.Log("camY" + camY);
                maxZlengthtoPlayer = Mathf.Sqrt(Mathf.Pow(MaxLength, 2) - Mathf.Pow(camY, 2));
                Debug.Log("maxZlen" + maxZlengthtoPlayer);

                catchDirection = new Vector3(0, camY, maxZlengthtoPlayer);
                catchDirection = transform.rotation * catchDirection;
                catchDirection = catchDirection.normalized;

                Debug.Log("CatchDirection" + catchDirection);
            }
            catchReady = false;
        }
        if (catchReady == false)
        {
            arm.SetActive(true);
            clawMove(catchDirection, clawMoveState);
        }
	}

    void clawMove(Vector3 direction, int state)
    {
        if (state == 0)
        {
            claw.transform.Translate(direction * clawSpeed * Time.deltaTime,Space.World);
            armLength = Vector3.Distance(this.transform.localPosition, claw.transform.localPosition);
            // arm.transform.localPosition = new Vector3(0, 0, armLength / 2);
            arm.transform.localPosition = claw.transform.localPosition / 2;
            arm.transform.LookAt(claw.transform.position);
            arm.transform.Rotate(90, 0, 0, Space.Self);
            Vector3 armScale = arm.transform.localScale;
            armScale.y = armLength / 2;
            arm.transform.localScale = armScale;
            if (armLength >= MaxLength)
            {
                clawMoveState = 1;
            }
        }
        else if(state == 1 )
        {
            claw.transform.Translate(-direction * clawSpeed * Time.deltaTime, Space.World);
            armLength = Vector3.Distance(this.transform.localPosition, claw.transform.localPosition);
            //arm.transform.localPosition = new Vector3(0, 0, armLength / 2);
            arm.transform.localPosition = claw.transform.localPosition / 2;
            arm.transform.LookAt(claw.transform.position);
            arm.transform.Rotate(90, 0, 0, Space.Self);
            Vector3 armScale = arm.transform.localScale;
            armScale.y = armLength / 2;
            arm.transform.localScale = armScale;
            curClawDirection = claw.transform.localPosition - Vector3.zero;
            if (curClawDirection == Vector3.zero || curClawDirection.z < 0 )
            {
                armScale.y = 0;
                arm.transform.localScale = armScale;
                claw.transform.localPosition = Vector3.zero;
                clawMoveState = 0;
                Debug.Log("!!");
                catchReady = true;
                arm.SetActive(false);
                //unlock moving
                player.GetComponent<PlayerControler>().enabled = true;

            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
            Debug.Log("bad");

        Collider myCollider = collision.contacts[0].thisCollider;
        if(myCollider.gameObject.name == "claw")
        {
            Debug.Log("good");
            clawMoveState = 1;
        }
    }

}
