using UnityEngine;
using System.Collections;

public class KillPlayerInSeconds : MonoBehaviour {

    #region Public & Protected Members



    #endregion

    #region Main Methods

    /// <summary>
    /// Destroy player on collision
    /// </summary>
    /// <param name="_other">The object that collide</param>
    void OnTriggerEnter( Collider _other )
    {
        Destroy( _other.gameObject );
    }


    #endregion


    #region Utils

    #endregion


    #region Private Members

    #endregion
}
