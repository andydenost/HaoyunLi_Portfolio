using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBodyStatus : MonoBehaviour {

    //public static HumanBodyStatus _humanBodyStatus;
    public static HumanBodyStatus humanBodyStatus { get; private set; }

    public int bodyHealth;
    private  int bodyImmunityIncrease;
    public int bodyImmunity;
    private float IHfactor;



    // Use this for initialization

    private void Awake()
    {
        if (humanBodyStatus == null )
        {
            humanBodyStatus = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float result = bodyHealth * IHfactor;
        bodyImmunityIncrease = (int)result;
        bodyImmunity += bodyImmunityIncrease;
	}
}
