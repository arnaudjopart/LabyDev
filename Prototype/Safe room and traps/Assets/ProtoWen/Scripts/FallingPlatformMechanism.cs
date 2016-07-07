using UnityEngine;
using System.Collections;

public class FallingPlatformMechanism : MonoBehaviour {

    #region Public & Protected Members

    public bool isWorking ;

    [Range(5, 10)]
    public float m_fallingSpeed;
    [HideInInspector]
    public GameObject m_platform;


    #endregion

    #region Main Methods

    void Start()
    {
        m_isActive = false;
        m_PlatformTransform = m_platform.GetComponent<Transform>();
    }

    void Update()
    {
        // Floor fall when activated
        if( m_isActive )
        {
            m_PlatformTransform.position = new Vector3( m_PlatformTransform.position.x, m_PlatformTransform.position.y - (Time.deltaTime * m_fallingSpeed), m_PlatformTransform.position.z );
        }
        else if( m_PlatformTransform.position.y < -10f )
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Call on Trigger to activate trap (Floor -> fall)
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
    private Transform m_PlatformTransform;

    #endregion

}
