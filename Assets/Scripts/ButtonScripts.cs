using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
    public int buttonId;
    public InputField m_IpEntry;
    public AudioSource  m_clickSoundSource;
    public AudioClip m_clickSound;
    public Animator m_viewAnim;
    public Animator m_connectViewAnim;


    void Start()
    {
        GetComponent<Button>().onClick.AddListener( () => Clicked() );
        m_click = Instantiate( m_clickSoundSource ) as AudioSource;
        m_click.GetComponent<AudioSource>();
    }

    void Clicked()
    {
        if( buttonId == -1 )
        {
            if( m_click != null )
            {
                m_click.Play();
            }
            Debug.Log( "Quit Game" );
            Application.Quit();
        }

        if( buttonId == 0 )
        {
            if( m_click != null )
            {
                m_click.Play();
            }
            m_viewAnim.SetTrigger( "DownView" );
            m_connectViewAnim.SetTrigger( "UpView" );
        }


        if( buttonId == 1 )
        {

            m_click.Play();

            Global.Server = true;
            StartCoroutine( LoadScene( 1, 2f ) );
        }

        if( buttonId == 2 )
        {
            if( m_click != null )
            {
                m_click.Play();
            }
            Global.Server = false;

            if( m_IpEntry.text != "" )
                Global.Ip = m_IpEntry.text;

            StartCoroutine( LoadScene( 1, 2f ) );
        }

        if( buttonId == 3 ) // Retry - implements reload a connection / retry a game
        {
            if( m_click != null )
            {
                m_click.Play();
            }

            StartCoroutine( LoadScene( 1, 2f ) );

            Debug.Log( "Retry the game" );
        }

        if( buttonId == 4 ) // back to the menu
        {
            if( m_click != null )
            {
                m_click.Play();
            }
            StartCoroutine( LoadScene( 0, 2f ) );
        }
    }

    IEnumerator LoadScene(int index, float delayTime)
    {
        yield return new WaitForSeconds( delayTime );
        SceneManager.LoadScene( index );
    }

    private AudioSource m_click;
}