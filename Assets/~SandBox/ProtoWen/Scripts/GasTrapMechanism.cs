using UnityEngine;
using System.Collections;

public class GasTrapMechanism : MonoBehaviour {

    #region Public & Protected Members

    [Range(1, 10)]
    public float m_gasProgressionSpeed;
    [HideInInspector]
    public GameObject m_gas;

  
    public GameObject m_gasPart;

    #endregion

    #region Main Methods

    void Start()
    {
        m_isActive = false;
        m_gasTransform = m_gas.GetComponent<Transform>();
       
    }

    void Update()
    {
        // Floor fall when activated
        if( m_isActive && m_gasTransform.localScale.x < 10f )
        {
            m_gas.SetActive( true );
            m_gasPart.SetActive( true );
            m_gasTransform.localScale = new Vector3( m_gasTransform.localScale.x + (Time.deltaTime * m_gasProgressionSpeed), m_gasTransform.localScale.y, m_gasTransform.localScale.z + (Time.deltaTime * m_gasProgressionSpeed));
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
    private Transform m_gasTransform;

    #endregion
}
