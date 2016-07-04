﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    #region Public and Protected Members
    // 0 = méchant, 1 = gentil
    public int objective;
    public bool m_PlayerIsAlive;
    #endregion

    #region Main Methods
    void Start()
    {
        objective = Random.Range( 0, 1 );
        StartCoroutine( But() );

        //m_PlayerIsAlive = GameObject.Find( "Player" ).GetComponent<FPSController>().m_isAlive;

    }
    void Update()
    {
        if ( !m_PlayerIsAlive )
        {
            //Application.LoadLevel();
        }
    }
    #endregion

    #region Utils
    IEnumerator But()
    {
        yield return new WaitForSeconds( Random.Range( 30, 80 ) );
        if ( objective == 0 )
        {
            objective = 1;
        }
        else
        {
            objective = 0;
        }
    }
    #endregion

    #region Private Members
    #endregion

}
