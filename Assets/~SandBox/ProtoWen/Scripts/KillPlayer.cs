using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

    #region Public & Protected Members
    
 

    #endregion

    #region Main Methods

    
    // TODO 
    // Kill the player on collide
    void OnTriggerEnter(Collider _other)
    {
        
        //Destroy( _other.gameObject );
        GameManager.GameOver();
    }
    #endregion


    #region Utils

    #endregion


    #region Private Members

    #endregion
}
