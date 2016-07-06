using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UICanvas : MonoBehaviour {
    public Text m_TextMessagePrefab;
    public GameObject m_historic;

   
    public float m_time;
    public float m_timeMax;
    public Image m_energy1;
    public Image m_energy2;
    public Image m_energy3;
    public Image m_energy4;
    public Image m_energy5;
    public GameManager m_gameManager;

    // Use this for initialization
    void Start () {
        m_gameManager = GameObject.FindObjectOfType<GameManager>();
        m_timeMax = m_gameManager.m_gameTimeInSeconds;


        //LoadNewSMS( "Lorem ipsums dolor..." );

    }

    // Update is called once per frame
    void Update() {
        m_time = m_gameManager.m_gameTimeInSeconds;
        if ( m_time  < m_timeMax-m_timeMax/5 )
        {
            m_energy5.color = Color.clear;
        }
        if ( m_time  < m_timeMax - 2*m_timeMax / 5 )
        {
            m_energy4.color = Color.clear;
        }
        if ( m_time  < m_timeMax -3*m_timeMax / 5  )
        {
            m_energy3.color = Color.clear;
        }
        if ( m_time  < m_timeMax - 4 * m_timeMax / 5 )
        {
            m_energy2.color = Color.clear;
        }
        if ( m_time  <= 0 )
        {
            m_energy1.color = Color.clear;
            
        }
        Debug.Log( m_time );
    }

    public void LoadNewSMS(string _text)
    {
        Text newText = Instantiate( m_TextMessagePrefab,Vector3.zero,Quaternion.identity ) as Text;
        newText.transform.SetParent( m_historic.transform, false );
        newText.text = _text;     
        
    }

}
