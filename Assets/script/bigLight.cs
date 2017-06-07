using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigLight : Lookable {

	bool charging;
	public float croissance = 0.01f;
	public float speedOfLoss = 2;

	public float intensityOfModifier = 0.1f;

	float modifSpeedOfGrowth;

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
				this.transform.parent.localScale = new Vector3 (2,2,2);
				this.GetComponentInChildren<Light> ().intensity = 0;
			}
		}

		if (charging)
		{
			modifSpeedOfGrowth = 0;
			foreach(GameObject light in lights)
			{
				if(light.GetComponentInChildren<Light> ().intensity > 0)
				{
					modifSpeedOfGrowth++;
				}
			}

			modifSpeedOfGrowth*= intensityOfModifier;

			this.transform.parent.localScale = new Vector3(transform.parent.localScale.x+croissance * modifSpeedOfGrowth, 1, transform.parent.localScale.z + croissance * modifSpeedOfGrowth);
			this.GetComponentInChildren<Light> ().intensity += croissance;

			Debug.Log (croissance * modifSpeedOfGrowth);

			Debug.Log(transform.parent.localScale.x);

			if(transform.parent.localScale.x > 4)
			{
				GameManager.fadeToDo = GameManager.fadeState.FadeOut;
			}
		}
		else
		{
			if (transform.parent.localScale.x > 2 && transform.parent.localScale.x < 4)
			{
				this.transform.parent.localScale = new Vector3 (transform.parent.localScale.x - croissance/2, 1, transform.parent.localScale.z - croissance/2);
			}
		}
	}
}
