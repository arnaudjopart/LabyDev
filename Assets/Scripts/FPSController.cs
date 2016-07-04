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
    public float rotationYp=0f;
    public float rotationX = 0F;
    public GameObject m_cibleTop;
    public GameObject m_cibleRight;
    public GameObject m_spot;

    public Transform m_player;
    public Rigidbody m_rb;
    public Camera m_camera;
    public float jumpMagnitude=2f;

    public RaycastHit m_raycast;


    #endregion
    


    #region Main Methods
    void Start()
    {

    }

    void Update()
    {
        //Sert à faire pointer la torche là où le joueur regarde
        RaycastHit hit;
        if (Physics.Raycast(m_camera.transform.position, m_camera.transform.TransformDirection(Vector3.forward), out hit))
        {
            m_spot.transform.LookAt( hit.point );
        }

        
        //Fait tourner le joueur et la camera
        transform.Rotate( 0, Input.GetAxis( "Mouse X" ) * sensitivityX, 0 );
        rotationY += Input.GetAxis( "Mouse Y" ) * sensitivityY;
        rotationY = Mathf.Clamp( rotationY, minimumY, maximumY );
        m_camera.transform.localEulerAngles = new Vector3( -rotationY, 0, 0 );

        


        if( Input.GetKey( "space" ) )
        {
            m_rb.AddForce( new Vector3( 0, jumpMagnitude, 0 ) );
            
        }


        if( Input.GetKey( "up" ) )
        {
            transform.Translate( new Vector3( m_cibleTop.transform.localPosition.x / 10, 0, m_cibleTop.transform.localPosition.z / 10 ) );
        }

        if( Input.GetKey( "right" ) )
        {
            transform.Translate( new Vector3( m_cibleRight.transform.localPosition.x / 10, 0, m_cibleRight.transform.localPosition.z / 10 ) );
        }

        if( Input.GetKey( "left" ) )
        {
            transform.Translate( new Vector3( (m_cibleRight.transform.localPosition.x / 10) * -1, 0, (m_cibleRight.transform.localPosition.z / 10) * -1 ) );
        }

        if( Input.GetKey( "down" ) )
        {

            transform.Translate( new Vector3( (m_cibleTop.transform.localPosition.x / 10) * -1, 0, (m_cibleTop.transform.localPosition.z / 10) * -1 ) );
        }

        
    }
    #endregion
}



