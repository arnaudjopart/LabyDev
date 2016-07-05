using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _Started : MonoBehaviour
{
    LevelManager level;

    #region Unity Function
    void Start()
    {
        level = new LevelManager();
    }
    #endregion

    #region My Function
    public void inputStart()
    {
        StartCoroutine( delayInput() );
    }
    #endregion

    #region Numertor
    IEnumerator delayInput()
    {
        yield return new WaitForSeconds( level.m_ChangeScene );
        level.StartGame();
    }
    #endregion
}
