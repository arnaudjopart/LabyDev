using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour {

    #region Public and Protected Members
    public GameObject m_cube;
    
    #endregion

    #region Main Methods

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Utils
    void OnTriggerEnter(Collider _col )
    {
        //if(_col.gameObject.name == "FPSController")
        //{
            
        //}
        CubeScript c = m_cube.GetComponent<CubeScript>();
        c.m_triggered = true;
    }
    #endregion

    #region Private Members
    
    #endregion

}
