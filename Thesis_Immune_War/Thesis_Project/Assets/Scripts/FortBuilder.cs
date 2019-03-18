using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortBuilder : MonoBehaviour {
    public Camera characterCamera;
    private GameObject fortCell;
    public GameObject BCellFort;
    public GameObject TCellFort;
    public GameObject MCellFort;
    public Vector3 direction1;
    public Vector3 direction2;
    public Vector3 direction3;
    public Vector3 direction4;
    public Vector3 fortPos;
    public float maxDistanceBuild;

    private int fortIndex;

    public int moneyB;
    public int moneyT;

    Ray ray;
    RaycastHit hit;
	// Use this for initialization
	void Start () {
        direction1 = new Vector3(1,0,1);
        direction2 = new Vector3(1,0,-1);
        direction3 = new Vector3(-1,0,-1);
        direction4 = new Vector3(-1,0,1);

    }

    // Update is called once per frame
    void Update () {

        
         fortIndex = gameObject.GetComponent<SwitchCharacter>().CharcaterCount;
         ray = SightRayManager.Sight.ray;
         if (Physics.Raycast(ray, out hit))
         {
            if (Vector3.Distance(hit.transform.position,transform.position) < maxDistanceBuild)//only build when around fort
            {
                if (Input.GetKeyDown("e"))
                {
                    if (fortIndex == 0)
                    {
                        if (hit.collider.name == "Fort1" && GameManager.GM.BodyImmunity >= moneyB)
                        {
                            GameObject readyEffect = hit.collider.transform.GetChild(0).gameObject;// if ready effect is active then player could build the fort
                            if (readyEffect.activeSelf == true)
                            {
                                fortPos = hit.collider.transform.position;
                                BuildFort(fortIndex, fortPos, direction1);
                                readyEffect.SetActive(false);
                            }
                        }
                        if (hit.collider.name == "Fort2" && GameManager.GM.BodyImmunity >= moneyB)
                        {
                            GameObject readyEffect = hit.collider.transform.GetChild(0).gameObject;// if ready effect is active then player could build the fort
                            if (readyEffect.activeSelf == true)
                            {
                                fortPos = hit.collider.transform.position;
                                BuildFort(fortIndex, fortPos, direction2);
                                readyEffect.SetActive(false);
                            }
                        }
                        if (hit.collider.name == "Fort3" && GameManager.GM.BodyImmunity >= moneyB)
                        {
                            GameObject readyEffect = hit.collider.transform.GetChild(0).gameObject;// if ready effect is active then player could build the fort
                            if (readyEffect.activeSelf == true)
                            {
                                fortPos = hit.collider.transform.position;
                                BuildFort(fortIndex, fortPos, direction3);
                                readyEffect.SetActive(false);
                            }
                        }
                        if (hit.collider.name == "Fort4" && GameManager.GM.BodyImmunity >= moneyB)
                        {
                            GameObject readyEffect = hit.collider.transform.GetChild(0).gameObject;// if ready effect is active then player could build the fort
                            if (readyEffect.activeSelf == true)
                            {
                                fortPos = hit.collider.transform.position;
                                BuildFort(fortIndex, fortPos, direction4);
                                readyEffect.SetActive(false);
                            }
                        }
                    }
                }
                if (Input.GetKeyDown("r"))
                {
                    if (fortIndex == 1)
                    {
                        if (hit.collider.name == "TCellFortQuad")
                        {
                            GameObject readyEffect = hit.collider.transform.GetChild(0).gameObject;// if ready effect is active then player could build the fort
                            if (readyEffect.activeSelf == true)
                            {
                                fortPos = hit.collider.transform.position;
                                BuildFort(fortIndex, fortPos, Vector3.left);
                                readyEffect.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
	}

    void BuildFort(int index, Vector3 pos, Vector3 dir)
    {
        if (index == 0)
        {
            if (GameManager.GM.BodyImmunity>=moneyB)
            {
                fortCell = BCellFort;
                pos.y = pos.y + 0.8f;
                GameObject f = Instantiate(fortCell, pos, Quaternion.LookRotation(dir, Vector3.up));
                GameManager.GM.BodyImmunity -= moneyB;
                f.transform.SetParent(hit.transform);
            }
        }

        else if(index == 1){
            
            if (GameManager.GM.BodyImmunity >= moneyT)
            {
                fortCell = TCellFort;
                pos.y = pos.y + 0.6f;
                GameObject f = Instantiate(fortCell, pos, Quaternion.LookRotation(dir, Vector3.up));
                f.transform.SetParent(hit.transform);
                f.GetComponent<GuardAntiAttack>().immunity = moneyT;
                GameManager.GM.BodyImmunity -= moneyT;
                
            }
        }
    }
}
