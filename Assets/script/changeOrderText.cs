using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeOrderText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().sortingOrder = 300;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
