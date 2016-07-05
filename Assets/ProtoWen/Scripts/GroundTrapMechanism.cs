using UnityEngine;
using System.Collections;

public class GroundTrapMechanism : MonoBehaviour {

    #region Public & Protected Members

    
    public GameObject m_platformref;
    [HideInInspector]
    public int  m_x = 10,
                m_y = 10;

    private GameObject[][] m_platforms;

    private float m_currentTimer;
    private int m_count;
    [Range(1,5)]
    public float m_freqTimer = 2;
    #endregion

    #region Main Methods

    void Start()
    {
        m_isActive = false;
        m_currentTimer = 0;
        m_count = 0;

        m_platforms = new GameObject[ m_x ][];
        for( int i = 0; i < m_x; i++ )
        {
            m_platforms[ i ] = new GameObject[ m_y ];
            for( int j = 0; j < m_y; j++ )
            {
                m_platforms[ i ][ j ] = (GameObject)Instantiate(m_platformref, new Vector3(i -4.5f, 0, j - 4.5f), Quaternion.identity);
            }
        }
    }

    void Update()
    {
        // Floor fall when activated
        if( m_isActive )
        {
            m_currentTimer += Time.deltaTime;

            if( m_currentTimer >= m_freqTimer * (m_count + 1) && m_count <= 4)
            {
                for( int i = 4 - m_count; i < 6 + m_count; i++ )
                {
                    for( int j = 4 - m_count; j < 6 + m_count; j++ )
                    {
                       
                       
                            FallingPlatformMechanism plat = m_platforms[ i ][ j ].GetComponent<FallingPlatformMechanism>();
                            plat.LaunchTrap();
                        

                       
                       
                    }
                }

                m_count++;
            }

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

    #endregion
}
