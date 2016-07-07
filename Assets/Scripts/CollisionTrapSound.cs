using UnityEngine;
using System.Collections;

public class CollisionTrapSound : MonoBehaviour {

    public AudioSource m_trapSound;
    
   
	void OnTriggerEnter(Collider _other)
    {
        m_trapSound.Play();
    }
}
