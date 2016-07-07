using UnityEngine;
using System.Collections;

public class AlligatortrapTriggeredByPlayer : MonoBehaviour {

    #region Public & Protected Members

    public GameObject m_parent;

    public float m_maxTimer = 5f;

    #endregion

    #region Main Methods

    void Start()
    {
        m_script = m_parent.GetComponent<AlligatorTrapMechanism>();
    }

    void Update()
    {

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

    private AlligatorTrapMechanism m_script;
    private float m_currentTimer;

    #endregion
}
