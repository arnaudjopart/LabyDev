using UnityEngine;
using System.Collections;

public class CollisionTrapSound : MonoBehaviour {

    public AudioSource m_trapSound;
    public float m_timerTrap = 5f;
 
    
   
	void OnTriggerEnter(Collider _other)
    {

        elapsedTime += Time.deltaTime;
        
        if( _other.GetComponent<WallTriggeredByPlayer>() && (isTrapped || elapsedTime > m_timerTrap))
        
            LaunchTrapSound();
        
        if( _other.GetComponent<GasTriggeredByPlayer>() && (isTrapped || elapsedTime > m_timerTrap) )
        
            LaunchTrapSound();
        
        if( _other.GetComponent<GroundTriggeredByPlayer>() && (isTrapped || elapsedTime > m_timerTrap) )
        
            LaunchTrapSound();
        
        if( _other.GetComponent<TilesTriggerScript>() && (isTrapped || elapsedTime > m_timerTrap) )
        
            LaunchTrapSound();
        
        if( _other.GetComponent<PyramideTriggerScript>() && (isTrapped || elapsedTime > m_timerTrap) )
        
            LaunchTrapSound();
        
        if( _other.GetComponent<FallingPlatTriggeredByPlayer>() && (isTrapped || elapsedTime > m_timerTrap) )
        
            LaunchTrapSound();
        
        if( _other.GetComponent<FloorTriggeredByPlayer>() && (isTrapped || elapsedTime > m_timerTrap) )
            LaunchTrapSound();

    
        

    }
    private void LaunchTrapSound()
    {
        isTrapped = false;
        elapsedTime = 0f;
        m_trapSound.Play();
    }

    private float elapsedTime = 0;
    private bool isTrapped=true;
}
