using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {
    public AudioClip audioClip;
    public AudioSource audioSource;
    // Use this for initialization
    void Start () {
        audioSource.clip = audioClip;
    }
	
	// Update is called once per frame
	void Update () {
        audioSource.Play();
    }
}
