using UnityEngine;
using System.Collections;

public class WinGame : MonoBehaviour {
    public AudioSource m_winSound;
	// Use this for initialization
	void Start () {
        m_winSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider _other)
    {
        m_winSound.Play();
        print( "You win" );
        NetworkManager.m_instance.SendPlayerWin();
        GameManager.m_player1Win = true;
        GameManager.GameOver();
    }

   
}
