using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Public and Protected Members
    public static bool m_PlayerIsAlive;
    public static bool m_player1Win;
    public static bool IsGameOver = false;
    public int m_gameTimeInSeconds;
    public static int m_player2Objective; 
    
    public UICanvas m_uiCanvas;
    public NetworkManager m_networkManager;

    public static GameManager s_instance;

    #endregion

    #region Main Methods

    void Awake()
    {
        if ( !s_instance )
        {
            s_instance = this;
        }
    }
    
    public static void GameOver()
    {
        IsGameOver = true;

        Debug.Log( "GameOver" );
        
        if( Global.Server )
        {
            if(!m_player1Win )
            {
                SoundScript.PlayDeathSound();
            }
            
            GameManager.s_instance.m_networkManager.SendGameOver();
        }

        //NetworkManager.m_instance.CloseSocket();

        GameManager.s_instance.Invoke( "LoadGameOverScene", 1.5f );        
    }

    void Start()
    {
        IsGameOver = false;

        if (!Global.Server)
        {
            m_player2Objective = Random.Range( 0, 2 );
            StartCoroutine( But() );
        }
        else
        {
            StartCoroutine( GameTick() );
        }
    }

    void Update()
    {

    }
    #endregion

    #region Utils
    IEnumerator But()
    {
        yield return new WaitForSeconds( Random.Range( 30, 80 ) );
        if ( m_player2Objective == 0 )
        {
            m_player2Objective = 1;
        }
        else
        {
            m_player2Objective = 0;
        }

        m_networkManager.SendObjectif( m_player2Objective );
    }

    IEnumerator GameTick()
    {
        while( m_gameTimeInSeconds > 0 )
        {
            yield return new WaitForSeconds( 1f );
            m_gameTimeInSeconds--;
        }
        GameOver();
    }

    private void LoadGameOverScene()
    {
        SceneManager.LoadScene( 3 );
    }
    #endregion


    #region Private Members
    private bool m_isWaitingToLoadNextScene = false;
    #endregion
}
