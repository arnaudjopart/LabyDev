using UnityEngine;
using System.Collections;

public class FloorTrapMechanism : MonoBehaviour {

    #region Public & Protected Members

    [Range(1, 10)]
    public float m_fallingSpeed;

    public GameObject m_floor;
    public GameObject m_blockingVol;

    #endregion

    #region Main Methods

    void Start()
    {
        m_isActive = false;
        m_floorTransform = m_floor.GetComponent<Transform>();
    }

    void Update()
    {
        // Floor fall when activated
        if( m_isActive && m_floorTransform.localPosition.y > 5f)
        {
            m_floorTransform.localPosition = new Vector3( m_floorTransform.localPosition.x, m_floorTransform.localPosition.y -(Time.deltaTime * m_fallingSpeed) , m_floorTransform.localPosition.z );
        }else if( m_isActive )
        {
            m_blockingVol.SetActive( true );
            m_floor.SetActive( false );
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
    private Transform m_floorTransform;

    #endregion
}
