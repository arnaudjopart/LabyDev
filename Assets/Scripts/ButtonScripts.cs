using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
    public int buttonId;

	void Start ()
    {
        GetComponent<Button>().onClick.AddListener( () => Clicked() );
    }

    void Clicked()
    {
        if ( buttonId == 0)
        {
            Global.Server = true;
            SceneManager.LoadScene( 1 );
        }

        if( buttonId == 1 )
        {
            Global.Server = false;
            SceneManager.LoadScene( 1 );
        }
    }
}
