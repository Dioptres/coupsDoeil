using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RumeurBehavior : Lookable {

	public Transform[] checkPoints;
	UnityEngine.AI.NavMeshAgent agent;
	int actualCheckPoint;
	bool flee;
	public bool shy;

	public override void Start ()
	{
		flee = false;
		base.Start ();
		actualCheckPoint = 0;
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		agent.destination = checkPoints[(actualCheckPoint+1) % checkPoints.Length].position;
	}

	public override void Update ()
	{
		if (Input.GetKey ("space"))
		{
			DoAction ();
		}

		base.Update ();

		if (agent.remainingDistance == 0f)
		{
			if(!flee)
			{
				actualCheckPoint++;
				actualCheckPoint = actualCheckPoint % checkPoints.Length;
			}
			else
			{
				flee = false;
			}
			
			agent.destination = checkPoints[(actualCheckPoint + 1) % checkPoints.Length].position;
		}
	}

	public override void DoAction ()
	{
		Debug.Log ("act");
		if(shy)
		{
			agent.destination = checkPoints[actualCheckPoint].position;
			flee = true;
		}
		else
		{

			agent.velocity = Vector3.zero;
		}
	}
}
