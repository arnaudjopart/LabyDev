using UnityEngine;
using System.Collections;

public class WinGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider _other)
    {
        print( "You win" );
        GameManager.m_player1Win = true;
        GameManager.GameOver();
    }
}
