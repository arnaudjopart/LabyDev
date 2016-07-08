using UnityEngine;
using System.Collections;

public class TriangleScript : MonoBehaviour {

    #region Public and Protected Members
    public float m_speed = 0.001f;
    public bool m_triggered = false;
    #endregion

    #region Main Methods

    // Use this for initialization
    void Start()
    {
        m_pyrTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_triggered)
        {
            if( m_pyrTransform.localPosition.y <= 0)
            {
                m_pyrTransform.position = new Vector3( m_pyrTransform.position.x, m_pyrTransform.position.y + m_speed, m_pyrTransform.position.z );
              
            }
        }
       
        
    }
    #endregion

    #region Utils

    #endregion

    #region Private Members

    private Transform m_pyrTransform;

    #endregion
}
