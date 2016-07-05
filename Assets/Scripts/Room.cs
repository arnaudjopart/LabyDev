using UnityEngine;
using System.Collections;

public enum TypeRoom {
    defaultRoom = 0, 
    safeRoom = 1,
    dangerRoom = 2,
    startRoom
}
public class Room {

    #region Public and Protected Members
    public string Name { get { return m_name; } set { m_name = value; } }
    public TypeRoom RoomType { get { return m_RoomType; } set { m_RoomType = value; } }
    public Transform Transform { get { return m_transform; } }
    #endregion

    #region Main Methods
    public Room() { }
    public Room(Transform _transform, string _name) {
        m_transform = _transform;
       
        m_name = _name;
        m_icon = _transform.GetChild(0);

        m_RoomType = 0;
    }

    public override string ToString()
    {
        return Name  + " - Position : " + Transform.position;
    }
    #endregion

    #region Utils

    #endregion

    #region Private Members
    private Transform m_transform;
    private string m_name;
    private TypeRoom m_RoomType;
    public Transform m_icon;
    #endregion
}
