using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Public and Protected Members
    // 0 = méchant, 1 = gentil
    public int objective;
    public bool m_PlayerIsAlive;
    public FPSController playerFPS;
    public static bool IsGameOver = false;
    public int m_gameTimeInSeconds;

    static GameManager s_instance;

    #endregion

    #region Main Methods

    void Awake()

    {
        if( !s_instance )
        {
            s_instance = this;
        }
    }


    public static void GameOver()
    {
        
        IsGameOver = true;
        //launch end scene;
        Debug.Log( "GameOver" );
        SoundScript.PlayDeathSound();
        GameManager.s_instance.Invoke( "LoadGameOverScene", 2f );
        
    }

    void Start()
    {
        
        IsGameOver = false;

        objective = Random.Range( 0, 1 );
        StartCoroutine( But() );

        //m_PlayerIsAlive = GameObject.Find( "Player" ).GetComponent<FPSController>().m_isAlive;

    }

    void Update()
    {
        /*if( IsGameOver )
        {
            if( !m_isWaitingToLoadNextScene )
            {
                m_isWaitingToLoadNextScene = true;
                
            }
            

        }*/

    }
    #endregion

    #region Utils
    IEnumerator But()
    {
        yield return new WaitForSeconds( Random.Range( 30, 80 ) );
        if ( objective == 0 )
        {
            objective = 1;
        }
        else
        {
            objective = 0;
        }
    }
    IEnumerator GameTick()
    {
        while( m_gameTimeInSeconds > 0 )
        {
            yield return new WaitForSeconds( 1f );
            m_gameTimeInSeconds--;
        }
        
        
    }
    private void LoadGameOverScene()
    {
        SceneManager.LoadScene( 2 );
    }

    #endregion


    #region Private Members
    private bool m_isWaitingToLoadNextScene = false;
    #endregion

}
