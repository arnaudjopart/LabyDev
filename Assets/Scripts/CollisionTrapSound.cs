using UnityEngine;
using System.Collections;

public class CollisionTrapSound : MonoBehaviour {

    public AudioSource m_trapSound;
    public float m_timerTrap = 5f;
 
    
   
	void OnTriggerEnter(Collider _other)
    {

        
        
        if( _other.GetComponent<WallTriggeredByPlayer>() )

            LaunchTrapSound();
        
        if( _other.GetComponent<GasTriggeredByPlayer>() )
        
            LaunchTrapSound();
        
        if( _other.GetComponent<GroundTriggeredByPlayer>() )
        
            LaunchTrapSound();
        
        if( _other.GetComponent<TilesTriggerScript>() )
        
            LaunchTrapSound();
        
        if( _other.GetComponent<PyramideTriggerScript>())
        
            LaunchTrapSound();
        
        if( _other.GetComponent<FallingPlatTriggeredByPlayer>() )
        
            LaunchTrapSound();
        
        if( _other.GetComponent<FloorTriggeredByPlayer>() )
            LaunchTrapSound();

    
        

    }
    private void LaunchTrapSound()
    {
        if( Time.timeSinceLevelLoad > m_startTimer+m_timerTrap )
        {
            m_startTimer = Time.timeSinceLevelLoad;
            if( !m_trapSound.isPlaying ) m_trapSound.Play();

        }
        
        
        
        
    }

    private float m_startTimer=0;
}
