using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UICanvas : MonoBehaviour {
    //blic Text m_sms;
    public Text m_TextMessagePrefab;
    public GameObject m_historic;

    public static UICanvas s_instance;

	// Use this for initialization
	void Start () {
        //m_sms = transform.GetComponentInChildren<Text>();

        if( !s_instance )
            s_instance = this;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadNewSMS(string _text)
    {
        Text newText = Instantiate( m_TextMessagePrefab,transform.position,Quaternion.identity ) as Text;
        newText.transform.SetParent( m_historic.transform, false );
        newText.text = _text;     
        //m_sms.text = _text;
    }

}
