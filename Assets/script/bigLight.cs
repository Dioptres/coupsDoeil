using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigLight : Lookable {

	bool charging;
	public float croissance = 0.01f;
	public float speedOfLoss = 2;

	protected override void StartLookable ()
	{
		base.StartLookable ();
		charging = false;
	}

	public override void DoAction ()
	{
		
		base.DoAction ();
		charging = true;
	}

	public override void QuitSee ()
	{
		base.QuitSee ();
		charging = false;
	}

	protected override void UpdateLookable ()
	{
		base.UpdateLookable ();
		Debug.Log (conditionsDactivation.Length);
		if (charging)
		{
			this.transform.localScale = new Vector3(transform.localScale.x+croissance, 1, transform.localScale.z + croissance);
			this.GetComponentInChildren<Light> ().intensity += croissance;
		}
		else
		{
			if (transform.localScale.x > 2)
			{
				this.transform.localScale = new Vector3 (transform.localScale.x - croissance/2, 1, transform.localScale.z - croissance/2);
			}
		}
	}
}
