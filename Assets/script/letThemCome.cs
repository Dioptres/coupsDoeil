using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letThemCome : MonoBehaviour {

	UnityEngine.AI.NavMeshAgent agent;



	// Use this for initialization
	void Start () {
		agent = GameObject.Find ("Bassiste").GetComponent<UnityEngine.AI.NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}


/*
batteur : -4.5_2.6
bassiste : -3.6_-1.2
flutiste : -1.1_-1.8
guitariste : -1.5_2.2
Ukulele : 0.2_1.1
*/