using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class NetworkScript : NetworkBehaviour
{
    public NetworkManager m_networkManager;
    public Text m_ConnectedMsg;

    void Start ()
    {
	    if (Global.Server)
        {
            m_ConnectedMsg.text = "Start Server ...";
            Debug.Log( m_ConnectedMsg.text );
        }
        else
        {
            m_ConnectedMsg.text = "Try to connect to : " + Global.Ip;
            Debug.Log( m_ConnectedMsg.text );
        }

        m_Connecting = true;
        m_ConnectTimer = Time.realtimeSinceStartup + 1.0f;
    }
    
    void Update()
    {
        if( m_Connecting )
        {
            //Loading Wait (Tiem to see Server start and player connecting
            if( Time.realtimeSinceStartup > m_ConnectTimer )
            {
                if( Global.Server )
                {
                    m_networkManager.StartHost();
                }
                else
                {
                    m_networkManager.StartClient();
                }

                m_Connecting = false;
            }
        }
        else
        if( !m_networkManager.isNetworkActive )
        {
            //TimeOut (Marche Poa :/)
            Debug.Log( "TimeOut !" );
            SceneManager.LoadScene( 0 );
            Destroy( this );
        }
    }

    public void LoadingWait()
    {
        
    }
    
    private bool m_Connecting = true;
    private float m_ConnectTimer;
}
