using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicianBehavior : Lookable
{
	public enum Instrument
	{
		None = 0,
		Bass = 1,
		Flute = 2,
		Guitare = 4,
		Percu = 8,
		Uku = 16
	}

	Animator anim;

	bool musicLaunched;

	public float distanceOtherMusician;
	int nbrOfMusician;

	public bool boule;

	GameObject lampWakingMeUp;

	bool isSleeping;

	GameObject myPlace;

	public bool lastPlace;

	public bool stopMoving;

	UnityEngine.AI.NavMeshAgent agent;

	private MusicianBehavior[] Musician;
	GameObject[] lampes;

	public AudioClip son1;
	public AudioClip son2;
	public AudioClip superSon;

	public GameObject GO1;

	public int numberOfFouleSpwn = 8;
	public GameObject foule;
	public Instrument instrument;

	private AudioSource source;

	float time;

	GameManager manager;

	private static Instrument _groupSize;
	private static Instrument groupSize
	{
		get { return _groupSize; }
		set
		{
			if ((_groupSize | value) == (_groupSize ^ value))
			{
				GameManager.numberMaxMusiGroupTogether++;
			}
			_groupSize = value;
		}
	}

	public void spwnFoule ()
	{
		GameObject[] foulesSPWNer;
		foulesSPWNer = GameObject.FindGameObjectsWithTag ("place");
		for (int i = 0; i<numberOfFouleSpwn; i++)
		{
			int randPosition = Random.Range (0, foulesSPWNer.Length);
			Debug.Log (randPosition + "   " + foulesSPWNer.Length);
			GameObject go = Instantiate (foule, foulesSPWNer[randPosition].transform.position, Quaternion.identity);
		}
	}

	private void Awake ()
	{
		
		
		nbrOfMusician = 0;
		source = GetComponent<AudioSource> ();
		manager = GameObject.FindGameObjectWithTag ("Game_Manager").GetComponent<GameManager> ();
		Musician = GameObject.FindObjectsOfType<MusicianBehavior> ();
	}

	protected override void StartLookable ()
	{
		anim = GetComponentInChildren<Animator> ();
		Debug.Log (anim);
		musicLaunched = false;
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		stopMoving = false;
		lampes = GameObject.FindGameObjectsWithTag ("lampe");
		isSleeping = true;
		base.StartLookable ();
		time = 0;

		if (instrument == Instrument.Percu)
		{
			AkSoundEngine.SetState (instrument.ToString (), "percu_intro");
		}

		GameObject[] foulesSPWNer;
		foulesSPWNer = GameObject.FindGameObjectsWithTag ("soonToBePlace");

		myPlace = foulesSPWNer[0];

		for (int i = 1; i < foulesSPWNer.Length; i++)
		{
			if (Vector3.Distance (this.transform.position, foulesSPWNer[i].transform.position) < Vector3.Distance (this.transform.position, myPlace.transform.position))
			{
				myPlace = foulesSPWNer[i];
			}
		}

		lampWakingMeUp = lampes[0];
		for (int i = 1; i < lampes.Length; i++)
		{
			if (Vector3.Distance (lampes[i].transform.position, this.transform.position) < Vector3.Distance (lampWakingMeUp.transform.position, this.transform.position))
			{
				lampWakingMeUp = lampes[i];
			}
		}

	}

	public void MoveThere (GameObject targetPosition)
	{
		Debug.Log ("shouldWalk");
		anim.SetBool ("isWalking", true);
		if (!isSleeping)
		{
			foreach (MusicianBehavior musicos in Musician)
			{

				if (Vector3.Distance (this.transform.position, musicos.transform.position) < distanceOtherMusician)
				{
					GameObject[] foules = GameObject.FindGameObjectsWithTag ("foule");

					foreach (GameObject foule in foules)
					{
						
					}

					myPlace = targetPosition;
					musicos.agent.destination = targetPosition.transform.position;
				}
			}
		}
	}

	protected override void UpdateLookable ()
	{
		base.UpdateLookable ();
		if (Vector3.Distance (this.transform.position, agent.destination) < 1)
		{
			anim.SetBool ("isWalking", false);
			if (lastPlace)
			{
				stopMoving = true;
				agent.speed = 0;
			}
		}
		if (lampWakingMeUp.transform.GetChild (0).GetComponent<Light> ().intensity > 0)
		{
			isSleeping = false;
		}
		if (!isSleeping)
		{
			int lastNbrOfMusician = nbrOfMusician;

			nbrOfMusician = 0;
			foreach (MusicianBehavior musicos in Musician)
			{
				if (musicos == this)
					continue; //ignore celui-ci et passe au prochain

				if (Vector3.Distance (transform.position, musicos.transform.position) < distanceOtherMusician)
				{
					nbrOfMusician++;
				}
			}
			if (nbrOfMusician != lastNbrOfMusician)
			{//si la valeur a change et que l'etat de son anim a besoin d'etre changee
				anim.SetBool ("isPlayingMusic", nbrOfMusician > 0);
				if (instrument != Instrument.Percu && !musicLaunched)
				{
					musicLaunched = true;
					AkSoundEngine.SetState (instrument.ToString (), instrument.ToString () + (nbrOfMusician > 0 ? "_is" : "_not") + "playing");
				}
				Debug.Log (instrument.ToString () + (nbrOfMusician > 0 ? "_is" : "_not") + "playing");

			}

			if (nbrOfMusician > 0)
			{
				if ((groupSize & instrument) != instrument)
				{
					groupSize |= instrument;
					spwnFoule ();
					AkSoundEngine.SetState (instrument.ToString (), "percu_Jeu");
				}
			}
		}
	}

	public override void DoAction ()
	{
		anim.SetTrigger ("isPlayingOnce");
	}
}
