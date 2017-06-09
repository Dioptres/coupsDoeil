using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnTime : MonoBehaviour {

	public float timer = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(timer <= 0)
		{
			Destroy (this.gameObject);
		}

		timer -= Time.deltaTime;
	}
}
