using UnityEngine;
using System.Collections;

public class FPSController : MonoBehaviour
{
    #region Public and Protected Members
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;

    public float minimumY = -60F;
    public float maximumY = 60F;

    public float rotationY = 0F;

    public GameObject m_cibleTop;
    public GameObject m_cibleRight;
    public Light m_spot;

    public Transform m_player;
    public Rigidbody m_rb;
    public Camera m_camera;

    public bool isWaitingForJack;

    public RaycastHit m_raycast;

    public float m_time;
    public GameManager m_gameManager;



    public bool m_canFlash;
    public bool m_waitCoroutine;
    public bool m_flashing;

    public bool m_isAlive;

    public float m_speed;

    public GameObject m_cone;



    #endregion



    #region Main Methods
    void Start()
    {
        m_gameManager = GameObject.FindObjectOfType<GameManager>();
        m_cone.GetComponent<Renderer>().material.color = new Color( 1.0f, 1.0f, 1.0f, 0.2f );

        m_audio = GetComponents<AudioSource>()[ 2 ];

    }

    void Update()
    {


        m_time = m_gameManager.m_gameTimeInSeconds;
        if ( m_time <= 0 )
        {
            m_cone.GetComponent<Renderer>().material.color = new Color( 1.0f, 1.0f, 1.0f, 0f );
        }

        if ( Random.Range( 999, 1000 ) >= 999 && m_canFlash == true && m_waitCoroutine == false && m_flashing == false )
        {
            StartCoroutine( Flash() );
            m_canFlash = false;
            m_flashing = true;

        }

        if ( m_canFlash == false && m_waitCoroutine == false && m_flashing == false )
        {
            m_waitCoroutine = true;
            StartCoroutine( Wait() );
        }


        if ( m_flashing == false )
        {
            m_spot.intensity = m_time / 30;
        }
        //Sert à faire pointer la torche là où le joueur regarde
        RaycastHit hit;
        if ( Physics.Raycast( m_camera.transform.position, m_camera.transform.TransformDirection( Vector3.forward ), out hit ) )
        {
            m_spot.transform.LookAt( hit.point );

        }




        //Fait tourner le joueur et la camera
        transform.Rotate( 0, Input.GetAxis( "Mouse X" ) * sensitivityX, 0 );
        rotationY += Input.GetAxis( "Mouse Y" ) * sensitivityY;
        rotationY = Mathf.Clamp( rotationY, minimumY, maximumY );
        m_camera.transform.localEulerAngles = new Vector3( -rotationY, 0, 0 );

        // isConnected TRISTAN !!!!!!!!!!! 
        if ( true )
        {
            if ( Input.GetKey( "up" ) )
            {
                transform.Translate( new Vector3( m_cibleTop.transform.localPosition.x * m_speed * Time.deltaTime, 0, m_cibleTop.transform.localPosition.z * m_speed * Time.deltaTime ) );
            }

            if ( Input.GetKey( "right" ) )
            {
                transform.Translate( new Vector3( m_cibleRight.transform.localPosition.x * m_speed * Time.deltaTime, 0, m_cibleRight.transform.localPosition.z * m_speed * Time.deltaTime ) );
            }

            if ( Input.GetKey( "left" ) )
            {
                transform.Translate( new Vector3( (m_cibleRight.transform.localPosition.x * m_speed * Time.deltaTime) * -1, 0, (m_cibleRight.transform.localPosition.z * m_speed * Time.deltaTime) * -1 ) );
            }

            if ( Input.GetKey( "down" ) )
            {

                transform.Translate( new Vector3( (m_cibleTop.transform.localPosition.x * m_speed * Time.deltaTime) * -1, 0, (m_cibleTop.transform.localPosition.z * m_speed * Time.deltaTime) * -1 ) );
            }
            if ( transform.position.y < -1 && transform.position.y > -10 )
            {
                if ( !m_audio.isPlaying )
                {
                    m_audio.Play();
                    m_audio.loop = true;
                }
            }
            else
            {
                m_audio.Stop();
            }
        }
    }

    #endregion

    #region Utils
    IEnumerator Flash()
    {

        int j = Random.Range(2,6);
        Debug.Log( j );
        for ( int i = 0; i < j; i++ )
        {

            m_spot.intensity = 0;
            m_cone.GetComponent<Renderer>().material.color = new Color( 1.0f, 1.0f, 1.0f, 0f );
            yield return new WaitForSeconds( Random.Range( 0.1f, 0.5f ) );
            m_spot.intensity = m_time / 30;
            m_cone.GetComponent<Renderer>().material.color = new Color( 1.0f, 1.0f, 1.0f, 0.2f );
            yield return new WaitForSeconds( Random.Range( 0.1f, 0.3f ) );
        }
        m_flashing = false;
        StopCoroutine( Flash() );
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds( 15f );

        m_canFlash = true;
        m_waitCoroutine = false;
        StopCoroutine( Wait() );
    }

    #endregion

    private AudioSource m_audio;
}



