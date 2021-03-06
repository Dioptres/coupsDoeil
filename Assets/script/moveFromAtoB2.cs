﻿using UnityEngine;
using System.Collections;
using Tobii.EyeTracking;

public class moveFromAtoB2 : Lookable {
	private enum State : byte {
		Idle,
		LookedAt,
		Ready,
		WaitingLocation,
		Cancelling
	}

	private State state = State.Idle;

	Vector3 targetLocation;
	bool targetLocationIsValid;

	bool throwFanim;


	float i;
	bool canMove;
	bool goingUp;
	bool goingRight;

	float timerMove;
	float timerCanMove;

	GameObject[] catchables;

	public bool attire;

	Animator animator;

	public float maxDistance = 0.7f;
	public float timeTillReady = 1.0f;
	public float timeTillCancel = 1.0f;
	public float timeTillTeleport = 0.4f;

	public void StopTP()
	{
		state = State.Idle;
	}

	public override void DoAction () { // LookingAt
		
		
		if (state == State.Idle) {

			
			state = State.LookedAt;
			//animator.SetInteger ("doing", 1);
		}
		else if (state == State.LookedAt) {
			if (timerCanMove < 0)
			{
				AkSoundEngine.PostEvent ("Lampiste_regard", gameObject);
				animator.SetBool ("isLookedAt", true);
				state = State.Ready;
				//animator.SetInteger ("doing", 0);
			}
		}
		else if (state == State.WaitingLocation) {
			timerCanMove = timeTillCancel;
			state = State.Cancelling;
			targetLocationIsValid = false;
		}
		else if (state == State.Cancelling) {
			if (timerCanMove < 0) {
				state = State.LookedAt;
				timerCanMove = timeTillReady;
				//animator.SetInteger ("doing", 1);
			}
		}
	}

	public override void QuitSee () {
		base.QuitSee ();
		animator.SetBool ("isLookedAt", false);
		timerCanMove = timeTillReady;
		if (state == State.LookedAt) {
			state = State.Idle;
			//animator.SetInteger ("doing", 2);
		}
		else if (state == State.Ready || state == State.Cancelling) {
			state = State.WaitingLocation;
			targetLocation = transform.position;
		}
	}

	protected override void StartLookable () {
		base.StartLookable ();

		animator = GetComponentInChildren<Animator> ();

		throwFanim = false;
		timerCanMove = 0.6f;
		timerMove = 0;
		i = 0;
		targetLocation = Vector3.zero;
		targetLocationIsValid = false;

		// changer si on en instancie dans la scene (deplacer vers update)
		catchables = GameManager.lookables;
	}

	protected override void UpdateLookable () {
		base.UpdateLookable ();
		timerCanMove -= Time.deltaTime;


		if (throwFanim)
		{
			this.GetComponent<lampFireflySpxn> ().throwFireFly ();
			throwFanim = false;
		}

		if (state == State.WaitingLocation) {
			if (targetLocationIsValid && Vector3.Distance (GameManager.whereIlook, targetLocation) < maxDistance) {
				timerMove += Time.deltaTime;
			}
			else {
				Ray ray = Camera.main.ScreenPointToRay (GameManager.gazePoint.Screen);
				Debug.DrawRay (ray.origin, ray.direction * 10.0f, Color.red);
				RaycastHit hit;
				targetLocationIsValid = !Physics.Raycast (ray, out hit, 20.0f, 1 << 10);
				if (targetLocationIsValid)
					targetLocationIsValid = Physics.OverlapSphere (GameManager.whereIlook, maxDistance, 1 << 10).Length <= 0;
				if (targetLocationIsValid) {
					targetLocation = GameManager.whereIlook;
					timerMove = 0;
				}
			}
			if (timerMove > timeTillTeleport) {
				if (attire) {
					i = 0;
					foreach (GameObject catched in catchables) {
						if (catched.layer == 8) { // layer 8 = Musicians

							if (Vector3.Distance (this.transform.position, catched.transform.position) < 2) {
								i++;

								if (i == 1) {
									catched.transform.position = new Vector3 (targetLocation.x + 1, 0, targetLocation.z);
								}
								else if (i == 2) {
									catched.transform.position = new Vector3 (targetLocation.x - 1, 0, targetLocation.z);
								}
								else if (i == 3) {
									catched.transform.position = new Vector3 (targetLocation.x, 0, targetLocation.z - 1);
								}
								Debug.Log ("MOVE      " + new Vector3 (this.transform.position.x + 1, 0, this.transform.position.z));
							}
						}
					}
				}

				if (Mathf.Abs (targetLocation.x) < 10 && Mathf.Abs (targetLocation.z) < 6)
				{
					this.transform.position = new Vector3 (targetLocation.x, 0, targetLocation.z);
					animator.SetBool ("nextToLamp", false);
					animator.SetTrigger ("isTeleported");
					animator.SetBool ("isDancing", false);
					AkSoundEngine.PostEvent ("Lampiste_tp", gameObject);
					state = State.Idle;
				}
				else
				{
					state = State.Idle;
				}
				
				
				

				throwFanim = true;
			}
		}
	}
}
