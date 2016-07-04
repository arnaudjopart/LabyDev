using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InitiateMapManager : MonoBehaviour {
    #region Public and Protected Members

    #endregion

    #region Main Methods
    // Use this for initialization
    void Start()
    {       
       foreach(  Transform item in transform )
        {
            m_rooms.Add( new Room(item, item.GetComponent<MeshRenderer>(), item.name ));
        }
        InitiateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region Utils
    private void debugInfoRoom() {
        Debug.Log( "Type de room : " );

        Debug.Log( "Position du player : " );
        Debug.Log( m_startRoom );
        Debug.Log( " ---------------------- " );
        Debug.Log( "Positions des Safes Room : " );
        foreach( var room in m_safeRooms )
        {
            Debug.Log( room );
        }
        Debug.Log( " ---------------------- " );
        Debug.Log( "Positions des traps room : " );
        foreach( var room in m_trapRooms )
        {
            Debug.Log( room );
        }
    }
    /// <summary>
    /// Initialise map :
    /// - The player starts in a corner at random
    /// - x Safe Room in the three remaining corners
    /// - x Trap Room in the remaining rooms
    /// <Const param name="m_nbSafeRoom"> Safe Room </param>
    /// <Const param name="m_nbTrapRoom"> Trap Room </param>
    /// </summary>
    private void InitiateMap() {
        int tmpIdx;
        Color tmpColor;

        // List index room
        List<int> idxToDistribute = new List<int>(m_rooms.Count);
        for( int i = 0; i < m_rooms.Count; i++ )
        {
            idxToDistribute.Add( i );
        }

        // Recupererer les coinsD
        List<int> idxCornerMap = new List<int>();
        idxCornerMap.Add( 0 );
        idxCornerMap.Add( m_rooms.Count - 1 );
        idxCornerMap.Add( m_mapWidth - 1);
        idxCornerMap.Add((m_rooms.Count  - m_mapWidth  ));
        //Debug.Log( idxCornerMap[ 0 ] + " " + idxCornerMap[ 1 ] + " " + idxCornerMap[ 2 ] + " " + idxCornerMap[ 3 ] + " " );

        // Prototypage map :
        // Start Room
        tmpIdx = Random.Range( 0, idxCornerMap.Count );
        m_rooms[ idxCornerMap[tmpIdx] ].RoomType = TypeRoom.StartRoom;
        m_startRoom = m_rooms[ idxCornerMap[ tmpIdx ] ];
        idxToDistribute[idxCornerMap [tmpIdx] ] = -1;
        idxCornerMap[tmpIdx ] = -1;

        // Safe Room
        for( int i = 0; i < m_nbSafeRoom; i++ )
        {
            do
                tmpIdx = Random.Range( 0, idxCornerMap.Count );
            while( idxCornerMap[ tmpIdx ] == -1 );
            m_rooms[ idxCornerMap[ tmpIdx ] ].RoomType = TypeRoom.SafeRoom;
            m_safeRooms.Add( new Room( m_rooms[ idxCornerMap[ tmpIdx ] ] ) );
            idxToDistribute[idxCornerMap[ tmpIdx ]] = -1;
            idxCornerMap[tmpIdx ] = -1;
        }

        // Trap Room
        for( int i = 0; i < m_nbTrapRoom; i++ )
        {
            do
                tmpIdx = Random.Range( 0, idxToDistribute.Count );
            while( idxToDistribute[ tmpIdx ] == -1 );
            m_rooms[ idxToDistribute[ tmpIdx ] ].RoomType = TypeRoom.TrapRoom;
            m_trapRooms.Add( new Room( m_rooms[ idxToDistribute[ tmpIdx ] ] ) );
            idxToDistribute[ tmpIdx ] = -1;
        }

        // Colorisation Room
        for( int i = 0; i < m_rooms.Count; i++ )
        {
            switch( m_rooms[i].RoomType )
            {
                case TypeRoom.DefaultRoom: tmpColor = Color.gray;
                    break;
                case TypeRoom.SafeRoom: tmpColor = Color.green;
                    break;
                case TypeRoom.TrapRoom: tmpColor = Color.red;
                    break;
                case TypeRoom.StartRoom: tmpColor = Color.white;
                    break;
                default: tmpColor = Color.black;
                    break;
            }
            m_rooms[ i ].MeshRenderer.materials[ 0 ].SetColor( "_Color", tmpColor );
        }

        // Debug Infos Room
        debugInfoRoom();
    }
    
    #endregion

    #region Private Members
    private List<Room> m_rooms = new List<Room>();
    private List<Room> m_safeRooms = new List<Room>();
    private List<Room> m_trapRooms = new List<Room>();
    private Room m_startRoom = new Room();

    // ---------------------------------
    private const int m_nbSafeRoom = 2;
    private const int m_nbTrapRoom = 10;
    private const int m_mapWidth = 8;
    private const int m_mapHeigt = 4;

    #endregion

}
