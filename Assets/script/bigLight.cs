using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigLight : Lookable {

	bool charging;
	public float croissance = 0.01f;
	public float howManyIlost = 0.2f;

	Animator anim;


	public float whenTheyWake = 3;

	public millePatteBehavior rumeur1;
	public millePatteBehavior rumeur2;
	public millePatteBehavior rumeur3;

	bool reveil;

	GameObject[] lights;

	protected override void StartLookable ()
	{
		base.StartLookable ();

		croissance /= 4;
		howManyIlost /= 4;
		whenTheyWake /= 4;

		anim = GetComponentInChildren<Animator> ();
		reveil = false;
		charging = false;
	}

	public override void DoAction ()
	{
		base.DoAction ();
		AkSoundEngine.PostEvent ("Champi_gonfle", gameObject);
		AkSoundEngine.PostEvent ("Champi_stopdegonfle", gameObject);
		anim.SetTrigger ("isLookedAt");
		charging = true;
	}

	public override void QuitSee ()
	{
		base.QuitSee ();
		AkSoundEngine.PostEvent ("Champi_degonfle", gameObject);
		AkSoundEngine.PostEvent ("Champi_stopgonfle", gameObject);
		charging = false;
	}

	public void lostAlight()
	{
		AkSoundEngine.PostEvent ("Champi_attaque", gameObject);
		if (transform.parent.localScale.x > 0.25+howManyIlost)
		{
			this.transform.parent.localScale = new Vector3 (transform.parent.localScale.x - howManyIlost, 1, transform.parent.localScale.z - howManyIlost);
		}
		else
		{
			this.transform.parent.localScale = new Vector3 (0.25f, 1, 0.25f);
		}
	}

	protected override void UpdateLookable ()
	{
		
		base.UpdateLookable ();

		Debug.Log (transform.parent.localScale.x);

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
				this.transform.GetChild(1).GetComponent<Light> ().intensity = 0;
			}
		}

		if (charging)
		{
			


			this.transform.parent.localScale = new Vector3(transform.parent.localScale.x+croissance*Time.deltaTime, 1, transform.parent.localScale.z + croissance * Time.deltaTime);
			this.transform.GetChild (1).GetComponent<Light> ().intensity += croissance*Time.deltaTime;





			if(transform.parent.localScale.x >= 1)
			{
				GameManager.fadeToDo = GameManager.fadeState.FadeOut;
				AkSoundEngine.PostEvent ("Champi_stopgonfle", gameObject);
				AkSoundEngine.PostEvent ("Champi_flash", gameObject);
			}
		}
		else
		{
			if (transform.parent.localScale.x > 0.25 && transform.parent.localScale.x < 1)
			{
				
				this.transform.parent.localScale = new Vector3 (transform.parent.localScale.x - (croissance/2)* Time.deltaTime, 1, transform.parent.localScale.z - (croissance/2) * Time.deltaTime);
			}
			else
			{
				AkSoundEngine.PostEvent ("Champi_stopdegonfle", gameObject);
			}
		}
	}
}
