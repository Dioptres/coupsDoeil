using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letThemCome : MonoBehaviour {

	UnityEngine.AI.NavMeshAgent agent;
	Animator anim;


	// Use this for initialization
	void Start () {
		agent = GameObject.Find ("Bassiste").GetComponent<UnityEngine.AI.NavMeshAgent> ();
		agent.destination = new Vector3 (-3.6f,0,-1.2f);

		anim = GameObject.Find ("Bassiste").GetComponentInChildren<Animator> ();
		anim.SetBool ("isWalking", true);
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log (agent.remainingDistance);
		if(agent.remainingDistance == 0)
		{
			anim.SetBool ("isWalking", false);
		}
	}
}


/*
bassiste : -3.6_-1.2
flutiste : -1.1_-1.8
guitariste : -1.5_2.2
Ukulele : 0.2_1.1
*/