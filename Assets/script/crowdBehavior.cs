using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crowdBehavior : MonoBehaviour {

	GameObject[] places;
	public Vector3 MyStartTarget;
	public GameObject MainPlace;

	public UnityEngine.AI.NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		places = GameObject.FindGameObjectsWithTag ("place");

		int RandomNumber = Random.Range (0, places.Length * 2);
		if(RandomNumber < places.Length)
		{
			MyStartTarget = places[RandomNumber].transform.position;
		}
		else
		{
			MyStartTarget = MainPlace.transform.position;
		}

		agent.destination = MyStartTarget;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
