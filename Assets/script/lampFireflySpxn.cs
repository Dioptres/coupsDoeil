using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lampFireflySpxn : MonoBehaviour {

	public Animator anim;

	public float intensityOfLampist = 2;

	GameObject fireFly1;

	GameObject fireFly2;

	GameObject fireFly3;

	GameObject fireFly4;

	Vector3 move;

	int lampeAllume;

	public Transform lastPos;

	int totalNbrLampes;

	bool end;

	Color fireflyColor;

	public GameObject luciol;

	public float distanceActivationLampe;

	public float lampPostIntensity = 3;
	public float lampPostRange = 50;

	float timerAnim;
	bool feedback;

	public float timerBeforeLeaving = 1.4f;

	bool exist = false;

	int waveNumber = 0;

	GameObject[] lampes;

	private AudioSource source;

	public AudioClip fireFly;

	protected void Start () {
		feedback = true;

		end = false;
		lampeAllume = 0;
		source = GetComponent<AudioSource> ();

		this.transform.GetChild (0).GetComponent<Light> ().intensity = intensityOfLampist;
	}


	public void throwFireFly () {
		if (true)
		{
			source.PlayOneShot (fireFly);
			anim.SetBool ("isThrowing", true);
			timerAnim = 0;

			float randA = Random.Range (-1f, 1f);
			float randB = Random.Range (-1f, 1f);

			if (randA == 0 && randB == 0) {
				randA = 1;
				randB = 1;
			}

			move = new Vector3 (randA, 0, randB);

			if (exist) {
				Destroy (fireFly1);
				Destroy (fireFly2);
				Destroy (fireFly3);
				Destroy (fireFly4);
			}

			//fireflyColor = new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f));
			switch (waveNumber) {
				case 0:
					fireflyColor = Color.cyan;
					break;

				case 1:
					fireflyColor = Color.yellow;
					break;

				case 2:
					fireflyColor = Color.magenta;
					break;
			}

			fireFly1 = Instantiate (luciol, this.transform.position, Quaternion.identity);
			fireFly1.GetComponentInChildren<Light> ().color = fireflyColor;
			fireFly1.name = "fireFly1";
			fireFly1.transform.LookAt (this.transform.position + move.normalized);

			fireFly2 = Instantiate (luciol, this.transform.position, Quaternion.identity);
			fireFly2.GetComponentInChildren<Light> ().color = fireflyColor;
			fireFly2.name = "fireFly2";
			fireFly2.transform.LookAt (this.transform.position - move.normalized);

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
			if (totalNbrLampes == 0)
			{
				totalNbrLampes = lampes.Length;
			}

			foreach (GameObject lampe in lampes) {
				if (Vector3.Distance (this.transform.position, lampe.transform.position) < distanceActivationLampe) {
					if (lampe.GetComponentInChildren<Light> ().intensity == 0) {
						lampeAllume++;
						waveNumber = (waveNumber+1)%3;
						this.transform.GetChild (0).GetComponent<Light> ().intensity -= intensityOfLampist/totalNbrLampes;

						if (lampeAllume == totalNbrLampes)
						{
							end = true;
							
						}

						lampe.GetComponent<lamp> ().song ();
					}
					lampe.GetComponentInChildren<Light> ().intensity = lampPostIntensity;
					lampe.GetComponentInChildren<Light> ().range = lampPostRange;
					lampe.GetComponentInChildren<Light> ().color = fireflyColor;


				}
			}
		}
		
	}

	protected void Update ()
	{
		if(end)
		{
			timerBeforeLeaving -= Time.deltaTime;

			if (timerBeforeLeaving <= 0.4f && feedback)
			{
				throwFireFly ();
				feedback = false;
			}
			else if (timerBeforeLeaving <= 0)
			{
				this.transform.GetChild (0).GetComponent<Light> ().intensity = intensityOfLampist;
				this.transform.position = lastPos.position;
				this.GetComponent<moveFromAtoB2> ().enabled = false;
			}
		}

		if(timerAnim < 0.2f)
		{
			timerAnim+=Time.deltaTime;
		}
		else
		{
			anim.SetBool ("isThrowing", false);
		}
		

		if (exist && fireFly1 != null) {
			fireFly1.transform.Translate (Vector3.forward * Time.deltaTime);
			fireFly2.transform.Translate (Vector3.forward * Time.deltaTime);
			fireFly3.transform.Translate (Vector3.forward * Time.deltaTime);
			fireFly4.transform.Translate (Vector3.forward * Time.deltaTime);

		}
	}
}
