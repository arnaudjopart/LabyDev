using UnityEngine;
using System.Collections;

public class FloorTrapMechanism : MonoBehaviour {

    #region Public & Protected Members

    [Range(1, 10)]
    public float m_fallingSpeed;
    [HideInInspector]
    public GameObject m_floor;

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
        if( m_isActive && m_floorTransform.position.y > 0)
        {
            m_floorTransform.position = new Vector3( m_floorTransform.position.x, m_floorTransform.position.y -(Time.deltaTime * m_fallingSpeed) , m_floorTransform.position.z );
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
