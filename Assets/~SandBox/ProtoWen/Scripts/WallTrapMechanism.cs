using UnityEngine;
using System.Collections;

public class WallTrapMechanism : MonoBehaviour {

    #region Public & Protected Members

    [Range(1, 10)]
    public float m_movingSpeed;
   
    public GameObject   m_wall1, 
                        m_wall2;

    #endregion

    #region Main Methods

    void Start()
    {
        m_isActive = false;
        m_transformWall1 = m_wall1.GetComponent<Transform>();
        m_transformWall2 = m_wall2.GetComponent<Transform>();
    }

    void Update()
    {
        // Wall1 move when activated
        if( m_isActive && m_transformWall1.localPosition.z > 2.5f )
        {
            m_wall1.SetActive( true );
            m_transformWall1.position = new Vector3( m_transformWall1.position.x, m_transformWall1.position.y, m_transformWall1.position.z - (Time.deltaTime * m_movingSpeed) );
        }

        // Wall2 move when activated
        if( m_isActive && m_transformWall2.localPosition.z < -2.5f )
        {
            m_wall2.SetActive( true );
            m_transformWall2.position = new Vector3( m_transformWall2.position.x, m_transformWall2.position.y, m_transformWall2.position.z + (Time.deltaTime * m_movingSpeed) );
        }


    }

    /// <summary>
    /// Call on Trigger to activate trap (Wall -> Move)
    /// </summary>
    public void LaunchTrap()
    {
        m_isActive = true;
    }

    #endregion


    #region Utils

    #endregion


    #region Private Members

    private bool m_isActive = false;
    private Transform   m_transformWall1,
                        m_transformWall2;

    #endregion
}
