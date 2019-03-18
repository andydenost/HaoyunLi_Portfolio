using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniIcon : MonoBehaviour {

    public Transform player;

    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, player.eulerAngles.y, transform.eulerAngles.z);
    }
}
