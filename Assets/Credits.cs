﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Credits : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnEndAnim()
    {
        SceneManager.LoadScene( 0 );
    }
}
