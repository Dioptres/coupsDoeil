using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

	public Vector3 dir;

	// Use this for initialization
	void Start () {
		
	}

	void OnBecameInvisible ()
	{
		Destroy (gameObject);
	}


	// Update is called once per frame
	void Update () {
		transform.Translate (dir * Time.deltaTime);
	}
}
