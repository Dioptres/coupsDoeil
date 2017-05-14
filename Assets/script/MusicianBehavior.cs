using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicianBehavior : Lookable {

	Animator anim;

	public float distanceOtherMusician;
	int nbrOfMusician;
	public int trueNbrOfMusician;

	public bool boule;

	public GameObject[] Musician;

	public AudioClip son1;
	public AudioClip son2;
	public AudioClip superSon;

	public GameObject GO1;

	private AudioSource source;

	float time;

	private void Awake () {
		anim = GetComponentInChildren<Animator> ();
		nbrOfMusician = 0;
		trueNbrOfMusician = 0;
		source = GetComponent<AudioSource> ();

	}

	protected override void StartLookable () {
		base.StartLookable ();
		time = 0;
	}

	protected override void UpdateLookable () {
		base.UpdateLookable ();
		if (trueNbrOfMusician > 1) {
			anim.SetBool ("dancerIsDancing", true);
		}
		
		if (time > 0 && trueNbrOfMusician == 1) {
			time -= Time.deltaTime;
			if (time <= 0 && trueNbrOfMusician != Musician.Length) {
				anim.SetBool ("dancerIsDancing", false);
			}
		}
		
		if (Input.GetKeyDown ("space")) {
			DoAction ();
		}
		
		nbrOfMusician = 0;
		foreach (GameObject musicos in Musician) {

			if (boule) {
			}

			if (Vector3.Distance (this.transform.position, musicos.transform.position) < distanceOtherMusician) {

				nbrOfMusician++;
			}
		}
		if (nbrOfMusician > trueNbrOfMusician) {
			if (nbrOfMusician < Musician.Length) {
				trueNbrOfMusician = nbrOfMusician;
			}
			else {
				foreach (GameObject musicos in Musician) {
					musicos.GetComponent<MusicianBehavior> ().trueNbrOfMusician = nbrOfMusician;
					if (trueNbrOfMusician > 1) {
						musicos.GetComponent<MusicianBehavior> ().anim.SetBool ("dancerIsDancing", true);
					}
				}
			}

		}
		if (trueNbrOfMusician == Musician.Length) {
			anim.SetBool ("dancerIsDancing", true);
			source.loop = false;
			if (superSon != null) {
				if (source.loop != true && source.clip != superSon) {
					source.loop = true;
					source.clip = superSon;
					source.Play ();
				}

			}
			else {
				source.loop = false;
				source.clip = null;
			}
		}
		else if (trueNbrOfMusician > 1) {
			anim.SetBool ("dancerIsDancing", true);
			if (source.loop != true && source.clip != son2) {
				source.loop = true;
				source.clip = son2;
				source.Play ();
			}
		}
		else {
			if (source.loop) {
				source.Stop ();
			}

		}
		if (trueNbrOfMusician > 1) {
			anim.SetBool ("dancerIsDancing", true);
		}
	}

	public override void DoAction () {
		if (trueNbrOfMusician == 1) {
			source.PlayOneShot (son1);
			time = 0.2f;
			anim.SetBool ("dancerIsDancing", true);
		}
	}
}
