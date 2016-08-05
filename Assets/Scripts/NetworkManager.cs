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

    public Transform m_CamTransform;

    void Start()
    {
        m_instance = this;

        m_msgconnection = m_ConnectionFrame.GetComponentInChildren<Text>();

        StartClient( Global.Ip );
    }

    Vector3 lastPosSended;
    Quaternion lastRotSended;
    int lastObjectifSend;

    void Update()
    {
        if( m_IsConnected )
        {
            try
            {
                Receive();

                if( m_IsServer )
                {
                    if( !GameManager.IsGameOver )
                    {
                        if( m_CamTransform.position != lastPosSended )
                        {
                            lastPosSended = m_CamTransform.position;
                            Send( 1, GetBytes( lastPosSended ) );
                        }

                        if( m_CamTransform.rotation != lastRotSended )
                        {
                            lastRotSended = m_CamTransform.rotation;
                            Send( 6, GetBytes( lastRotSended ) );
                        }
                    }
                }
                else
                {
                    if( lastObjectifSend != GameManager.m_player2Objective )
                    {
                        lastObjectifSend = GameManager.m_player2Objective;
                        SendObjectif( lastObjectifSend );
                    }
                }
            }
            catch( Exception _e )
            {
                //close();
                //SceneManager.LoadScene( 1 );
            }
        }
    }

    private void Receive()
    {
        while( m_socket.Available > 0 )
        {
            if( m_lenghtPacket != -1 )
            {
                if( m_socket.Available >= m_lenghtPacket )
                {
                    byte[] data = new byte[m_lenghtPacket];
                    m_socket.Receive( data );

                    if( data.Length > 0 )
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
        if( _packet[ 0 ] == 1 ) //FPS Pos
        {
            Vector3 pos = GetVector3( SubPacket( _packet, 1, _packet.Length - 1 ) );
            pos.y = 100;

            m_FpsIcon.transform.position = pos;
        }
        else
        if( _packet[ 0 ] == 2 ) //Sms
        {
            string sms = Encoding.ASCII.GetString( SubPacket( _packet, 1, _packet.Length - 1 ) );

            m_gameManager.m_uiCanvas.LoadNewSMS( sms );
        }
        else
        if( _packet[ 0 ] == 3 ) //objectif state
        {
            GameManager.m_player2Objective = _packet[ 1 ];
            //m_gameManager.m_uiCanvas.LoadNewSMS( "objectif changed : " + GameManager.m_player2Objective );
        }
        else
        if( _packet[ 0 ] == 4 ) //Player VR is dead
        {
            if( !m_IsServer )
                SendGameOver();

            GameManager.GoGameOverScene();
        }
        else
        if( _packet[ 0 ] == 5 ) //Set Color Rooms
        {
            m_ConnectionFrame.SetActive( false );
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
        if( _packet[ 0 ] == 8 ) //Send Map info
        {
            m_gameManager.m_uiCanvas.LoadNewSMS( "Jack is Here !" );

            for( int i = 0; i < m_mapManager.m_rooms.Count; i++ )
                SendTypeRoom( i, (int)m_mapManager.m_rooms[ i ].RoomType );

            Send( 1, GetBytes( lastPosSended ) );
            Send( 6, GetBytes( lastRotSended ) );
        }
        else
        if( _packet[ 0 ] == 15 ) //Linked pLayer Gone
        {
            if( m_IsServer )
                m_gameManager.m_uiCanvas.LoadNewSMS( "Jack is gone :/" );
            else
            {
                close();
                SceneManager.LoadScene( 1 );
            }
        }
    }

    private void StartClient(string _Ip)
    {
        m_Ip = _Ip;
        m_IsServer = Global.Server;

        m_lenghtPacket = -1;

        InitClient();
    }

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

                if( m_IsServer )
                {
                    StartCoroutine( "WaitAndRemoveUIConnection", 1.0F );
                }
                else
                {
                    m_IsConnected = true;
                    m_msgconnection.text = "En Attente d'un Player1";

                    m_UIGSM.SetActive( false );
                    m_FpsIcon = Instantiate( m_prefabFpsIcon );
                    m_Monitor = Instantiate( m_prefabMonitor );

                    Send( 22, new byte[] { 0 } );
                }
            }
        }
        catch
        {
            Debug.Log( "Time Out !" );
            SceneManager.LoadScene( 1 );
        }
    }

    #region Send
    private void Send(byte Id, byte[] _data)
    {
        if( m_IsConnected )
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

    public void close()
    {
        m_IsConnected = false;
        m_socket.Close();
        m_lenghtPacket = -1;
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
    #endregion

    #region Utils
    private static byte[] AddPacket(byte[] _packet1, byte[] _packet2)
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

    private static byte[] GetBytes(float _value)
    {
        return BitConverter.GetBytes( _value );
    }

    private static float GetFloat(byte[] _value)
    {
        return System.BitConverter.ToSingle( _value, 0 );
    }

    private static byte[] GetBytes(Vector3 _value)
    {
        byte[] result = new byte[12];

        GetBytes( _value.x ).CopyTo( result, 0 );
        GetBytes( _value.y ).CopyTo( result, 4 );
        GetBytes( _value.z ).CopyTo( result, 8 );

        return result;
    }

    private static byte[] GetBytes(Quaternion _value)
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

        return new Vector3( x, y, z );
    }

    private static Quaternion GetQuaternion(byte[] _value)
    {
        float x = GetFloat( SubPacket( _value, 0, 4 ));
        float y = GetFloat( SubPacket( _value, 4, 4 ));
        float z = GetFloat( SubPacket( _value, 8, 4 ));
        float w = GetFloat( SubPacket( _value, 12, 4 ));

        return new Quaternion( x, y, z, w );
    }
    #endregion

    private IEnumerator WaitAndRemoveUIConnection(float _waitTime)
    {
        yield return new WaitForSeconds( _waitTime );
        m_ConnectionFrame.SetActive( false );

        if( Global.Server )
        {
            m_FpsLocal = (GameObject)Instantiate( m_prefabFpsLocal, Global.playerSpawnPosition, Quaternion.identity );
            m_IsConnected = true;
            Send( 22, new byte[] { 1 } );
            m_CamTransform = Camera.main.GetComponent<Transform>();
        }
    }

    private Socket m_socket;
    private int m_lenghtPacket;

    private GameObject m_FpsLocal;
    private GameObject m_FpsIcon;
    private GameObject m_Monitor;

    private Text m_msgconnection;
    private bool m_showConnect;
}
