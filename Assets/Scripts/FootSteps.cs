using UnityEngine;
using System.Collections;

public class FootSteps : MonoBehaviour {

    #region Public and Protected Members 
    public AudioClip m_steps;
 
    #endregion

    #region Main Methdos
    void Awake()
    {
        var audioSources = gameObject.GetComponents<AudioSource>();
        m_Audio= audioSources[ 0 ];
      
        m_playerPos = gameObject.GetComponent<Transform>();
    }

    void Start()
    {
       
        playSteps = true;
        pos = new Vector3( m_playerPos.position.x, m_playerPos.position.y, m_playerPos.position.z );
    }

    void Update()
    {
    
        pos = m_playerPos.position;
        isGetKeyDown =!playSteps && (Input.GetKeyDown( "up" ) || Input.GetKeyDown( "down" ) || Input.GetKeyDown( "left" ) ||Input.GetKeyDown( "right" ));
        isKeyAllDirections=Input.GetKeyUp( "up" )||Input.GetKeyUp( "down" )||Input.GetKeyUp( "left" )||Input.GetKeyUp( "right" );

        //print( isKeyAllDirections );
       

        if( isKeyAllDirections )
        {
            m_Audio.Pause();
            isKeyAllDirections=false;
        }


        if( playSteps )
        {
            playSteps = false;
            StopCoroutine( PlayFootstep() );
        }
        else if( isGetKeyDown )
        {
            StartCoroutine( PlayFootstep() );
        }

    }
    #endregion

    #region Utils
    IEnumerator PlayFootstep()
    {

        yield return new WaitForSeconds( 0.4f );
        if( !m_Audio.isPlaying )
            m_Audio.Play();

    }
    #endregion

    #region Private Members
    private AudioSource m_Audio;
    private Vector3 pos;
    private Transform m_playerPos;
    private bool playSteps;
    private bool isGetKeyDown=false;
    private bool isKeyAllDirections=false;
   
    #endregion
}
