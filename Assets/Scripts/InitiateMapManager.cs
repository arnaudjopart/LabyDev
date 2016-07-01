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
        //debugInfoRoom();
        InitiateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region Utils
    private void debugInfoRoom() {
        foreach( var item in m_rooms )
        {
            Debug.Log( item.ToString() );
        }
    }
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

        // Start Room
        tmpIdx = Random.Range( 0, idxCornerMap.Count );
        m_rooms[ idxCornerMap[tmpIdx] ].RoomType = TypeRoom.startRoom;
        idxToDistribute[idxCornerMap [tmpIdx] ] = -1;
        idxCornerMap[tmpIdx ] = -1;

        // Safe Room
        for( int i = 0; i < m_nbSafeRoom; i++ )
        {
            do
                tmpIdx = Random.Range( 0, idxCornerMap.Count );
            while( idxCornerMap[ tmpIdx ] == -1 );
            m_rooms[ idxCornerMap[ tmpIdx ] ].RoomType = TypeRoom.safeRoom;
            idxToDistribute[idxCornerMap[ tmpIdx ]] = -1;
            idxCornerMap[tmpIdx ] = -1;
        }

        // Danger Room
        for( int i = 0; i < m_nbDangerRoom; i++ )
        {
            do
                tmpIdx = Random.Range( 0, idxToDistribute.Count );
            while( idxToDistribute[ tmpIdx ] == -1 );

            m_rooms[ idxToDistribute[ tmpIdx ] ].RoomType = TypeRoom.dangerRoom;
            idxToDistribute[ tmpIdx ] = -1;

        }
        //int[] idxSafeRoom = new int[m_nbSafeRoom];
        //int[] idxDangerRoom = new int[m_nbDangerRoom];
        // StartPlayer

        for( int i = 0; i < m_rooms.Count; i++ )
        {
            switch( m_rooms[i].RoomType )
            {
                case TypeRoom.defaultRoom: tmpColor = Color.gray;
                    break;
                case TypeRoom.safeRoom: tmpColor = Color.green;
                    break;
                case TypeRoom.dangerRoom: tmpColor = Color.red;
                    break;
                case TypeRoom.startRoom: tmpColor = Color.white;
                    break;
                default: tmpColor = Color.black;
                    break;
            }
            m_rooms[ i ].MeshRenderer.materials[ 0 ].SetColor( "_Color", tmpColor );
        }
    }
    
    #endregion

    #region Private Members
    private List<Room> m_rooms = new List<Room>();
    private Transform m_startRoom;

    // ---------------------------------
    private const int m_nbSafeRoom = 2;
    private const int m_nbDangerRoom = 10;
    private const int m_mapWidth = 8;
    private const int m_mapHeigt = 4;

    #endregion

}
