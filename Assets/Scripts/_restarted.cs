using UnityEngine;
using System.Collections;

public class _restarted : MonoBehaviour
{
    LevelManager level;

    #region Function Unity
    void Start()
    {
        level = new LevelManager();
    }

    #endregion

    #region My Function
    public void reStart()
    {
        StartCoroutine( delayInputRestart() );
    }
    #endregion

    #region Numerator
    IEnumerator delayInputRestart()
    {
        yield return new WaitForSeconds( level.m_ChangeScene );
        level.Restart();
    }

    #endregion

}
