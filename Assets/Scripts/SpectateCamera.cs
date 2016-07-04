using UnityEngine;
using System.Collections;

public class SpectateCamera: MonoBehaviour
{
    #region Public And Protected Members
    [Header ("Zoom")]
    public float m_speedZoom = 1;
    public float m_zoomInMin = 1;
    public float m_zoomOutMax = 5;

    [Header("Map")]
    public float m_speedMovement;
    public float m_xMin;
    public float m_xMax;
    public float m_zMin;
    public float m_zMax;

    [Header("Mouse")]
    public float m_speedMouse = 1;
    public float m_SpeedscrollMouse = 1;


    [HideInInspector]
    public Camera m_cam;
    #endregion

    #region Main Methods
    void Start()
    {
        m_camera = m_cam.GetComponent<Camera>();
        m_defaultPosition = m_camera.transform.position;
        m_position = m_camera.transform.position;
        m_sizeCam = m_camera.orthographicSize;
    }

    void Update()
    {
        if( Input.GetKey( "i" ) )
        {
            Debug.Log( "Inzoom" );
            m_sizeCam -= m_speedZoom * Time.deltaTime;
            m_sizeCam = Mathf.Clamp( m_sizeCam, m_zoomInMin, m_zoomOutMax );
            m_camera.orthographicSize = m_sizeCam;
        }

        if( Input.GetKey( "o" ) )
        {
            Debug.Log( "OutZoom" );
            m_sizeCam += m_speedZoom * Time.deltaTime;
            m_sizeCam = Mathf.Clamp( m_sizeCam, m_zoomInMin, m_zoomOutMax );
            m_camera.orthographicSize = m_sizeCam;
        }

        if( Input.GetKey( "q" ) )
        {
            Debug.Log( "Gauche" );
            m_position.x += m_speedMovement * Time.deltaTime;
            m_position.x = Mathf.Clamp( m_position.x, m_xMin, m_xMax );
            m_camera.transform.position = m_position;
        }

        if( Input.GetKey( "d" ) )
        {
            Debug.Log( "Droite" );
            m_position.x -= m_speedMovement * Time.deltaTime;
            m_position.x = Mathf.Clamp( m_position.x, m_xMin, m_xMax );
            m_camera.transform.position = m_position;
        }

        if( Input.GetKey( "z" ) )
        {
            Debug.Log( "Haut" );
            m_position.z -= m_speedMovement * Time.deltaTime;
            m_position.z = Mathf.Clamp( m_position.z, m_zMin, m_zMax );
            m_camera.transform.position = m_position;
        }

        if( Input.GetKey( "s" ) )
        {
            Debug.Log( "Bas" );
            m_position.z += m_speedMovement * Time.deltaTime;
            m_position.z = Mathf.Clamp( m_position.z, m_zMin, m_zMax );
            m_camera.transform.position = m_position;
        }

        if( Input.GetKey( "p" ) || Input.GetMouseButton( 1 ) )
        {
            Debug.Log( "DefaultPosition" );
            m_sizeCam = m_zoomOutMax;
            m_camera.orthographicSize = m_zoomOutMax;            
            m_camera.transform.position = m_defaultPosition;
            m_position = m_defaultPosition;
        }

        if( Input.GetMouseButtonDown( 0 ) )
        {
            m_mouseDefault = m_camera.ScreenToWorldPoint( Input.mousePosition );
        }

        if( Input.GetMouseButton( 0 ) )
        {
            Debug.Log( "Pressed left click." );
            m_mouseCurrent = m_camera.ScreenToWorldPoint( Input.mousePosition );

            m_mouseVector = m_mouseDefault - m_mouseCurrent;

            m_camera.transform.Translate( m_mouseVector, Space.World );

            m_position = m_camera.transform.position;
            m_position.x = Mathf.Clamp( m_position.x, m_xMin, m_xMax );
            m_position.z = Mathf.Clamp( m_position.z, m_zMin, m_zMax );
            m_camera.transform.position = m_position;
        }

        m_sizeCam += Input.GetAxis( "Mouse ScrollWheel" ) * m_SpeedscrollMouse;
        m_sizeCam = Mathf.Clamp( m_sizeCam, m_zoomInMin, m_zoomOutMax );

        m_camera.orthographicSize = m_sizeCam;
    }
    #endregion

    #region Utils

    #endregion

    #region Private Members
    private float m_sizeCam;

    private Camera m_camera;

    private Vector3 m_position;
    private Vector3 m_defaultPosition;
    private Vector3 m_mouseVector;
    private Vector3 m_mouseCurrent;
    private Vector3 m_mouseDefault;
    #endregion
}
