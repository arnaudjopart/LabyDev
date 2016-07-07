using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour {


    #region Public and Protected Members 
    public AudioClip m_backgroundMusic;
    public AudioClip m_ambience;
    public AudioClip m_paAnnounce;
    public AudioClip m_paAnnounce1;
    public AudioClip m_paAnnounce2;
    //public AudioClip m_paAnnounce3;
    public static AudioClip m_deathSound;
    #endregion

    #region Main Methdos
    void Awake()
    {
        var audioSources = gameObject.GetComponents<AudioSource>();
        
        m_Audio1 = audioSources[ 0 ];
        m_Audio2 = audioSources[ 1 ];
        m_Audio3 = audioSources[ 2 ];
        m_Audio4 = audioSources[ 3 ];
        m_Audio5 = audioSources[ 4 ];
        m_Audio6 = audioSources[ 5 ];
        m_playerPos = gameObject.GetComponent<Transform>();
    }

    void Start()
    {
        elapsedTime =0f;
        m_Audio1.volume=0.1f;
        m_Audio1.Play();
        m_Audio1.loop=true;
        m_Audio2.volume=0.15f;
        m_Audio2.Play();
        m_Audio2.loop=true;
       
        pos = new Vector3( m_playerPos.position.x, m_playerPos.position.y, m_playerPos.position.z );
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        
        pos = m_playerPos.position;
        if( elapsedTime > 20 )
        {

            switch( paEffectNumber )
            {
                case 0:
                    if( !m_Audio3.isPlaying )
                        m_Audio3.volume=1.2f;
                        m_Audio3.PlayOneShot( m_paAnnounce );
                    elapsedTime = 0;
                    paEffectNumber = 1;
                    break;
                case 1:
                    if( !m_Audio4.isPlaying )
                    {
                        m_Audio4.volume=0.1f;
                        m_Audio4.PlayOneShot( m_paAnnounce1 );
                       
                    }
                    elapsedTime = 0;
                    paEffectNumber = 2;
                    break;

                case 2:
                    if( !m_Audio5.isPlaying )
                    {
                        m_Audio5.volume=0.1f;
                        m_Audio5.PlayOneShot( m_paAnnounce2 );

                    }
                    elapsedTime = 0;
                    paEffectNumber = 0;
                  
                    break;

                case 3:
                    //if( !m_Audio3.isPlaying )
                    //    m_Audio3.PlayOneShot( m_paAnnounce3 );
                    //elapsedTime = 0;
                    //paEffectNumber = 0;
                    break;

                default:
                    break;
            }
            if( GameManager.IsGameOver )
            {
                

            }
            
            
        }
          
        

    }

    public static void PlayDeathSound()
    {
        m_Audio6.Play();
        /*if( !m_Audio6.isPlaying )
        {
            print( "Meurs !!!" );
            m_Audio6.volume = 1f;
            

        }*/
    }
    
    #endregion

    #region Utils

    #endregion

    #region Private Members
    private AudioSource m_Audio1;
    private AudioSource m_Audio2;
    private AudioSource m_Audio3;
    private AudioSource m_Audio4;
    private AudioSource m_Audio5;
    public static AudioSource m_Audio6;
    

    private Vector3 pos;
    private float elapsedTime = 0f;
    private Transform m_playerPos;
    private int paEffectNumber = 0;
    #endregion

}
  

