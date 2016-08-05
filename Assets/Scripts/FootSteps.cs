using UnityEngine;
using System.Collections;

public class FootSteps : MonoBehaviour {

    #region Public and Protected Members 
    //public AudioClip m_steps1;
    //public AudioClip m_steps2;
    //public AudioClip m_steps3;
    //public AudioClip m_steps4;
    //public AudioClip m_steps5;
    //public AudioClip m_steps6;
    //public AudioClip m_steps7;

    #endregion

    #region Main Methdos
    void Awake()
    {
        var audioSources = gameObject.GetComponents<AudioSource>();
        m_Audio1= audioSources[ 0 ];
        m_Audio2= audioSources[ 1 ];
        m_Audio3= audioSources[ 2 ];
        m_Audio4= audioSources[ 3 ];
        m_Audio5= audioSources[ 4 ];
        m_Audio6= audioSources[ 5 ];
        m_Audio7= audioSources[ 6 ];



        m_playerPos = gameObject.GetComponent<Transform>();
    }

    void Start()
    {
       
        playSteps = true;
        pos = new Vector3( m_playerPos.position.x, m_playerPos.position.y, m_playerPos.position.z );
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        pos = m_playerPos.position;
        isGetKeyDown =(!playSteps && OVRInput.Get( OVRInput.Button.DpadUp ) || OVRInput.Get( OVRInput.Button.DpadDown ) ||OVRInput.Get( OVRInput.Button.DpadLeft ) ||OVRInput.Get( OVRInput.Button.DpadRight ));
        isKeyAllDirections=!(OVRInput.Get( OVRInput.Button.DpadUp ) || OVRInput.Get( OVRInput.Button.DpadDown ) ||OVRInput.Get( OVRInput.Button.DpadLeft ) ||OVRInput.Get( OVRInput.Button.DpadRight ));

        //print( isKeyAllDirections );
        //Debug.Log( "keydown"+ " " +isGetKeyDown );
      

        if( isKeyAllDirections )
        {
            m_Audio1.Stop();
            m_Audio2.Stop();
            m_Audio3.Stop();
            m_Audio4.Stop();
            m_Audio5.Stop();
            m_Audio6.Stop();
            m_Audio7.Stop();

            isKeyAllDirections=false;
        }


        if( playSteps )
        {
            playSteps = false;
            StopCoroutine( PlayFootstep() );
        }
        else if( isGetKeyDown)
        {
       
            StartCoroutine( PlayFootstep() );
            
        }
        if( elapsedTime > 0.5f  )
        {
            if( !isGetKeyDown )
            {
                m_Audio1.Stop();
                m_Audio2.Stop();
                m_Audio3.Stop();
                m_Audio4.Stop();
                m_Audio5.Stop();
                m_Audio6.Stop();
                m_Audio7.Stop();
                elapsedTime = 0f;
            }
            if( OVRInput.Get( OVRInput.Button.DpadUp ) || OVRInput.Get( OVRInput.Button.DpadDown ) ||OVRInput.Get( OVRInput.Button.DpadLeft ) ||OVRInput.Get( OVRInput.Button.DpadRight ))
             {
            StartCoroutine( PlayFootstep() );
            }


        }

    }
    #endregion

    #region Utils
    IEnumerator PlayFootstep()
    {

        yield return new WaitForSeconds( 0.4f );
        if( !m_Audio1.isPlaying && !m_Audio2.isPlaying && !m_Audio3.isPlaying && !m_Audio4.isPlaying && !m_Audio5.isPlaying && !m_Audio6.isPlaying && !m_Audio7.isPlaying )
        {

           count = Random.Range( 1, 8 );
            print( count );
            if(count == 1)
                m_Audio1.Play();
            if( count == 2 )
                m_Audio2.Play();
            if( count == 3 )
                m_Audio3.Play();
            if( count == 4 )
                m_Audio4.Play();
            if( count == 5 )
                m_Audio5.Play();
            if( count == 6 )
                m_Audio6.Play();
            if( count == 7 )
                m_Audio7.Play();


        }
            
        elapsedTime = 0f;

    }
    #endregion

    #region Private Members
    private AudioSource m_Audio1;
    private AudioSource m_Audio2;
    private AudioSource m_Audio3;
    private AudioSource m_Audio4;
    private AudioSource m_Audio5;
    private AudioSource m_Audio6;
    private AudioSource m_Audio7;
    private Vector3 pos;
    private Transform m_playerPos;
    private float elapsedTime =0f;
    private bool playSteps;
    private bool isGetKeyDown=false;
    private bool isKeyAllDirections=false;
    private int count;
    private Random rnd = new Random();

    #endregion
}
