using UnityEngine;
using System.Collections;

public class SphereEmitterScript : MonoBehaviour {

    #region Public and Protected Members
    public bool m_triggered = false;
    #endregion

    #region Main Methods
    public GameObject m_sphere;

    public Transform m_parent;
    public int m_timerSpawn = 100;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(m_triggered)
        {
            if(m_counter == 0)
            {
                m_counter = Random.Range( 10, m_timerSpawn );

                float tmpX = Random.Range( -1f, 1f );
                float tmpZ = Random.Range( -1f, 1f );
                Vector3 v3 = new Vector3(tmpX, m_parent.position.y +10, tmpZ);

                GameObject temp = Instantiate(m_sphere, Vector3.zero, Quaternion.identity) as GameObject;
                temp.transform.SetParent( m_parent, false );
                temp.transform.localPosition = v3;
            }
            m_counter--;
        }
    }
    #endregion

    #region Utils

    #endregion

    #region Private Members
    private int m_counter;
    #endregion
}
