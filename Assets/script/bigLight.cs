using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigLight : Lookable {

	bool charging;
	public float croissance = 0.01f;
	public float speedOfLoss = 2;

	GameObject[] lights;

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

		lights = GameObject.FindGameObjectsWithTag ("lampe");

		for(int i = 0; i < lights.Length; i++)
		{
			if (lights[i].GetComponentInChildren<Light> ().intensity > 0)
			{
				break;
			}
			if(i == lights.Length-1)
			{
				for (int j = 0; j < lights.Length; j++)
				{
					lights[j].GetComponentInChildren<Light> ().intensity = 2;
				}
				this.transform.localScale = new Vector3 (2,2,2);
				this.GetComponentInChildren<Light> ().intensity = 0;
			}
		}

		if (charging)
		{
			this.transform.localScale = new Vector3(transform.localScale.x+croissance, 1, transform.localScale.z + croissance);
			this.GetComponentInChildren<Light> ().intensity += croissance;

			if(transform.localScale.x > 4)
			{
				GameManager.fadeToDo = GameManager.fadeState.FadeOut;
			}
		}
		else
		{
			if (transform.localScale.x > 2 && transform.localScale.x < 4)
			{
				this.transform.localScale = new Vector3 (transform.localScale.x - croissance/2, 1, transform.localScale.z - croissance/2);
			}
		}
	}
}
