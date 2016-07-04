using UnityEngine;
using System.Collections;

public class characterController : MonoBehaviour 
{

	public float  m_speed = 5f;

	Transform m_transform;

	void Awake()
	{
		m_transform = GetComponent<Transform> ();
	}

	// Update is called once per frame
	void Update () {
		float moveX = Input.GetAxis ("Horizontal");
		float moveY = Input.GetAxis ("Vertical");

		m_transform.Translate (new Vector3 (moveX, 0f, moveY) * m_speed * Time.deltaTime);
	}
}
