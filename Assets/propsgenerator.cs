using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class propsgenerator : MonoBehaviour {

    public List<GameObject> m_props;

	void Start () {
        Transform container = GameObject.Find("Environment").transform; 
        GameObject prop = Instantiate(m_props[Random.Range(0, m_props.Count)], transform.position, Quaternion.identity) as GameObject;
        //prop.transform.SetParent(container, false);
	}
}
