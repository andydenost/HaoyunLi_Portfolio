using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave{ //persist each wave of enemies property
    public PathogenInfo pathogenInfo;
    public int count;//how many pathogens
    public float rate;//interval between the pathogens
    public Transform[] routePoints = new Transform[4];
    public float speed;
}
