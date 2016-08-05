using UnityEngine;
using System.Collections;
using System;

public class VRInput : MonoBehaviour {

    public event Action OnClickEvent;
    public event Action OnDoubleClickEvent;

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if( Input.GetMouseButtonDown( 0 ) )
        {
            if( OnClickEvent != null )
            {
                OnClickEvent();
            }
        }
    }
}