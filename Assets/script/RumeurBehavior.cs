using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RumeurBehavior : Lookable {

	public Transform[] checkPoints;
	UnityEngine.AI.NavMeshAgent agent;
	int actualCheckPoint;
	bool flee;
	public bool shy;

	private AudioSource source;

	public AudioClip move;

	public AudioClip look1;
	public AudioClip look2;
	public AudioClip look3;

	public GameObject go1;
	public GameObject go2;
	public GameObject go3;
	public GameObject go4;

	public void Awake ()
	{
		if(shy)
		{
			go1.SetActive (false);
			go2.SetActive (false);
			go3.SetActive (false);
			go4.SetActive (false);
		}

		source = GetComponent<AudioSource> ();
		flee = false;
		base.Start ();
		actualCheckPoint = 0;
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		agent.destination = checkPoints[(actualCheckPoint+1) % checkPoints.Length].position;
	}

	public override void Update ()
	{
		if(!source.isPlaying && shy)
		{
			source.loop = true;
			source.clip = move;
			source.Play ();
		}

		if((actualCheckPoint + 1) % checkPoints.Length == 0 && shy)
		{
			go1.SetActive (true);
			go2.SetActive (true);
			go3.SetActive (true);
			go4.SetActive (true);
			Destroy (this.transform.parent.gameObject);
		}

		if (Input.GetKey ("space"))
		{
			DoAction ();
		}

		base.Update ();


		if (agent.remainingDistance == 0f)
		{
			if(!flee)
			{
				
					actualCheckPoint++;
					actualCheckPoint = actualCheckPoint % checkPoints.Length;
				

				
			}
			else
			{
				flee = false;
			}
			
			agent.destination = checkPoints[(actualCheckPoint + 1) % checkPoints.Length].position;
		}
	}

	public override void DoAction ()
	{
		if(shy)
		{
			agent.destination = checkPoints[actualCheckPoint].position;
			flee = true;


			if (source.clip == move)
			{
				int random = Random.Range (1, 4);


				switch (random)
				{
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
