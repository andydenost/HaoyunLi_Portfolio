using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapScript : MonoBehaviour {
    public Transform player;
    public Vector3 center;
    public GameObject minimap;
    public GameObject worldminimap;

    private void Start()
    {
        center = transform.position;
        minimap.SetActive(true);
        worldminimap.SetActive(false);

    }

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.M))
        {
            transform.position = center;
            this.GetComponent<Camera>().orthographicSize = 54f;
            worldminimap.SetActive(true);
            minimap.SetActive(false);
        }
        else
        {
            Vector3 newPosition = player.position;
            newPosition.y = transform.position.y;
            transform.position = newPosition;
            this.GetComponent<Camera>().orthographicSize = 40f;
            worldminimap.SetActive(false);
            minimap.SetActive(true);

        }

    }
}
