﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oldMusicianBehavior : Lookable {

	/*
	 * enum :
	 *	guitare
	 *  basse
	 *  ukulele
	 * flutte
	 * batterie
	 */

	public bool lastPlace;

	public bool stopMoving;

	UnityEngine.AI.NavMeshAgent agent;

	Animator anim;

	public float distanceOtherMusician;
	int nbrOfMusician;
	public int trueNbrOfMusician;

	public bool boule;

	public GameObject lampWakingMeUp;

	bool isSleeping;

	GameObject myPlace;

	public GameObject[] Musician;
	GameObject[] lampes;

	public AudioClip son1;
	public AudioClip son2;
	public AudioClip superSon;

	public GameObject GO1;

	public int numberOfFouleSpwn = 8;
	public GameObject foule;

	private AudioSource source;

	float time;

	GameObject manager;

	
	public void spwnFoule()
	{
		/*GameObject[] foulesSPWNer;
		foulesSPWNer = GameObject.FindGameObjectsWithTag ("place");
		for (int i = 0; i<numberOfFouleSpwn; i++)
		{
			int randPosition = Random.Range (0, foulesSPWNer.Length);
			Debug.Log (randPosition + "   " + foulesSPWNer.Length);
			GameObject go = Instantiate (foule, foulesSPWNer[randPosition].transform.position, Quaternion.identity);
			go.GetComponent<crowdBehavior> ().MainPlace = myPlace;
		}*/
	}

	private void Awake () {
		anim = GetComponentInChildren<Animator> ();
		nbrOfMusician = 0;
		trueNbrOfMusician = 0;
		source = GetComponent<AudioSource> ();
		manager = GameObject.FindGameObjectWithTag ("Game_Manager");
	}

	protected override void StartLookable ()
	{
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		stopMoving = false;
		lampes = GameObject.FindGameObjectsWithTag ("lampe");

		lampWakingMeUp = lampes[0];
		for (int i = 1; i < lampes.Length; i++)
		{
			if (Vector3.Distance (lampes[i].transform.position, this.transform.position) < Vector3.Distance (lampWakingMeUp.transform.position, this.transform.position))
			{
				lampWakingMeUp = lampes[i];
			}
		}

		isSleeping = true;
		base.StartLookable ();
		time = 0;

		GameObject[] foulesSPWNer;
		foulesSPWNer = GameObject.FindGameObjectsWithTag ("soonToBePlace");

		myPlace = foulesSPWNer[0];

		for(int i = 1; i<foulesSPWNer.Length; i++)
		{
			if(Vector3.Distance(this.transform.position, foulesSPWNer[i].transform.position) < Vector3.Distance (this.transform.position, myPlace.transform.position))
			{
				myPlace = foulesSPWNer[i];
			}
		}

		

	}

	public void MoveThere (GameObject targetPosition)
	{
		if(!isSleeping && !stopMoving)
		{
			foreach (GameObject musicos in Musician)
			{

				if (Vector3.Distance (this.transform.position, musicos.transform.position) < distanceOtherMusician)
				{
					GameObject[] foules = GameObject.FindGameObjectsWithTag ("foule");

					

					myPlace = targetPosition;
					musicos.GetComponent<UnityEngine.AI.NavMeshAgent> ().destination = targetPosition.transform.position;
				}
			}
		}
	}

	protected override void UpdateLookable () {
		base.UpdateLookable ();

		if (Vector3.Distance (this.transform.position, agent.destination) < 1)
		{
			if(lastPlace)
			{
				stopMoving = true;
			}
		}

			if (lampWakingMeUp.transform.GetChild(0).GetComponent<Light>().intensity > 0)
		{
			isSleeping = false;
		}

		if (trueNbrOfMusician > 1) {
			anim.SetBool ("dancerIsDancing", true);
		}
		
		if (time > 0 && trueNbrOfMusician == 1) {
			time -= Time.deltaTime;
			if (time <= 0 && trueNbrOfMusician != Musician.Length) {
				anim.SetBool ("dancerIsDancing", false);
			}
		}
				
		nbrOfMusician = 0;
		foreach (GameObject musicos in Musician) {

			if (Vector3.Distance (this.transform.position, musicos.transform.position) < distanceOtherMusician) {

				nbrOfMusician++;
			}

			if(GameManager.numberMaxMusiGroupTogether < nbrOfMusician)
			{
				GameManager.numberMaxMusiGroupTogether = nbrOfMusician;
				spwnFoule ();
			}
		}
		if (nbrOfMusician > trueNbrOfMusician) {
			if (nbrOfMusician < Musician.Length) {
				trueNbrOfMusician = nbrOfMusician;
			}
			else {
				foreach (GameObject musicos in Musician) {
					musicos.GetComponent<oldMusicianBehavior> ().trueNbrOfMusician = nbrOfMusician;
					if (trueNbrOfMusician > 1) {
						musicos.GetComponent<oldMusicianBehavior> ().anim.SetBool ("dancerIsDancing", true);
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
		if (!isSleeping)
		{
			if (trueNbrOfMusician == 1)
			{
				source.PlayOneShot (son1);
				time = 0.2f;
				anim.SetBool ("dancerIsDancing", true);
			}
		}
	}
}
