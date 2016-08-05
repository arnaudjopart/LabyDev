using UnityEngine;
using System.Collections;
using System;

public class VRInteractiveElement : MonoBehaviour {


    public event Action OnClickEvent;
    public event Action OnOverEvent;
    public event Action OnOutEvent;
    public event Action OnDoubleClickEvent;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnClick()
    {
        if(OnClickEvent!=null)
            OnClickEvent();
    }
    public void OnOver()
    {
        if( OnOverEvent != null )
            OnOverEvent();
    }
    public void OnOut()
    {
        if( OnOutEvent != null )
            OnOutEvent();
    }
    public void OnDoubleClick()
    {
        if( OnDoubleClickEvent != null )
            OnDoubleClickEvent();
    }
}
