using UnityEngine;
using System.Collections;

public class WallTriggeredByPlayer : MonoBehaviour {

    #region Public & Protected Members

    [HideInInspector]
    public GameObject m_parent;

    #endregion

    #region Main Methods

    void Start()
    {
        m_script = m_parent.GetComponent<WallTrapMechanism>();

    }

    /// <summary>
    /// Activate Trap mechanism on trigger
    /// </summary>
    /// <param name="_other">The object that collide</param>
    void OnTriggerEnter( Collider _other )
    {
        if( m_script != null )
        {
            m_script.LaunchTrap();
        }
    }
    #endregion

    #region Utils

    #endregion

    #region Private Members

    private WallTrapMechanism m_script;

    #endregion
}
