using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RumeurBehavior : Lookable {

	public Transform[] checkPoints;
	UnityEngine.AI.NavMeshAgent agent;
	int actualCheckPoint;

	public override void Start ()
	{
		base.Start ();
		actualCheckPoint = 0;
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		agent.destination = checkPoints[(actualCheckPoint+1) % checkPoints.Length].position;
	}

	public override void Update ()
	{
		if (Input.GetKeyDown ("space"))
		{
			DoAction ();
		}

		base.Update ();

		Debug.Log (agent.remainingDistance);
		if (agent.remainingDistance == 0f)
		{
			actualCheckPoint++;
			actualCheckPoint = actualCheckPoint % checkPoints.Length;
			agent.destination = checkPoints[(actualCheckPoint + 1) % checkPoints.Length].position;
		}
	}

	public override void DoAction ()
	{
		agent.destination = checkPoints[actualCheckPoint].position;
		actualCheckPoint--;
	}
}
