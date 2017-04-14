using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attraction : Lookable
{

	GameObject[] lookables;
	public float dist;
	
	void Start ()
	{
		StartLookable ();
	}

	protected override void StartLookable ()
	{
		base.StartLookable ();
		lookables = GameObject.FindGameObjectsWithTag ("attractable");
	}

	public override void QuitSee ()
	{
		foreach (GameObject peon in lookables)
		{
			if (Vector3.Distance (this.gameObject.transform.position, peon.transform.position) < dist)
			{
				peon.GetComponent<NavMeshAgent> ().SetDestination (peon.transform.position);
			}
		}
	}

	public override void DoAction ()
	{
		foreach (GameObject peon in lookables)
		{
			if (Vector3.Distance (this.gameObject.transform.position, peon.transform.position) < dist)
			{
				peon.GetComponent<NavMeshAgent> ().SetDestination (this.transform.position);
			}
		}
	}

}