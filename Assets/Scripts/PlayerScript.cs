﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

class PlayerScript : NetworkBehaviour
{
    [SyncVar]
    public Vector3 posPlayerFps;

    public GameObject m_playerPrefab;

    void Start ()
    {
        if (isLocalPlayer)
        {
            if (isServer)
                m_state = PlayerType.FpsLocal; //Player instance is the Local Fps Player
            else
                m_state = PlayerType.MapLocal; //Player instance is the Local Map Player
        }
        else
        {
            if ( !isServer )
                m_state = PlayerType.FpsDistant; //Player instance is the Distant Fps Player
        }

        InitPlayer();
    }

	void Update ()
    {
	    if ( m_state == PlayerType.FpsLocal )
        {
            m_transform.Translate( new Vector3( Input.GetAxis( "Horizontal" ) * 0.1f, 0, Input.GetAxis( "Vertical" ) * 0.1f ) );
            posPlayerFps = m_transform.position;
        }
        else
        if ( m_state == PlayerType.FpsDistant )
        {
            m_transform.position = posPlayerFps;
        }
        else
        if ( m_state == PlayerType.MapLocal )
        {
            Camera.main.transform.Translate( new Vector3( Input.GetAxis( "Horizontal" ) * 0.1f, Input.GetAxis( "Vertical" ) * 0.1f, 0 ) );
        }
	}

    #region Utils
    private void InitPlayer()
    {
        Debug.Log( "Connected !" );
        Camera.main.GetComponentInChildren<Canvas>().enabled = false;

        m_transform = GetComponent<Transform>();

        if( m_state == PlayerType.FpsLocal )
        {
            this.name = "Fps Player (Local)";

            m_player = (GameObject)Instantiate( m_playerPrefab, new Vector3( 0, 0, 0 ), Quaternion.identity );
            m_player.transform.parent = this.transform;

            Camera.main.transform.position = new Vector3( 0, 2, -4 );
            Camera.main.transform.parent = m_player.transform;
        }
        else
        if( m_state == PlayerType.MapLocal )
        {
            this.name = "Map Player (Local)";

            Camera.main.transform.position = new Vector3( 0, 50, 0 );
            Camera.main.transform.eulerAngles = new Vector3( 90, 0, 0 );
        }
        else
        if( m_state == PlayerType.FpsDistant )
        {
            this.name = "Fps Player (Distant)";

            m_player = (GameObject)Instantiate( m_playerPrefab, new Vector3( 0, 0, 0 ), Quaternion.identity );
            m_player.transform.parent = this.transform;
        }
    }
    #endregion

    #region Private Var
    private GameObject m_player;
    private Transform m_transform;

    private enum PlayerType { FpsLocal, FpsDistant, MapLocal };
    private PlayerType m_state;
    #endregion
}
