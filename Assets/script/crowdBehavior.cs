using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crowdBehavior : Lookable {

	public doCroaCroa doCroa;

	public UnityEngine.AI.NavMeshAgent agent;
	float timer;

	public float timeDancing = 4;
	float dancingTime;

	Animator anim;

	// Use this for initialization
	protected override void StartLookable ()
	{
		base.StartLookable ();

		anim = GetComponentInChildren<Animator> ();

		int temp = Random.Range (0, 4);

		anim.SetInteger ("villagerType", temp);

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
			doCroa.good();
			anim.SetBool ("isDancing", true);
			dancingTime = timeDancing;
		}
	}

	// Update is called once per frame
	protected override void UpdateLookable ()
	{
		base.UpdateLookable ();

		if(dancingTime > 0)
		{
			dancingTime -= Time.deltaTime;
			if(dancingTime <=0)
			{
				anim.SetBool ("isDancing", false);
			}
		}

		if (timer > -1)
		{
			timer -= Time.deltaTime;
		}
		if (timer > 0)
		{
			//this.transform.GetChild (0).eulerAngles = new Vector3(this.transform.GetChild (0).eulerAngles.x, this.transform.GetChild (0).eulerAngles.y+1, this.transform.GetChild (0).eulerAngles.z);
			anim.SetTrigger("danceQuick");
		}
	}
}
