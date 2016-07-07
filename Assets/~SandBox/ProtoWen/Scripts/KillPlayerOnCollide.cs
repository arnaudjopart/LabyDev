using UnityEngine;
using System.Collections;

public class KillPlayerOnCollide : MonoBehaviour {

    #region Public & Protected Members



    #endregion

    #region Main Methods


    // TODO 
    // Kill the player on collide
    void OnCollisionEnter(Collision _other)
    {
        if( _other.gameObject.GetComponent<FPSController>() )
        {
            //Destroy( _other.gameObject );
            GameManager.GameOver();
        }
        

    }
    #endregion


    #region Utils

    #endregion


    #region Private Members

    #endregion
}
