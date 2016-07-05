using UnityEngine;
using System.Collections;

public class enemi : MonoBehaviour
{
    LevelManager level;
    public int life;
    // Use this for initialization
    void Start()
    {
        level = new LevelManager();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(life<=0)
        {
            level.EndGame();                             
        }
    }

}
