using UnityEngine;
using System.Collections;

public class CollisionTrapSound : MonoBehaviour {

    public AudioSource m_trapSound;

 
    
   
	void OnTriggerEnter(Collider _other)
    {
        
        print( "Collison "+_other.name );
        if( _other.GetComponent<WallTriggeredByPlayer>() && !isTrapped)
        {
            isTrapped = true;
            m_trapSound.Play();
        }
        if( _other.GetComponent<GasTriggeredByPlayer>() && !isTrapped )
        {
            isTrapped = true;
            m_trapSound.Play();
        }
        if( _other.GetComponent<GroundTriggeredByPlayer>() && !isTrapped )
        {
            isTrapped = true;
            m_trapSound.Play();
        }
        if( _other.GetComponent<TilesTriggerScript>() && !isTrapped )
        {
            isTrapped = true;
            m_trapSound.Play();
        }
        if( _other.GetComponent<PyramideTriggerScript>() && !isTrapped )
        {
            isTrapped = true;
            m_trapSound.Play();
        }
        if( _other.GetComponent<FallingPlatTriggeredByPlayer>() && !isTrapped )
        {
            isTrapped = true;
            m_trapSound.Play();
        }
        if( _other.GetComponent<FloorTriggeredByPlayer>() && !isTrapped )
        {
            isTrapped = true;
            m_trapSound.Play();
        }
        

    }

    private bool isTrapped;
}
