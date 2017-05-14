using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAlamp : Lookable
{

	public GameObject lampist;

	float timerlamp;

	bool act;

	bool activate;

	UnityEngine.AI.NavMeshAgent agent;

	protected override void StartLookable ()
	{
		base.StartLookable ();

		
			activate = false;
			agent = lampist.GetComponent<UnityEngine.AI.NavMeshAgent> ();
			timerlamp = 0.4f;
			act = false;
		
		
	}

	public override void DoAction ()
	{
	if (this.GetComponentInChildren<Light> ().intensity == 0)
	{
		agent.destination = this.transform.position;
		activate = true;
		}
	}
	


}
