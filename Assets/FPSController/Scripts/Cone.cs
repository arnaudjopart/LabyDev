using UnityEngine;
using System.Collections;

public class Cone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnParticleCollision( GameObject other )
    {
        Debug.Log( "Touché" );
        Destroy( other );
    }

    /*void OnTriggerEnter(Collider other)
    {
        Debug.Log( "Touché" );
    }*/


}
