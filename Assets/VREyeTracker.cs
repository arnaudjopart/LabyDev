using UnityEngine;
using System.Collections;

public class VREyeTracker : MonoBehaviour {

    #region Public and Proetected Members
    public LayerMask m_layerMask;
    public VRInput m_vrInput;
    public float m_maxDistance;
        
    #endregion
    // Use this for initialization
    void Start () {
        m_transform = GetComponent<Transform>();
	}
	void Awake()
    {
        m_vrInput.OnClickEvent += HandleClick;
        m_vrInput.OnDoubleClickEvent += HandleDoubleClick;
    }
	// Update is called once per frame
	void Update () {

        Ray eyeTarcker = new Ray(m_transform.position, m_transform.forward);

        if( Physics.Raycast( eyeTarcker, out m_hit, m_maxDistance, m_layerMask )){

            VRInteractiveElement interactive = m_hit.collider.GetComponent<VRInteractiveElement>();
            
            if(interactive && interactive != m_lastInteractiveElement )
            {
                RemoveLastInteractiveElemnt();
                interactive.OnOver();
            }

            m_lastInteractiveElement = interactive;
            m_currentInteractiveElement = interactive;


        }else
        {
            RemoveLastInteractiveElemnt();
            m_currentInteractiveElement = null;
            
            

        }
	}

    private void HandleClick()
    {
        if( m_currentInteractiveElement != null )
        {
            m_currentInteractiveElement.OnClick();
        }

    }

    private void HandleDoubleClick()
    {
        m_currentInteractiveElement.OnDoubleClick();
    }

    #region Utils

    void RemoveLastInteractiveElemnt()
    {
        if( m_lastInteractiveElement == null )
        {
            return;
        }
        m_lastInteractiveElement.OnOut();
        m_lastInteractiveElement = null;

    }
    #endregion
    #region Private variables

    private RaycastHit m_hit;
    private Transform m_transform;

    private VRInteractiveElement m_currentInteractiveElement;
    private VRInteractiveElement m_lastInteractiveElement;



    #endregion
}
