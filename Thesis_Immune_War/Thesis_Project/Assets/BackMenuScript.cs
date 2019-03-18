using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BackMenuScript : MonoBehaviour {

	// Use this for initialization
    public void backToMenu()
    {
        SceneManager.LoadScene("StartingPage", LoadSceneMode.Single);
    }
}
