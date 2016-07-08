using UnityEngine;
using System.Collections;

public class SphereScript : MonoBehaviour {



    #region Public and Protected Members
    public int m_lifeTime = 100;
    #endregion

    #region Main Methods
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(m_lifeTime == 0)
        {
            Destroy( this.gameObject );
        }
        m_lifeTime--;
    }
    #endregion

    #region Utils

    #endregion

    #region Private Members
    
    #endregion
}
