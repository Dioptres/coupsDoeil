using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireflyBehavior : Lookable
{

	public Color myColor;
	public GameObject lampe;
	float timer;

	float timeBeforeDestroy = 0.65f;
	bool destroy;

	Animator anim;
	int numbOfPonderation;

	public float speedMin = 0.5f;
	public float speedMax = 1.5f;

	public GameObject[] fireWorks;
	public int[] pourcentageDePop;

	float speed;

	// Use this for initialization
	protected override void StartLookable ()
	{
		base.StartLookable ();

		

		destroy = false;

		numbOfPonderation = 0;

		anim = GetComponentInChildren<Animator> ();
		timer = 0;

		speed = Random.Range (speedMin, speedMax);

		if (myColor == Color.cyan)
		{
			anim.SetInteger ("fireflyColor", 1);
		}
		else if (myColor == Color.magenta)
		{
			anim.SetInteger ("fireflyColor", 2);
		}
		else if (myColor == Color.yellow)
		{
			anim.SetInteger ("fireflyColor", 3);
		}


	}



	// Update is called once per frame
	protected override void UpdateLookable ()
	{
		if (destroy)
		{
			if (timeBeforeDestroy <= 0)
			{
				Destroy (this.gameObject);
			}
			timeBeforeDestroy -= Time.deltaTime;
		}

		base.UpdateLookable ();

		timer += Time.deltaTime;
		transform.Translate (Vector3.forward * Time.deltaTime * speed);


		if (this.transform.position.x < -13 || this.transform.position.x > 12 || this.transform.position.z < -7 || this.transform.position.z > 8)
		{
			Destroy (gameObject);
		}
	}

	public override void DoAction ()
	{
		if (myColor == lampe.GetComponent<sayWhichOneToExplode> ().GetComponentInChildren<Light> ().color || lampe.GetComponent<sayWhichOneToExplode> ().GetComponentInChildren<sayWhichOneToExplode> ().lastFireworks)
		{

			anim.SetTrigger ("burst");
			destroy = true;

			this.speed = 0;

			lampe.GetComponent<sayWhichOneToExplode> ().addFirefly ();

			int whichOneExplode;
			int whichIndexExplode;



			for (int i = 0; i < pourcentageDePop.Length; i++)
			{
				numbOfPonderation += pourcentageDePop[i];
			}

			whichIndexExplode = Random.Range (0, numbOfPonderation);
			Debug.Log ("NUM Choose index !!!  " + whichIndexExplode);
			numbOfPonderation = 0;

			for (int j = 0; j < fireWorks.Length; j++)
			{
				numbOfPonderation += pourcentageDePop[j];
				if (whichIndexExplode <= numbOfPonderation)
				{
					AkSoundEngine.PostEvent ("Luciole_Firework", gameObject);
					GameObject myFire = Instantiate (fireWorks[j], this.transform.position, Quaternion.identity);

					foreach (Transform child in myFire.transform)
					{
						child.GetComponent<ParticleSystem> ().startColor = myColor;
					}
				}
			}


		}
		else
		{
			AkSoundEngine.PostEvent ("Luciole_Fail", gameObject);
			Destroy (gameObject);
		}
		
	}

	private void OnDestroy ()
	{
		lampe.GetComponent<sayWhichOneToExplode> ().removeOne (myColor);
	}
}
