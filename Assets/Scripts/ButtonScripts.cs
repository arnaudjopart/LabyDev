using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
    public int buttonId;
    public InputField m_IpEntry;

    public Animator m_viewAnim;
    public Animator m_connectViewAnim;

    void Start ()
    {
        GetComponent<Button>().onClick.AddListener( () => Clicked() );
    }

    void Clicked()
    {
        if( buttonId == -1 )
        {
            Debug.Log( "Quit Game" );
            Application.Quit();
        }

        if ( buttonId == 0)
        {
            m_viewAnim.SetTrigger( "DownView" );
            m_connectViewAnim.SetTrigger( "UpView" );
        }


        if (buttonId == 1)
        {
            Global.Server = true;
            SceneManager.LoadScene( 1 );
        }

        if( buttonId == 2 ) 
        {
            Global.Server = false;

            if( m_IpEntry.text != "" )
                Global.Ip = m_IpEntry.text;

            SceneManager.LoadScene( 1 );
        }

        if( buttonId == 3 ) // Retry - implements reload a connection / retry a game
        {
            SceneManager.LoadScene( 1 );
        }

        if( buttonId == 4 ) // back to the menu
        {
            SceneManager.LoadScene( 0 );
        }
    }
}
