using UnityEngine;
using System.Collections;
using System.Text;

// Enumeration : Room Type
public enum TypeRoom {
    DefaultRoom = 0, 
    SafeRoom = 1,
    TrapRoom = 2,
    StartRoom = 3
}
public class Room {

    #region Public and Protected Members
    public string Name { get { return m_name; } set { m_name = value; } }
    public TypeRoom RoomType { get { return m_RoomType; } set { m_RoomType = value; } }
    public Transform Transform { get { return m_transform; } }
    public MeshRenderer MeshRenderer { get { return m_meshRenderer; } set { m_meshRenderer = value; } }
    #endregion

    #region Main Methods
    public Room() { }
    public Room( Room _room ) {
        m_transform = _room.m_transform;
        m_meshRenderer = _room.m_meshRenderer;
        m_name = _room.m_name;
        m_RoomType = _room.m_RoomType;
    }
    public Room(Transform _transform, MeshRenderer _meshRenderer, string _name) {
        m_transform = _transform;
        m_meshRenderer = _meshRenderer;
        m_name = _name;
        m_RoomType = 0;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat( "Name : {0}\n", Name );
        sb.AppendFormat( "Type Room : {0}\n", RoomType );
        sb.AppendFormat( "Transform [x : {0}, y : {1}, z : {2}]\n", 
            Transform.position.x.ToString(), 
            Transform.position.y.ToString(), 
            Transform.position.z.ToString() );
        sb.AppendFormat( "MeshRenderer [Color : {0}]\n", MeshRenderer.materials[ 0 ].color );
        return sb.ToString();
    }
    #endregion

    #region Utils
    
    #endregion

    #region Private Members
    private Transform m_transform;
    private MeshRenderer m_meshRenderer;
    private string m_name;
    private TypeRoom m_RoomType;
    #endregion
}
