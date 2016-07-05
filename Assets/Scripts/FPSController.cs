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



    public RaycastHit m_raycast;

    public float m_time;
    public bool m_canFlash;
    public bool m_waitCoroutine;

    public bool m_isAlive;

    public float m_speed;

    public GameObject m_cone;



    #endregion



    #region Main Methods
    void Start()
    {
        m_cone.GetComponent<Renderer>().material.color = new Color( 1.0f, 1.0f,1.0f, 0.1f );
        
       

    }

    void Update()
    {

        //Debug.Log( m_time );
        //Debug.Log( m_spot.intensity );
        m_time -= Time.deltaTime;

        if ( Random.Range( 999, 1000 ) >= 999 && m_canFlash == true && m_waitCoroutine == false )
        {
            StartCoroutine( Flash() );
            m_canFlash = false;

        }

        if ( m_canFlash == false && m_waitCoroutine == false )
        {
            m_waitCoroutine = true;
            StartCoroutine( Wait() );
        }



        m_spot.intensity = m_time / 30;
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

        

    }

    #endregion

    #region Utils
    IEnumerator Flash()
    {
        m_spot.intensity = 0;
        yield return new WaitForSeconds( 0.1f );
        m_spot.intensity = m_time / 30;
        yield return new WaitForSeconds( 0.1f );
        m_spot.intensity = 0;
        yield return new WaitForSeconds( 0.1f );
        m_spot.intensity = m_time / 30;
        m_spot.intensity = 0;
        yield return new WaitForSeconds( 0.1f );
        StopCoroutine( Flash() );
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds( 2f );

        m_canFlash = true;
        m_waitCoroutine = false;
        StopCoroutine( Wait() );
    }

    #endregion
}



