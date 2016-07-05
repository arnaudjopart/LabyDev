using UnityEngine;
using System.Collections;

public class CubeScript : MonoBehaviour {


    #region Public and Protected Members
    public float m_rotatespeed = 1f;
    public float m_expansionSpeed = 1f;
    public bool m_triggered = false;
    public GameObject m_0;
    public GameObject m_1;
    public GameObject m_2;
    #endregion

    #region Main Methods
    // Use this for initialization
    void Start()
    {
        m_triggered = false;
        m_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_triggered)
        {
            m_transform.Rotate( 0, 1 * m_rotatespeed, 0 );

            Vector3 v3 = m_0.transform.localScale;         
            if(v3.z <= 0.996f)
            {
                v3.z += 0.003f * m_expansionSpeed;
                m_0.transform.localScale = v3;
            }

            v3 = m_1.transform.localScale;
            if(v3.z <= 0.996f)
            {
                
                v3.z += 0.003f * m_expansionSpeed;
                m_1.transform.localScale = v3;
            }

            v3 = m_2.transform.localScale;
            if(v3.z <= 0.996f)
            {
                v3.z += 0.003f * m_expansionSpeed;
                m_2.transform.localScale = v3;
            }
            
        }
        
    }
    #endregion

    #region Utils

    #endregion

    #region Private Members
    private Transform m_transform;
    
    #endregion

}
