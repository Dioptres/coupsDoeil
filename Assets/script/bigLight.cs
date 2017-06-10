using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigLight : Lookable {

	bool charging;
	public float croissance = 0.01f;
	public float speedOfLoss = 2;
	public float howManyIlost = 0.2f;

	public float whenTheyWake = 3;

	public float intensityOfModifier = 0.1f;

	public millePatteBehavior rumeur1;
	public millePatteBehavior rumeur2;
	public millePatteBehavior rumeur3;

	bool reveil;

	GameObject[] lights;

	protected override void StartLookable ()
	{
		base.StartLookable ();
		reveil = false;
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

	public void lostAlight()
	{
		if(transform.parent.localScale.x > 2+howManyIlost)
		{
			this.transform.parent.localScale = new Vector3 (transform.parent.localScale.x - howManyIlost, 1, transform.parent.localScale.z - howManyIlost);
		}
		else
		{
			this.transform.parent.localScale = new Vector3 (2, 1, 2);
		}
		this.transform.parent.localScale = new Vector3 (transform.parent.localScale.x + croissance*Time.deltaTime, 1, transform.parent.localScale.z + croissance * Time.deltaTime);
	}

	protected override void UpdateLookable ()
	{
		
		base.UpdateLookable ();

		if (transform.parent.localScale.x >= whenTheyWake && !reveil)
		{
			reveil = true;
			rumeur1.wakeMeUp ();
			rumeur2.wakeMeUp ();
			rumeur3.wakeMeUp ();
		}

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
			


			this.transform.parent.localScale = new Vector3(transform.parent.localScale.x+croissance, 1, transform.parent.localScale.z + croissance);
			this.GetComponentInChildren<Light> ().intensity += croissance;





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
