using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    InputField m_console;
    public Text m_objectif;
    
	void Start ()
    {
        m_console = GetComponent<InputField>();
    }
	
	void Update ()
    {
        if( GameManager.m_player2Objective == 1 && m_objectif.text != "Save Dexter" )
        {
            m_objectif.text = "Save Dexter";
        }
        else
        {
            if ( m_objectif.text != "Kill Dexter")
                m_objectif.text = "Kill Dexter";
        }
        
        if (Input.GetKeyDown(KeyCode.Return) && m_console.text != "" )
        {
            NetworkManager.m_instance.SendSms( m_console.text );
            m_console.text = "";
        }
	}
}
