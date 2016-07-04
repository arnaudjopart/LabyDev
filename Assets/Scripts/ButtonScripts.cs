using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
    public int buttonId;
    public InputField m_IpEntry;

	void Start ()
    {
        GetComponent<Button>().onClick.AddListener( () => Clicked() );
    }

    void Clicked()
    {
        if ( buttonId == 0)
        {
            Global.Server = true;
            SceneManager.LoadScene( 1 );
        }

        if( buttonId == 1 )
        {
            Global.Server = false;

            if( m_IpEntry.text != "" )
                Global.Ip = m_IpEntry.text;

            SceneManager.LoadScene( 1 );
        }
    }
}
