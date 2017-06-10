using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RumeurBehavior : Lookable {

	public Transform checkPoints;
	public Transform terrier;
	UnityEngine.AI.NavMeshAgent agent;
	int actualCheckPoint;
	bool flee;
	public bool shy;

	bool isGoingLeft;

	Animator anim;

	public float timerBeforeLeaving = 1;
	float timeHappenedSinceLooked;

	private AudioSource source;

	public float speed = 1;

	public AudioClip move;

	public AudioClip look1;
	public AudioClip look2;
	public AudioClip look3;

	Vector3 myPreviousPos;


	public void Awake () {
		myPreviousPos = this.transform.position;

		isGoingLeft = false;

		source = GetComponent<AudioSource> ();
		flee = false;
		actualCheckPoint = 0;
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		
	}

	protected override void StartLookable ()
	{
		base.StartLookable ();

		anim = GetComponentInChildren<Animator> ();

		agent.speed = speed;

		timeHappenedSinceLooked = 0;

		agent.destination = checkPoints.position;
		this.transform.parent.gameObject.GetComponent<millePatteBehavior> ().hasStarted = true;
		this.transform.parent.gameObject.GetComponent<millePatteBehavior> ().active = true;
	}

	public void launch()
	{
		agent.destination = checkPoints.position;
	}

	protected override void UpdateLookable ()
	{
		base.UpdateLookable ();

		if (!source.isPlaying && shy) {
			source.loop = true;
			source.clip = move;
			source.Play ();
		}

		if(flee)
		{
			timeHappenedSinceLooked += Time.deltaTime;

			if(timeHappenedSinceLooked >= timerBeforeLeaving)
			{
				agent.speed = speed;
			}
		}

		if (agent.remainingDistance == 0f)
		{
			if(!flee)
			{
				Debug.Log (checkPoints.GetChild (0).GetComponent<Light> ().intensity);
				checkPoints.GetChild(0).GetComponent<Light> ().intensity = 0;
				anim.SetTrigger ("isSnatching");
				AkSoundEngine.PostEvent ("Lampadaire_off", gameObject);
				agent.destination = terrier.transform.position;
				flee = true;
				AkSoundEngine.PostEvent ("Rumeur_regard", gameObject);
			}
			else
			{
				
				flee = false;
				agent.speed = speed;
				anim.SetBool ("isFrightened", false);
				this.transform.parent.GetComponent<millePatteBehavior> ().deactivate ();
			}
		}



		if (myPreviousPos.x - this.transform.position.x > 0 && !isGoingLeft)
		{
			Debug.Log ("LEFT !!!!!!!!!!!!");
			isGoingLeft = true;
			anim.SetBool ("goingLeft", true);
		}
		else if (myPreviousPos.x - this.transform.position.x < 0 && isGoingLeft)
		{
			Debug.Log ("RIGHT !!!!!!!!!!!!");
			isGoingLeft = false;
			anim.SetBool ("goingLeft", false);
		}

		myPreviousPos = this.transform.position;
	}

	

	public override void DoAction () {
		if (shy)
		{
			agent.destination = terrier.transform.position;
			if (!flee)
			{
				AkSoundEngine.PostEvent ("Rumeur_regard", gameObject);

				flee = true;
				anim.SetBool ("isFrightened", true);

				agent.speed = 0;
				timeHappenedSinceLooked = 0;

				agent.speed = 0;
			}
			

			

		}

	}
}
