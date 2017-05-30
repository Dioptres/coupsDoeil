using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alreadyMoved : MonoBehaviour {

	public bool alreadyMove;

	// Use this for initialization
	void Start ()
	{
		alreadyMove = false;
	}

	private void LateUpdate ()
	{
		alreadyMove = false;
	}
}
