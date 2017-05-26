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

	private AudioSource source;

	public AudioClip move;

	public AudioClip look1;
	public AudioClip look2;
	public AudioClip look3;


	public void Awake () {
		

		source = GetComponent<AudioSource> ();
		flee = true;
		actualCheckPoint = 0;
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		
	}

	protected override void StartLookable ()
	{
		base.StartLookable ();

		agent.destination = terrier.position;
		this.transform.parent.gameObject.GetComponent<millePatteBehavior> ().hasStarted = true;
		this.transform.parent.gameObject.GetComponent<millePatteBehavior> ().active = true;
	}

	public void launch()
	{
		agent.destination = checkPoints.position;
	}

	protected override void UpdateLookable () {
		base.UpdateLookable ();

		if (!source.isPlaying && shy) {
			source.loop = true;
			source.clip = move;
			source.Play ();
		}


		if (agent.remainingDistance == 0f)
		{
			if(!flee)
			{
				checkPoints.GetChild(0).GetComponent<Light> ().intensity = 0;
				agent.destination = terrier.transform.position;
				flee = true;
			}
			else
			{
				this.transform.parent.GetComponent<millePatteBehavior> ().deactivate ();
				flee = false;
			}
		}
	}

	

	public override void DoAction () {
		if (shy) {
			agent.destination = terrier.transform.position;
			flee = true;


			if (source.clip == move) {
				int random = Random.Range (1, 4);


				switch (random) {
					case 1:
						source.loop = false;
						source.clip = look1;
						source.Play ();
						break;
					case 2:
						source.loop = false;
						source.clip = look2;
						source.Play ();
						break;
					case 3:
						source.loop = false;
						source.clip = look3;
						source.Play ();
						break;
				}
			}
		}

	}
}
