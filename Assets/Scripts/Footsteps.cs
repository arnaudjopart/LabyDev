using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour {


    #region Public and Protected Members
    public Vector3 pos;
    public bool inputBool;
    public int i;
    public AudioClip m_step;
    public AudioClip m_step1;
    #endregion

    #region Main Methdos
    void Awake()
    {
        var audioSources = gameObject.GetComponents<AudioSource>();
        m_Audio= audioSources[ 0 ];
        m_Audio1 = audioSources[ 1 ];
        m_playerPos = gameObject.GetComponent<Transform>();
    }

    void Start()
    {
        StartCoroutine( PlayFootstep() );
        playSteps = true;
        pos = new Vector3( m_playerPos.position.x, m_playerPos.position.y, m_playerPos.position.z );
    }

    void Update()
    {
        pos = m_playerPos.position;
        inputBool =!playSteps && (Input.GetKey( "up" ) || Input.GetKey( "down" ) || Input.GetKey( "left" ) ||Input.GetKey( "right" ));


        if( playSteps )
        {
            playSteps = false;
            StopCoroutine( PlayFootstep() );
        }
        else if( inputBool )
        {
            i++;
            pos = m_playerPos.position;
            StartCoroutine( PlayFootstep() );
        }

    }
    #endregion

    #region Utils
    IEnumerator PlayFootstep()
    {

        yield return new WaitForSeconds( 0.1f );

        if( i%2 == 0 )
        {
            if( !m_Audio.isPlaying &&   !m_Audio1.isPlaying )
                m_Audio.PlayOneShot( m_step );
        }
        else
        {
            if( !m_Audio1.isPlaying &&  !m_Audio.isPlaying )
                m_Audio1.PlayOneShot( m_step1 );
        }
    }
    #endregion

    #region Private Members
    private AudioSource m_Audio;
    private AudioSource m_Audio1;
    private Transform m_playerPos;
    private bool playSteps;
    #endregion

}
  

