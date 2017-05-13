using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lampFireflySpxn : Lookable {

	public Animator anim;

	float timerSpwnFirefly = 0.4f;

	GameObject fireFly1;

	GameObject fireFly2;

	GameObject fireFly3;

	GameObject fireFly4;

	Vector3 move;

	int lampeAllume;

	Color fireflyColor;

	public GameObject luciol;

	public float distanceActivationLampe;

    public float lampPostIntensity = 3;
    public float lampPostRange = 50;

    bool exist = false;

    int waveNumber = 0;

	GameObject[] lampes;

	private AudioSource source;

	public AudioClip fireFly;

	public override void Start ()
	{
		base.Start ();
		lampeAllume = 0;
		source = GetComponent<AudioSource> ();
	}

	public override void QuitSee ()
	{
		base.QuitSee ();
		anim.SetBool ("isThrowing", false);
	}

	public override void DoAction()
	{
		if (timerSpwnFirefly <= 0)
		{
			timerSpwnFirefly = 0.4f;
			source.PlayOneShot (fireFly);
			anim.SetBool ("isThrowing", true);
		float randA = Random.Range(-1f,1f);
		float randB = Random.Range (-1f, 1f);

		if(randA == 0 && randB == 0)
		{
			randA = 1;
			randB = 1;
		}

		move = new Vector3(randA,0,randB);

        if (exist)
		{
			Destroy (fireFly1);
			Destroy (fireFly2);
			Destroy (fireFly3);
			Destroy (fireFly4);
		}

		//fireflyColor = new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f));
        switch (waveNumber)
        {
            case 0:
                fireflyColor =  Color.cyan;
                waveNumber ++;
                break;

            case 1:
                fireflyColor = Color.yellow;
                waveNumber++;
                break;

            case 2:
                fireflyColor = Color.magenta;
                waveNumber = 0;
                break;
        }

		fireFly1 = Instantiate (luciol, this.transform.position, Quaternion.identity);
		fireFly1.GetComponentInChildren<Light> ().color = fireflyColor;
		fireFly1.name = "fireFly1";
		fireFly1.transform.LookAt (this.transform.position + move.normalized);

		fireFly2 = Instantiate (luciol, this.transform.position, Quaternion.identity);
		fireFly2.GetComponentInChildren<Light> ().color = fireflyColor;
		fireFly2.name = "fireFly2";
		fireFly2.transform.LookAt (this.transform.position  -move.normalized);

		fireFly3 = Instantiate (luciol, this.transform.position, Quaternion.identity);
		fireFly3.GetComponentInChildren<Light> ().color = fireflyColor;
		fireFly3.name = "fireFly3";
		fireFly3.transform.LookAt (this.transform.position + (Quaternion.AngleAxis (-90, Vector3.up) * move).normalized);

		fireFly4 = Instantiate (luciol, this.transform.position, Quaternion.identity);
		fireFly4.GetComponentInChildren<Light> ().color = fireflyColor;
		fireFly4.name = "fireFly4";
		fireFly4.transform.LookAt (this.transform.position + (Quaternion.AngleAxis (90, Vector3.up) * move).normalized);

		exist = true;

		lampes = GameObject.FindGameObjectsWithTag ("lampe");

		foreach (GameObject lampe in lampes)
		{
			if (Vector3.Distance (this.transform.position, lampe.transform.position) < distanceActivationLampe)
			{
					if(lampe.GetComponentInChildren<Light> ().intensity == 0)
					{
						lampeAllume++;
						Debug.Log (lampe.name);
						lampe.GetComponent<lamp> ().song ();
					}
					lampe.GetComponentInChildren<Light> ().intensity = lampPostIntensity;
                lampe.GetComponentInChildren<Light>().range = lampPostRange;
                lampe.GetComponentInChildren<Light> ().color = fireflyColor;
					
					
			}
		}
		}
		else
		{
			anim.SetBool ("isThrowing", true);
			timerSpwnFirefly -= Time.deltaTime;
		}
	}

	public override void UpdateLookable ()
	{
		base.UpdateLookable ();

		if (Input.GetKeyDown ("space"))
		{
			DoAction ();
		}

			if (exist && fireFly1 != null)
		{
			fireFly1.transform.Translate (Vector3.forward*Time.deltaTime);
			fireFly2.transform.Translate (Vector3.forward * Time.deltaTime);
			fireFly3.transform.Translate (Vector3.forward * Time.deltaTime);
			fireFly4.transform.Translate (Vector3.forward * Time.deltaTime);

		}
	}
}
