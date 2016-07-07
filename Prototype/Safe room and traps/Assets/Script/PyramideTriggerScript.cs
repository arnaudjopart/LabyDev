using UnityEngine;
using System.Collections;

public class PyramideTriggerScript : MonoBehaviour {

    #region Public and Protected Members
    public GameObject m_sphereEmitter;
    public GameObject m_triangle;
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
        TriangleScript t = m_triangle.GetComponent<TriangleScript>();
        t.m_triggered = true;

        SphereEmitterScript s = m_sphereEmitter.GetComponent<SphereEmitterScript>();
        s.m_triggered = true;
    }
    #endregion

    #region Private Members

    #endregion
}
