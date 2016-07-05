using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    #region Public and Protected Members
    // 0 = méchant, 1 = gentil
    public int objective;
    public bool m_PlayerIsAlive;
    public FPSController playerFPS;
    public static bool IsGameOver = false;
    public int m_gameTimeInSeconds;
    #endregion

    #region Main Methods

    public static void GameOver()
    {

        IsGameOver = true;
        //launch end scene;
        Debug.Log( "GameOver" );
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
        if ( m_PlayerIsAlive )
        {
            m_PlayerIsAlive = false;
            IsGameOver = true;
            //Application.LoadLevel();
        }
        
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
    #endregion

    #region Private Members
    #endregion

}
