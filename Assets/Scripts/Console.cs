using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    InputField m_console;
    
	void Start ()
    {
        m_console = GetComponent<InputField>();
    }
	
	void Update ()
    {
	    if (Input.GetKeyDown(KeyCode.Return) && m_console.text != "" )
        {
            NetworkManager.m_instance.SendSms( m_console.text );
            m_console.text = "";
        }
	}
}
