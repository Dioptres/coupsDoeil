using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.EyeTracking;

//Tobii.EyeX.Framework.BehaviorType.Tobii.EyeX.Client.EyePositionDataEventParams



public abstract class Lookable : MonoBehaviour {
	public float distanceDeVision;
	public int looked;

	private bool tempBool = false;

	private EyePositions po;

	public Lookable[] conditionsDactivation;

	public float timeTillAction;
	public float actionDuration;
	public bool repeat;
	bool done;

	protected float clock;
	private bool isLooked = false;

	public bool decrement;
	public float timeBeforeDecrement;
	private float timeForDecrement;
	public float decrementFactor;

	private GazeAware _gazeAware;

	private IEyeTrackingHost hst;

	public bool canAct;

	// Use this for initialization
	public virtual void Start () {
		StartLookable ();
	}

	protected virtual void StartLookable () {
		canAct = false;
		done = false;
		timeForDecrement = 0;
		clock = 0;
		looked = 0;
	}

	public virtual void DoAction () {
	}

	public virtual void QuitSee () {
		done = false;
	}

	void Stare () {

		isLooked = true;
		if (clock >= timeTillAction) {
			if (repeat) {
				if (clock <= timeTillAction + actionDuration) {
					DoAction ();
					done = true;
				}
			}
			else {
				if (!done) {
					DoAction ();
					done = true;
				}
			}
		}

	}

	private void Update () {
		UpdateLookable ();
	}

	public virtual void UpdateLookable () {

		if (!canAct) {

			if (conditionsDactivation.Length != 0) {
				for (var i = 0; i < conditionsDactivation.Length; i++) {
					if (conditionsDactivation[i].GetComponent<Lookable> ().done == false) {
						break;
					}
					else if (conditionsDactivation[i].GetComponent<Lookable> ().done == true && i == conditionsDactivation.Length - 1) {
						canAct = true;
					}
				}
			}
			else {
				canAct = true;
			}
		}

		if (Input.GetKeyDown ("space")) // toggle hasGazeAware with space key
		{
			tempBool = !tempBool;
		}


		//Debug.Log(po);
		if (looked == 2 && canAct) {
			timeForDecrement = 0.0f;
			clock += Time.deltaTime;
			Stare ();
		}
		else if (clock > 0) {
			if (looked == 1) {
				QuitSee ();
				isLooked = false;
				looked = 0;
			}
			if (decrement) {
				timeForDecrement += Time.deltaTime;
				if (timeForDecrement >= timeBeforeDecrement) {
					clock -= decrementFactor * Time.deltaTime;
					if (clock < 0.0f)
						clock = 0.0f;
				}
			}
			else {
				clock = 0.0f;
			}
		}
	}
}
