using UnityEngine;
using System.Collections;

public class SpikeTrapMechanism : MonoBehaviour
{

    #region Public & Protected Members

    public bool isWorking ;

    [HideInInspector]
    public float m_spikeInTimer;
    [HideInInspector]
    public float m_spikeOutTimer;
    [HideInInspector]
    public GameObject m_trap;
    [HideInInspector]
    public GameObject m_spike;

    #endregion

    #region Main Methods

    void Start()
    {
        m_currentTimer = 0;

        // gen. a random in and out timer for the spike mechanism
        m_spikeInTimer = Random.Range( 2f, 15f );
        m_spikeOutTimer = Random.Range( .5f, 3f );


    }

    void Update()
    {
        if( isWorking )
        {
            // Spikes -> In
            if( m_currentTimer >= m_spikeInTimer + m_spikeOutTimer )
            {
                m_currentTimer = 0;
                m_spike.SetActive( false );
            }
            // Spikes -> Out
            else if( m_currentTimer >= m_spikeInTimer )
            {
                m_spike.SetActive( true );
            }

            m_currentTimer += Time.deltaTime;

        }
    }


    #endregion


    #region Utils

    #endregion


    #region Private Members

    private float m_currentTimer;

    #endregion
}

