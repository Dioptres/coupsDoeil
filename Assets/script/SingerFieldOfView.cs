using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SingerFieldOfView : Lookable {
	public AudioClip[] singerSong;
	public AudioClip[] crowdSound;

	public float radiusRange;
	public float distanceFollowerToStop;

	public LayerMask targetMask;
	public LayerMask obstacleMask;

	private Animator dancerAnimator;
	private AudioSource singerAudio;

	protected override void StartLookable () {
		base.StartLookable ();
		dancerAnimator = GetComponentInChildren<Animator> ();
		singerAudio = GetComponent<AudioSource> ();
		if(singerSong.Length > 0)
		{
			singerAudio.clip = singerSong[Random.Range (0, singerSong.Length)];
		}
		
	}

	protected override void UpdateLookable () {
		base.UpdateLookable ();
		if (Input.GetKeyDown ("space")) {
			DoAction ();
		}
	}

	public override void DoAction () {
		dancerAnimator.SetTrigger ("dancerIsDancing");
		singerAudio.Play ();


		Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, radiusRange, targetMask);


		foreach (Collider catched in targetsInViewRadius) {
			Transform target = catched.GetComponent<Transform> ();

			Animator targetAnimator = catched.GetComponentInChildren<Animator> ();

			NavMeshAgent targetAgent = catched.GetComponent<NavMeshAgent> ();

			AudioSource targetAudio = catched.GetComponent<AudioSource> ();


			targetAgent.destination = transform.position - new Vector3 (1, 0, 1);

			Debug.Log (targetAgent.destination);


			targetAnimator.SetBool ("CrowdIsDancing", true);

			Debug.Log (targetAnimator);


			targetAudio.clip = crowdSound[Random.Range (0, crowdSound.Length)];
			targetAudio.Play ();
			Debug.Log (crowdSound);
		}
	}
}
