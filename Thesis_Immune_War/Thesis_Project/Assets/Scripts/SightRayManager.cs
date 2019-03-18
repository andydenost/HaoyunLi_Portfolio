using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightRayManager : MonoBehaviour {

    private Camera cam;
    public Ray ray;
    public RaycastHit hit;
    public static SightRayManager Sight { get; private set; }

    private void Awake()
    {
        if (Sight == null)
        {
            Sight = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
    }
}
