using UnityEngine;
using System.Collections;

public class SmokeScript : MonoBehaviour {



    #region Public and Protected Members
    public float m_emissionSpeed = 10f;
    public float m_maxRadiusShape = 10f;
    public float m_maxEmissionRate = 100f; 
    #endregion

    #region Main Methods
    // Use this for initialization
    void Start()
    {

        m_particleSystem = GetComponent<ParticleSystem>();
        

        m_shapeModule = m_particleSystem.shape;
        m_shapeModule.radius = 0f;

        m_emissionModule = m_particleSystem.emission;
        m_minMaxCurve = new ParticleSystem.MinMaxCurve( 1f);
        m_emissionModule.rate = m_minMaxCurve;

        m_count = m_emissionSpeed * 60;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_count > 0)
        {
            m_shapeModule.radius += m_maxRadiusShape / (m_emissionSpeed * 60);
            m_minMaxCurve = m_emissionModule.rate;
            m_minMaxCurve.constantMax += m_maxEmissionRate / (m_emissionSpeed * 60);
            //m_minMaxCurve = new ParticleSystem.MinMaxCurve( m_minMaxCurve.constantMax + m_maxEmissionRate / (m_emissionSpeed * 60) );
            m_emissionModule.rate = m_minMaxCurve;
            m_count--;
        }
       
    }
    #endregion

    #region Utils

    #endregion

    #region Private Members
    private ParticleSystem m_particleSystem;
    private ParticleSystem.ShapeModule m_shapeModule;
    private ParticleSystem.EmissionModule m_emissionModule;
    private ParticleSystem.MinMaxCurve m_minMaxCurve;
    private float m_count;
    #endregion
}
