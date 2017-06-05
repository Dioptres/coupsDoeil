using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crowdBehavior : Lookable {


	public UnityEngine.AI.NavMeshAgent agent;
	float timer;

	// Use this for initialization
	protected override void StartLookable ()
	{
		base.StartLookable ();
		timer = 0;
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
	}

	public void action()
	{
		timer = 1;
	}

	public override void DoAction ()
	{
		base.DoAction ();
		if(timer> -1)
		{
			agent.destination = new Vector3 (-0.4f, 0.5f, 1);
		}
	}

	// Update is called once per frame
	protected override void UpdateLookable ()
	{
		base.UpdateLookable ();

		if (timer > -1)
		{
			timer -= Time.deltaTime;
		}
		if (timer > 0)
		{
			
			this.transform.GetChild (0).eulerAngles = new Vector3(this.transform.GetChild (0).eulerAngles.x, this.transform.GetChild (0).eulerAngles.y+1, this.transform.GetChild (0).eulerAngles.z);
		}
	}
}
