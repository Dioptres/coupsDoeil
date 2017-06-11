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
			doCroa.neutral();
			AkSoundEngine.PostEvent ("foule", this.gameObject);
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
	
		if(timer == 1)
		{
			anim.SetTrigger("danceQuick");
		}
		if (timer > -1)
		{
			timer -= Time.deltaTime;
		}
	}
}
