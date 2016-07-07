using UnityEngine;
using System.Collections.Generic;

public class ReplaceMaterialOfAll : MonoBehaviour
{
    public string m_nameOfObjects;
    public Material m_materialToReplaceBy;
    public List<GameObject> m_allObjects;

    // Use this for initialization
    void Start()
    {
        foreach( GameObject obj in GameObject.FindObjectsOfType<GameObject>() )
        {
            if( obj.name == m_nameOfObjects )
            {
                obj.GetComponent<Renderer>().material = m_materialToReplaceBy;
            }
        }
    }
}