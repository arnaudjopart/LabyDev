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
        print( "collision with " + _other.name );
        if( _other.GetComponent<FPSController>() )
        {
            //print( "collision wirh player"+_other.name);//Destroy( _other.gameObject );
            GameManager.GameOver();
            
        }
        

    }
    #endregion


    #region Utils

    #endregion


    #region Private Members

    #endregion
}
