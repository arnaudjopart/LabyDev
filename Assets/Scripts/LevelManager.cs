using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{

    #region public variable;
     public float m_ChangeScene=1f;
    #endregion
    // Use this for initialization
    void Start()
    {
        Application.LoadLevel( "Menu" );
    }
    public void StartGame()
    {
        Application.LoadLevel( "Game" );
    }
    public void EndGame()
    {
        Application.LoadLevel( "EndGame" );
    }
    public void Restart()
    {
        Application.LoadLevel( "Menu" );
    }
}
