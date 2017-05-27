using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingerBehavior : MonoBehaviour {

	public UnityEngine.AI.NavMeshAgent agent;

	public float coeffSpeed;
	bool mustSing;

	bool porteLuneActivate;

	GameObject[] lampes;

	GameObject lampWakingMeUp;

	public float totalTimeSinging = 5;
	float timeSpendSinging;

	bool isSleeping;

	public GameObject porteLune;
	public GameObject carrier;

	GameObject[] places;

	public GameObject[] chanteurs;

	// Use this for initialization
	void Start ()
	{
		lampes = GameObject.FindGameObjectsWithTag ("lampe");
		lampWakingMeUp = lampes[0];
		for (int i = 1; i < lampes.Length; i++)
		{
			if (Vector3.Distance (lampes[i].transform.position, this.transform.position) < Vector3.Distance (lampWakingMeUp.transform.position, this.transform.position))
			{
				lampWakingMeUp = lampes[i];
			}
		}

		isSleeping = true;
		porteLuneActivate = false;
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		agent.speed = 0;
		places = GameObject.FindGameObjectsWithTag ("place");

		agent.destination = places[Random.Range (0, places.Length)].transform.position;
	}

	public void MoveThere (GameObject targetPosition)
	{
		if(!isSleeping)
		{
			this.GetComponent<UnityEngine.AI.NavMeshAgent> ().destination = targetPosition.transform.position;
		}		
	}

	void Sing()
	{
		if (!isSleeping)
		{
			timeSpendSinging += Time.deltaTime;

			if (timeSpendSinging >= totalTimeSinging)
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
	}

	// Update is called once per frame
	void Update ()
	{
		if (lampWakingMeUp.transform.GetChild (0).GetComponent<Light> ().intensity > 0)
		{
			isSleeping = false;
		}

		if (!isSleeping)
		{

			if (mustSing)
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
}
