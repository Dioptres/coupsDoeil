using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingerBehavior : MonoBehaviour {

	public UnityEngine.AI.NavMeshAgent agent;

	public float coeffSpeed;

	GameObject[] places;

	public GameObject[] chanteurs;

	// Use this for initialization
	void Start ()
	{
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		places = GameObject.FindGameObjectsWithTag ("place");

		agent.destination = places[Random.Range (0, places.Length)].transform.position;
	}

	public void MoveThere (GameObject targetPosition)
	{
		

			
				this.GetComponent<UnityEngine.AI.NavMeshAgent> ().destination = targetPosition.transform.position;
			
		
	}

	// Update is called once per frame
	void Update () {

		foreach(GameObject chanteur in chanteurs)
		{
			if(Vector3.Distance(chanteur.transform.position, this.transform.position)< 2)
			{
				chanteur.GetComponent<UnityEngine.AI.NavMeshAgent> ().destination = places[Random.Range (0, places.Length)].transform.position;
			}
		}

		agent.speed = Vector3.Distance (this.transform.position, GameManager.whereIlook) * coeffSpeed;

		if (Vector3.Distance(this.transform.position, agent.destination)<1)
		{
			agent.destination = places[Random.Range (0, places.Length)].transform.position;
		}
	}
}
