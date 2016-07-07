﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ShowRapportGame : MonoBehaviour {

    #region Public & Protected Members
    public GameObject m_viewRetry;
    #endregion

    #region Main Methods
    void Start() {
        m_viewRapport = GameObject.Find( "ViewRapportGame" ).GetComponent<Transform>();
        
        m_viewRetryAnim = m_viewRetry.GetComponent<Animator>();
        m_rapportUI = GameObject.Find( "labelRapport" ).GetComponent<Text>();
        m_rapportPlayer1UI = GameObject.Find( "labelRapportPlayer1" ).GetComponent<Text>();
        m_rapportPlayer2UI = GameObject.Find( "labelRapportPlayer2" ).GetComponent<Text>();

        m_deathPlayer1 = !GameManager.m_player1Win;
        m_objectivePlayer2 = GameManager.m_player2Objective == 1 ? true : false; // GameManager.m_player2Win; // true : gentil false : mechant
        ShowRapport();
    }

    public void onExitButtonClick() {
        Debug.Log( "exit rapport" );
        m_viewRapport.Translate( new Vector3( 0, -100, 0 ) );
        m_viewRetryAnim.SetBool( "ExitViewRapport", true );
    }
    #endregion

    #region Utils
    private void ShowRapport()
    {
        m_rapportPlayer1UI.text = m_deathPlayer1 ? "est mort dans le labyrhinte :( !!!" : "est sorti du labyrhinte en entier :D !!!";
        m_rapportPlayer2UI.text = m_objectivePlayer2 ? "devait sauver le player 1" : "devait tuer le player 1 ";

        if( !m_deathPlayer1 && !m_objectivePlayer2 )
        {
            m_rapportUI.text = "Player 1 gagne !!!";
        }
        if( m_deathPlayer1 && m_objectivePlayer2 )
        {
            m_rapportUI.text = "Les joueurs ont perdu !!!";
        }
        if( !m_deathPlayer1 && m_objectivePlayer2 )
        {
            m_rapportUI.text = "Les joueurs ont gagné !!!";
        }
        if( m_deathPlayer1 && !m_objectivePlayer2 )
        {
            m_rapportUI.text = "Player 2 gagne !!!";
        }
    }
    #endregion

    #region Private Members
    Transform m_viewRapport;
    private Animator m_viewRetryAnim;

    private bool m_deathPlayer1;
    private bool m_objectivePlayer2;

    private Text m_rapportPlayer1UI;
    private Text m_rapportPlayer2UI;
    private Text m_rapportUI;

    #endregion

}