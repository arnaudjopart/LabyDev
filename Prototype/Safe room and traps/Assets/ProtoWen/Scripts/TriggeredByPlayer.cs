using UnityEngine;
using System.Collections;

public class TriggeredByPlayer : MonoBehaviour {

    #region Public & Protected Members

    [HideInInspector]
    public GameObject m_parent;

    

    #endregion

    #region Main Methods

   void Start()
    {
        m_script = m_parent.GetComponent<FloorTrapMechanism>();
       
    }

    void OnTriggerEnter(Collider _other)
    {
        Debug.Log( "y'a collision" );
        if( m_script != null )
        {
            m_script.LaunchTrap();
            Debug.Log( "on doit tomber!" );
        }
    }


    #endregion


    #region Utils

    #endregion


    #region Private Members

    private FloorTrapMechanism m_script;

    #endregion
}
