using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingerBehavior : MonoBehaviour {

	public UnityEngine.AI.NavMeshAgent agent;

	public float coeffSpeed;
	bool mustSing;

	bool porteLuneActivate;

	public float totalTimeSinging = 5;
	float timeSpendSinging;

	public GameObject porteLune;
	public GameObject carrier;

	GameObject[] places;

	public GameObject[] chanteurs;

	// Use this for initialization
	void Start ()
	{
		porteLuneActivate = false;
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		places = GameObject.FindGameObjectsWithTag ("place");

		agent.destination = places[Random.Range (0, places.Length)].transform.position;
	}

	public void MoveThere (GameObject targetPosition)
	{
		

			
				this.GetComponent<UnityEngine.AI.NavMeshAgent> ().destination = targetPosition.transform.position;
			
		
	}

	void Sing()
	{
		timeSpendSinging += Time.deltaTime;

		if(timeSpendSinging >= totalTimeSinging)
		{
			agent.destination = places[Random.Range (0, places.Length)].transform.position;
			agent.speed = 1;


			if (!porteLuneActivate)
			{
				if (Vector3.Distance (this.transform.position, porteLune.transform.position) < 3)
				{
					porteLuneActivate = true;
					carrier.GetComponent<scarabBehavior> ().endSleep ();
				}
			}
		}
		else
		{
			mustSing = true;
		}
	}

	// Update is called once per frame
	void Update () {

		if(mustSing)
		{
			mustSing = false;
			Sing ();
		}


		else
		{


			/*foreach(GameObject chanteur in chanteurs)
			{
				if(Vector3.Distance(chanteur.transform.position, this.transform.position)< 2)
				{
					chanteur.GetComponent<UnityEngine.AI.NavMeshAgent> ().destination = places[Random.Range (0, places.Length)].transform.position;
				}
			}*/

			agent.speed = Vector3.Distance (this.transform.position, GameManager.whereIlook) * coeffSpeed;

			if (Vector3.Distance (this.transform.position, agent.destination) < 1)
			{
				agent.speed = 0;
				timeSpendSinging = 0;
				Sing ();
			}
		}
	}
}
