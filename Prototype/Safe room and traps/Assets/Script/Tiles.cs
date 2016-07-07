using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tiles : MonoBehaviour {



    #region Public and Protected Members
    //public GameObject [] m_tabTiles;
    public float m_moveSpeed = 1f;
    public float m_minRangeRrd = 0.0001f;
    public float m_maxRangeRrd = 0.003f;

    public bool m_triggered = false;

    public List<GameObject> m_tab;
    public List<TilesEvolution> m_tabTiles;
    #endregion

    #region Main Methods
    // Use this for initialization
    void Start()
    {
        m_tab = new List<GameObject>();
        m_tabTiles = new List<TilesEvolution>();
        int count = 0;
        foreach(Transform t in transform)
        {
            //m_tabTiles[count] = t.gameObject;
            foreach(Transform item in t)
            {
                m_tab.Add( item.gameObject );
                if(Random.Range( 0, 2 ) == 1)
                {
                    float tmp = Random.Range(m_minRangeRrd,m_maxRangeRrd);
                    m_tabTiles.Add( new TilesEvolution { m_speed = tmp * m_moveSpeed, m_up = true, m_tiles = item.gameObject } );

                }
                else
                {
                    float tmp = Random.Range(m_minRangeRrd,m_maxRangeRrd);
                    m_tabTiles.Add( new TilesEvolution { m_speed = tmp * m_moveSpeed, m_up = false, m_tiles = item.gameObject } );
                }
                

            }
            count++;
        }
            

    }

    // Update is called once per frame
    void Update()
    {
        if(m_triggered)
        {
            foreach(TilesEvolution item in m_tabTiles)
            {
                Vector3 tmp = item.m_tiles.transform.position;
                if(item.m_up)
                {
                    tmp.y += item.m_speed;
                }
                else
                {
                    tmp.y -= item.m_speed;
                }
                item.m_tiles.transform.position = tmp;
            }
        }
        
    }
    #endregion

    #region Utils

    #endregion

    #region Private Members
    private GameObject m_tiles;

    
    #endregion
}

public class TilesEvolution
{
    public bool m_up { get; set; }
    public float m_speed { get; set; }
    public GameObject m_tiles { get; set; }
}
