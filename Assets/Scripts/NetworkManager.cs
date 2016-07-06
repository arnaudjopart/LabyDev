using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System;
using UnityEngine.SceneManagement;
using System.Text;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager m_instance;

    public Canvas m_ConnectionFrame;

    public GameObject m_prefabFpsLocal;
    public GameObject m_prefabFpsIcon;
    public GameObject m_prefabMonitor;
    
    public bool m_IsServer;
    public bool m_IsConnected;
    public string m_Ip;

    public GameManager m_gameManager;

    void Start ()
    {
	    if ( !m_instance )
        {
            m_instance = this;
        }

        if( Global.Server )
            StartServer();
        else
            StartClient( Global.Ip );
    }

    void Update ()
    {
        if (m_IsConnected)
        {
            try
            {
                if (!m_showConnect)
                {
                    m_gameManager.m_uiCanvas.LoadNewSMS( "Jack join !" );

                    if (!m_IsServer)
                    {
                        SendObjectif( GameManager.m_player2Objective );
                    }

                    m_showConnect = true;
                }

                if( m_IsServer )
                {
                    Send( 1, GetBytes( m_FpsLocal.transform.position ) );
                }

                Receive();
            }
            catch
            {
                m_IsConnected = false;
                m_gameManager.m_uiCanvas.LoadNewSMS( "No network :/" );
            }
        }
	}
    
    #region Utils Server
    private void InitServer()
    {
        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 7777);
        
        m_socket = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
        
        m_socket.Bind( localEndPoint );

        m_socket.Listen( 100 );

        m_gameManager.m_uiCanvas.LoadNewSMS( "Server Started" );

        InitFpsPlayer();

        m_socket.BeginAccept( new AsyncCallback( AcceptCallback ),
                    m_socket );
    }

    public void AcceptCallback( IAsyncResult ar )
    {
        Socket listener = (Socket) ar.AsyncState;
        Socket handler = listener.EndAccept(ar);

        m_socket = handler;
        
        m_IsConnected = true;
    }

    private void InitFpsPlayer()
    {
        m_FpsLocal = (GameObject)Instantiate( m_prefabFpsLocal , Global.playerSpawnPosition, Quaternion.identity);
        m_ConnectionFrame.enabled = false;
    }
    #endregion

    #region Utils Client
    private void InitClient()
    {
        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(m_Ip), 7777);

        m_socket = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

        try
        {
            m_socket.Connect( localEndPoint );

            Debug.Log( "Connected" );
            m_IsConnected = true;

            m_ConnectionFrame.enabled = false;

            m_FpsIcon = Instantiate( m_prefabFpsIcon );
            m_Monitor = Instantiate( m_prefabMonitor );
        }
        catch
        {
            Debug.Log( "Time Out !" );
            SceneManager.LoadScene( 0 );
        }
    }
    #endregion

    #region Receive
    private void Receive()
    {
        while( m_socket.Available > 1)
        {
            if( m_lenghtPacket != -1 )
            {
                if( m_socket.Available >= m_lenghtPacket )
                {
                    byte[] data = new byte[m_lenghtPacket];
                    m_socket.Receive( data );

                    ProcessPacket( data );

                    m_lenghtPacket = -1;
                }
            }
            else
            {
                byte[] lenght = new byte[ 1 ];

                if( m_socket.Available > 0 )
                {
                    m_socket.Receive( lenght );
                    m_lenghtPacket = lenght[ 0 ];
                }
            }
        }
    }

    private void ProcessPacket(byte[] _packet)
    {
        if (_packet[0] == 0) //Debug
        {
            string array = "";

            foreach( byte b in _packet )
                array += b;

            Debug.Log( array );
        }
        else
        if( _packet[ 0 ] == 1 ) //FPS Pos
        {
            Vector3 pos = GetVector3( SubPacket( _packet, 1, _packet.Length - 1 ) );
            pos.y = 100;

            m_FpsIcon.transform.position = pos;
        }
        else
        if ( _packet[ 0 ] == 2 ) //Sms
        {
            string sms = Encoding.ASCII.GetString( SubPacket( _packet, 1, _packet.Length - 1 ) );

            m_gameManager.m_uiCanvas.LoadNewSMS( sms );
        }
        else
        if ( _packet[ 0 ] == 3 ) //objectif state
        {
            GameManager.m_player2Objective = _packet[ 1 ];
            m_gameManager.m_uiCanvas.LoadNewSMS( "objectif changed : " + GameManager.m_player2Objective );
        }
        else
        if( _packet[ 0 ] == 4 ) //Player VR is dead
        {
            GameManager.GameOver();
        }

    }
    #endregion

    #region Send
    private void Send(byte Id, byte[] _data)
    {
        if (m_IsConnected)
        {
            byte[] header = new byte[2] { (byte)(_data.Length + 1), Id };

            m_socket.Send( AddPacket( header, _data ) );
        }
    }

    public void SendSms(string _sms)
    {
        Send( 2, Encoding.ASCII.GetBytes( _sms ) );
    }

    public void SendObjectif(int _objectif)
    {
        Send( 3, new byte[] { (byte)_objectif } );
    }

    public void SendGameOver()
    {
        Send( 4, new byte[] { 0 } );
    }
    #endregion

    #region Utils
    private static byte[] AddPacket(byte[] _packet1, byte[] _packet2 )
    {
        byte[] result = new byte[_packet1.Length + _packet2.Length];

        _packet1.CopyTo( result, 0 );
        _packet2.CopyTo( result, _packet1.Length );

        return result;
    }

    private static byte[] SubPacket(byte[] _packet, int _index, int _length)
    {
        byte[] result = new byte[_length];
        Array.Copy( _packet, _index, result, 0, _length );
        return result;
    }

    private static byte[] GetBytes( float _value )
    {
        return BitConverter.GetBytes( _value );
    }

    private static float GetFloat(byte[] _value)
    {
        return System.BitConverter.ToSingle( _value, 0);
    }

    private static byte[] GetBytes(Vector3 _value)
    {
        byte[] result = new byte[12];

        GetBytes( _value.x).CopyTo( result, 0 );
        GetBytes( _value.y ).CopyTo( result, 4 );
        GetBytes( _value.z ).CopyTo( result, 8 );

        return result;
    }

    private static Vector3 GetVector3(byte[] _value)
    {
        float x = GetFloat( SubPacket( _value, 0, 4 ));
        float y = GetFloat( SubPacket( _value, 4, 4 ));
        float z = GetFloat( SubPacket( _value, 8, 4 ));

        return new Vector3( x, y, z);
    }
    #endregion

    public void StartClient( string _Ip )
    {
        ResetNetwork();
        m_Ip = _Ip;
        m_IsServer = false;

        InitClient();
    }

    public void StartServer()
    {
        ResetNetwork();
        m_IsServer = true;

        InitServer();
    }

    private void ResetNetwork()
    {
        m_showConnect = false;
        m_IsServer = true;
        m_IsConnected = false;
        m_Ip = "127.0.0.1";
        m_lenghtPacket = -1;
        GameManager.IsGameOver = false;
    }
    
    private Socket m_socket;
    private int m_lenghtPacket;
    
    private GameObject m_FpsLocal;
    private GameObject m_FpsIcon;
    private GameObject m_Monitor;

    private bool m_showConnect;
}
