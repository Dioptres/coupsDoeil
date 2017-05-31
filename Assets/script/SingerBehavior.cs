using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingerBehavior : MonoBehaviour {

	public UnityEngine.AI.NavMeshAgent agent;

	public float coeffSpeed;
	bool mustSing;

	public float distanceDarret = 1;

	bool willSing;
	bool willStop;
	public bool mustStop;

	GameObject[] foules;

	bool porteLuneActivate;

	int placeToGo;

	GameObject[] lampes;

	GameObject lampWakingMeUp;

	public float totalTimeSinging = 5;
	float timeSpendSinging;

	bool isSleeping;

	public GameObject porteLune;
	public GameObject carrier;

	public GameObject[] places;

	public GameObject[] chanteurs;

	public float speed = 1;

	public void stopSing()
	{
		agent.speed = speed;


		if (!porteLuneActivate)
		{
			if (Vector3.Distance (this.transform.position, porteLune.transform.position) < 3)
			{
				porteLuneActivate = true;
				AkSoundEngine.PostEvent ("foule", gameObject);
				carrier.GetComponent<scarabBehavior> ().endSleep ();
			}
		}
		mustSing = false;
	}

	// Use this for initialization
	void Start ()
	{
		mustStop = false;
		foules = GameObject.FindGameObjectsWithTag ("foule");

		placeToGo = 0;

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

		

		agent.destination = places[0].transform.position;
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
		Debug.Log ("NOW SING !!!");
		
		if (!isSleeping)
		{
			if (timeSpendSinging == 0)
			{
				AkSoundEngine.PostEvent ("Chanteur_fixe", gameObject);
			}
			timeSpendSinging += Time.deltaTime;

			if (timeSpendSinging >= totalTimeSinging)
			{
				placeToGo = (placeToGo + 1) % places.Length;
				agent.destination = places[placeToGo].transform.position;
				agent.speed = speed;

				mustStop = false;

				if (!porteLuneActivate && !mustStop)
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
				if(mustStop)
				{
					willStop = true;
				}
				else
				{
					mustSing = true;
				}
				
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

			if (mustSing || willStop)
			{
				mustSing = false;
				willStop = false;
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

				if(Vector3.Distance (this.transform.position, GameManager.whereIlook) < 1)
				{
					if (agent.speed != 0)
					{
						Debug.Log (agent.speed);
						AkSoundEngine.PostEvent ("Chanteur_regard", gameObject);
						agent.speed = 0;
					}
				}
				else
				{
					agent.speed = speed;
				}
				

				if (Vector3.Distance (this.transform.position, agent.destination) < distanceDarret)
				{
					willSing = false;
					willStop = false;
					foules = GameObject.FindGameObjectsWithTag ("foule");
					foreach (GameObject foule in foules)
					{
						if(Vector3.Distance(foule.transform.position,this.transform.position)<1.5)
						{
							agent.speed = 0;
							timeSpendSinging = 0;
							willSing = true;
							Sing ();
							break;
						}
					}
					if(mustStop)
					{
						agent.speed = 0;
						timeSpendSinging = 0;
						willStop = true;
						mustStop = false;
						Sing ();
					}

					if(!willSing && ! willStop)
					{
						placeToGo = (placeToGo + 1) % places.Length;
						agent.destination = places[placeToGo].transform.position;
					}
				}
			}
		}
	}
}
