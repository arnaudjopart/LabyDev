using UnityEngine;
using System.Collections;

public class AlligatorTrapMechanism : MonoBehaviour {

    #region Public & Protected Members

    public GameObject m_alligator;

    #endregion

    #region Main Methods

    void Start()
    {

    }

    void Update()
    {
        if( m_isActive )
        {
            
            m_alligator.SetActive( true );
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
    private Transform m_alliTransform;


    #endregion
}
