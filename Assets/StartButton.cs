using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {


    #region Public and Protected Members
    public VRInput m_vrInput;

    public AudioSource  m_clickSoundSource;
    public AudioClip m_clickSound;
    public Animator m_viewAnim;
    public Animator m_connectViewAnim;
    public Animator m_canvasAnim;




    #endregion

    #region Main Methods

    void Awake()
    {
        m_interactiveElement = GetComponent<VRInteractiveElement>();
    }
    // Use this for initialization
    void Start()
    {
        
        m_click = Instantiate( m_clickSoundSource ) as AudioSource;
        m_click.GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        m_interactiveElement.OnOverEvent += HandleOverEvent;
        m_interactiveElement.OnOutEvent += HandleOutEvent;

        m_interactiveElement.OnClickEvent += HandleClick;
        //m_vrInput.OnClickEvent += HandleClick;
        //m_vrInput.OnDoubleClickEvent += HandleDoubleClick;
    }

    void OnDisable()
    {
        m_interactiveElement.OnOverEvent -= HandleOverEvent;
        m_interactiveElement.OnOutEvent -= HandleOutEvent;

        

        //m_vrInput.OnClickEvent -= HandleClick;
        //m_vrInput.OnDoubleClickEvent -= HandleDoubleClick;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void HandleOverEvent()
    {
        Debug.Log( "Over On " + gameObject.name );
    }
    void HandleOutEvent()
    {
        Debug.Log( "Out form " + gameObject.name );
    }

    void HandleClick()
    {
        Debug.Log( "Click On " + gameObject.name );
        if( m_click != null )
        {
            m_click.Play();
        }
        m_viewAnim.SetTrigger( "DownView" );
        m_connectViewAnim.SetTrigger( "UpView" );
    }

    void HandleDoubleClick()
    {

    }
    #endregion

    #region Utils

    #endregion

    private AudioSource m_click;
    private VRInteractiveElement m_interactiveElement;

}
