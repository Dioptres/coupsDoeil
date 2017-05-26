using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scarabBehavior : Lookable
{

	public Transform[] checkPoints;
	UnityEngine.AI.NavMeshAgent agent;
	int actualCheckPoint;
	bool stop;
	bool rumeurActive;
	bool porteLuneDestroy;
	public bool sleep;
	public int speed = 1;
	public GameObject porteLune;
	public GameObject rumeur;


	public void Awake ()
	{

		actualCheckPoint = 0;
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();

	}

	public void endSleep()
	{
		sleep = false;
		agent.speed = speed;
	}

	protected override void StartLookable ()
	{
		rumeurActive = false;
		porteLuneDestroy = false;
		base.StartLookable ();
		sleep = true;
		agent.destination = checkPoints[0].position;
		agent.speed = 0;
	}

	protected override void UpdateLookable ()
	{
		base.UpdateLookable ();


		if (!porteLuneDestroy)
		{
			if (Vector3.Distance (this.transform.position, porteLune.transform.position) < 2.8)
			{
				Destroy (porteLune);
				porteLuneDestroy = true;
			}
		}
		if (!rumeurActive)
		{
			if (Vector3.Distance (this.transform.position, rumeur.transform.position) < 2.8)
			{
				rumeurActive = true;
				Destroy (rumeur.transform.GetChild (0).gameObject);
				foreach (Transform child in rumeur.transform)
				{
					child.gameObject.SetActive (true);
				}
			}
		}

		if (agent.remainingDistance == 0f)
		{
			if (actualCheckPoint < checkPoints.Length - 1)
			{
				actualCheckPoint++;
				agent.destination = checkPoints[actualCheckPoint].position;
			}
				
			
		}
	}

	public override void DoAction ()
	{
		if (!sleep)
		{
			agent.speed = 0;
		}
	}

	public override void QuitSee ()
	{
		if (!sleep)
		{
			base.QuitSee ();
			agent.speed = speed;
		}
	}
}
