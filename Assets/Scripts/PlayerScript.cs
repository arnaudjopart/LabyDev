using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

class PlayerScript : NetworkBehaviour
{
    [SyncVar]
    public Vector3 m_posPlayerFps;
    [SyncVar]
    public Quaternion m_rotatePlayerFps;

    public GameObject m_fpsPlayerPrefab;
    public GameObject m_fpsDistantPlayerPrefab;
    public GameObject m_mapPlayerPrefab;

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
            m_posPlayerFps = m_playerTransform.position;
            m_rotatePlayerFps = m_playerTransform.rotation;
        }
        else
        if ( m_state == PlayerType.FpsDistant )
        {
            m_playerTransform.position = new Vector3( m_posPlayerFps.x, 100, m_posPlayerFps.z); //100 = hauteur icone
            m_playerTransform.rotation = m_rotatePlayerFps;
        }
        else
        if ( m_state == PlayerType.MapLocal )
        {
            //Camera.main.transform.Translate( new Vector3( Input.GetAxis( "Horizontal" ) * 0.1f, Input.GetAxis( "Vertical" ) * 0.1f, 0 ) );
        }
	}

    #region Utils
    private void InitPlayer()
    {
        Debug.Log( "Connected !" );
        Camera.main.GetComponentInChildren<Canvas>().enabled = false;

        m_transform = GetComponent<Transform>();

        if( m_state == PlayerType.FpsLocal && !Global.PlayerSpawned )
        {
            Global.PlayerSpawned = true;

            this.name = "Fps Player (Local)";

            m_player = (GameObject)Instantiate( m_fpsPlayerPrefab, new Vector3( 0, 0, 0 ), Quaternion.identity );
            m_playerTransform = m_player.GetComponent<Transform>();

            m_playerTransform.parent = this.transform;
        }
        else
        if( m_state == PlayerType.MapLocal )
        {
            this.name = "Map Player (Local)";

            m_player = (GameObject)Instantiate( m_mapPlayerPrefab, new Vector3( 0, 0, 0 ), Quaternion.identity );
            m_playerTransform = m_player.GetComponent<Transform>();

            m_playerTransform.parent = this.transform;
        }
        else
        if( m_state == PlayerType.FpsDistant )
        {
            this.name = "Fps Player (Distant)";

            m_player = (GameObject)Instantiate( m_fpsDistantPlayerPrefab, new Vector3( 0, 0, 0 ), Quaternion.identity );
            m_playerTransform = m_player.GetComponent<Transform>();

            m_playerTransform.parent = this.transform;
        }
        else
        {
            m_state = PlayerType.Empty;
        }
    }
    #endregion

    #region Private Var
    private GameObject m_player;
    private Transform m_playerTransform;
    private Transform m_transform;

    private enum PlayerType { FpsLocal, FpsDistant, MapLocal, Empty };
    private PlayerType m_state;
    #endregion
}
