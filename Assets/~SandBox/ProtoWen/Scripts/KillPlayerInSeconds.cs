using UnityEngine;
using System.Collections;

public class KillPlayerInSeconds : MonoBehaviour {

    #region Public & Protected Members

    public float m_maxTimer ;

    #endregion

    #region Main Methods

    /// <summary>
    /// Destroy player on collision
    /// </summary>
    /// <param name="_other">The object that collide</param>
    void OnTriggerEnter( Collider _other )
    {
        print( "collision Enter with " + _other.name );
        m_actualTimer = 0f;
    }

    void OnTriggerStay(Collider _other )
    {
        print( "collision Stay with " +  m_actualTimer);

        if(m_actualTimer <= m_maxTimer )
        {
            m_actualTimer += Time.deltaTime;
        }
        else
        {
            if( _other.GetComponent<FPSController>() )
            {
               
                GameManager.GameOver();

            }

        }
    }

    void OnTriggerExit( Collider _other )
    {
        print( "collision Exit with " + _other.name );
    }

    #endregion


    #region Utils

    #endregion


    #region Private Members

    private float m_actualTimer = 0f;
    #endregion
}
