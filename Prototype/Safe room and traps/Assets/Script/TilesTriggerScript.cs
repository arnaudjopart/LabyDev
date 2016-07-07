using UnityEngine;
using System.Collections;

public class TilesTriggerScript : MonoBehaviour {

    #region Public and Protected Members
    public GameObject m_tile;
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
    void OnTriggerEnter( Collider _col )
    {
        Tiles c = m_tile.GetComponent<Tiles>();
        c.m_triggered = true;
        //if(_col.gameObject.name == "FPSController")
        //{
            
        //}
    }
    #endregion

    #region Private Members

    #endregion


}
