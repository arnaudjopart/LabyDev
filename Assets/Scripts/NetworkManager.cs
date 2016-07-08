using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System;
using UnityEngine.SceneManagement;
using System.Text;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager m_instance;

    public GameObject m_ConnectionFrame;
    public GameObject m_UIGSM;
    
    public GameObject m_prefabFpsLocal;
    public GameObject m_prefabFpsIcon;
    public GameObject m_prefabMonitor;
    
    public bool m_IsServer;
    public bool m_IsConnected;
    public string m_Ip;

    public GameManager m_gameManager;
    public InitiateMapManager m_mapManager;

    void Start ()
    {
        if( !m_instance )
        {
            m_instance = this;
        }

        m_msgconnection = m_ConnectionFrame.GetComponentInChildren<Text>();

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
                    lastPing = Time.realtimeSinceStartup;
                    m_gameManager.m_uiCanvas.LoadNewSMS( "Jack join !" );
                    m_showConnect = true;
                }

                if( m_IsServer )
                {
                    Send( 1, GetBytes( m_FpsLocal.transform.position ) );
                    Send( 6, GetBytes( m_FpsLocal.transform.rotation ) );
                }

                SendPing();

                Receive();

                if (Time.realtimeSinceStartup > lastPing + 2.0f)
                    Close();
            }
            catch
            {
                Close();
            }
        }
	}
    
    #region Utils Server

    private void Close()
    {
        m_IsConnected = false;
        m_showConnect = false;

        if( !m_IsServer )
            SceneManager.LoadScene( 1 );

        m_gameManager.m_uiCanvas.LoadNewSMS( "Jack is gone :/" );

        if( m_IsServer )
        {
            ResetNetwork();
            m_IsServer = true;

            InitServer();
        }
    }

    private void InitServer()
    {
        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 7777);
        
        m_socket = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

        m_socket.SetSocketOption( SocketOptionLevel.Socket, SocketOptionName.DontLinger, true );
        m_socket.SetSocketOption( SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true );

        m_socket.Bind( localEndPoint );

        m_socket.Listen( 100 );
        
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
        m_msgconnection.text = "Starting Server ...";
        StartCoroutine( "WaitAndRemoveUIConnection", 1.0F );
    }
    #endregion

    #region Utils Client
    private void InitClient()
    {
        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(m_Ip), 7777);

        m_socket = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

        m_msgconnection.text = "Try connect to " + m_Ip;

        try
        {
            m_socket.Connect( localEndPoint );

            if( m_socket.Connected )
            {
                Debug.Log( "Connected" );
                m_IsConnected = true;

                m_UIGSM.SetActive( false );

                m_FpsIcon = Instantiate( m_prefabFpsIcon );
                m_Monitor = Instantiate( m_prefabMonitor );

                SendObjectif( GameManager.m_player2Objective );
                StartCoroutine( "WaitAndRemoveUIConnection", 1.0F );
                RequestMapInfo();
            }
        }
        catch
        {
            Debug.Log( "Time Out !" );
            SceneManager.LoadScene( 1 );
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
            //m_gameManager.m_uiCanvas.LoadNewSMS( "objectif changed : " + GameManager.m_player2Objective );
        }
        else
        if( _packet[ 0 ] == 4 ) //Player VR is dead
        {
            GameManager.GameOver();
        }
        else
        if( _packet[ 0 ] == 5 ) //Set Color Rooms
        {
            m_mapManager.ApplyIconColor( _packet[ 1 ], _packet[ 2 ] );
        }
        else
        if( _packet[ 0 ] == 6 ) //FPS Rotation
        {
            Quaternion rotation = GetQuaternion( SubPacket( _packet, 1, _packet.Length - 1 ) );
            m_FpsIcon.transform.rotation = rotation;
        }
        else
        if( _packet[ 0 ] == 7 ) //Win Player
        {
            GameManager.m_player1Win = true;
        }
        else
        if ( _packet[0] == 8) //Send Map info
        {
            for( int i = 0; i < m_mapManager.m_rooms.Count; i++ )
                SendTypeRoom( i, (int)m_mapManager.m_rooms[ i ].RoomType );
        }
        else
        if( _packet[ 0 ] == 9 ) //ping
        {
            lastPing = Time.realtimeSinceStartup;
        }
    }

    public float lastPing; 
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

    public void SendTypeRoom(int _room, int _type)
    {
        Send( 5, new byte[] { (byte)_room, (byte)_type } );
    }

    public void SendPlayerWin()
    {
        Send( 7, new byte[] { 0 } );
    }

    public void RequestMapInfo()
    {
        Send( 8, new byte[] { 0 } );
    }

    public void SendPing()
    {
        Send( 9, new byte[] { 0 } );
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

    private static byte[] GetBytes( Quaternion _value )
    {
        byte[] result = new byte[16];

        GetBytes( _value.x ).CopyTo( result, 0 );
        GetBytes( _value.y ).CopyTo( result, 4 );
        GetBytes( _value.z ).CopyTo( result, 8 );
        GetBytes( _value.w ).CopyTo( result, 12 );

        return result;
    }

    private static Vector3 GetVector3(byte[] _value)
    {
        float x = GetFloat( SubPacket( _value, 0, 4 ));
        float y = GetFloat( SubPacket( _value, 4, 4 ));
        float z = GetFloat( SubPacket( _value, 8, 4 ));

        return new Vector3( x, y, z);
    }

    private static Quaternion GetQuaternion( byte[] _value )
    {
        float x = GetFloat( SubPacket( _value, 0, 4 ));
        float y = GetFloat( SubPacket( _value, 4, 4 ));
        float z = GetFloat( SubPacket( _value, 8, 4 ));
        float w = GetFloat( SubPacket( _value, 12, 4 ));

        return new Quaternion( x, y, z, w );
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
        m_gameManager.m_uiCanvas.LoadNewSMS( "Server Started" );
        InitFpsPlayer();
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
    
    public void CloseSocket()
    {
        if (m_socket.Connected)
        {
            m_socket.Shutdown( SocketShutdown.Both );
            m_socket.Disconnect( true );
        }
    }

    private IEnumerator WaitAndRemoveUIConnection( float _waitTime )
    {
        yield return new WaitForSeconds( _waitTime );
        m_ConnectionFrame.SetActive( false );
        
        if (Global.Server)
            m_FpsLocal = (GameObject)Instantiate( m_prefabFpsLocal, Global.playerSpawnPosition, Quaternion.identity );
    }
    
    private Socket m_socket;
    private int m_lenghtPacket;
    
    private GameObject m_FpsLocal;
    private GameObject m_FpsIcon;
    private GameObject m_Monitor;

    private Text m_msgconnection;
    private bool m_showConnect;
}
