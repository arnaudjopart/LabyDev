using UnityEngine;
using System.Collections;

public class SpikeTrapMechanism : MonoBehaviour
{

    #region Public & Protected Members

    [Range(2f,15f)]
    public float m_spikeInTimer;
    [Range(.5f,3f)]
    public float m_spikeOutTimer;

    [HideInInspector]
    public GameObject m_trap;
    [HideInInspector]
    public GameObject m_spike;
    // public bool m_isTrigger;
        

    #endregion

    #region Main Methods

    void Start()
    {
        m_currentTimer = 0;

     
    }

    void Update()
    {

        if( m_currentTimer >= m_spikeInTimer + m_spikeOutTimer )
        {
            m_currentTimer = 0;
            m_spike.SetActive( false );
        }
        else if(m_currentTimer >= m_spikeInTimer )
        {
   
            m_spike.SetActive( true );
        }
        
            m_currentTimer += Time.deltaTime;
        
        }


    #endregion


    #region Utils

    #endregion


    #region Private Members

    private float m_currentTimer;

    #endregion
}

