using UnityEngine;
using System.Collections;

public class ScriptPiegePlafond : MonoBehaviour
{
    #region Public Variable

    public Transform m_floor;
    public Vector3 m_downTrap;
    public bool m_activeTrap;

    #endregion

    #region Main Methode
    void Start()
    {
        m_activeTrap = false;
    }
  
    void Update()
    {
        if(m_activeTrap==true&&m_floor.position.y>0.6)
        {
            m_floor.position -= m_downTrap;
        }
      
    }

    #endregion
}
