using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.EyeTracking;

//Tobii.EyeX.Framework.BehaviorType.Tobii.EyeX.Client.EyePositionDataEventParams



public abstract class Lookable : MonoBehaviour
{
	public float distanceDeVision;
	public bool looked;

	private bool tempBool = false;

	private EyePositions po;

	public Lookable [] conditionsDactivation;

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
	public virtual void Start ()
	{
		StartLookable ();
	}

	protected virtual void StartLookable ()
	{
		canAct = false;
		done = false;
		timeForDecrement = 0;
		hst = EyeTrackingHost.GetInstance ();
		po = hst.GetEyePositionDataProvider ().Last;
		//myEyes = gaze;
		//position = myDevice.
		_gazeAware = GetComponent<GazeAware> ();
		clock = 0;
		//myDevice = EyeTrackingHost.GetInstance() as EyeTrackingHost;
		looked = false;
	}

	public virtual void DoAction ()
	{
	}

	public virtual void QuitSee ()
	{
		done = false;
	}

	void Stare ()
	{
		isLooked = true;
		if (clock >= timeTillAction)
		{
			if (repeat)
			{
				if (clock <= timeTillAction + actionDuration)
				{
					DoAction ();
					done = true;
				}
			}
			else
			{
				if (!done)
				{
					DoAction ();
					done = true;
				}
			}
		}

	}

	// Update is called once per frame
	public virtual void Update ()
	{
		
		if (!canAct)
		{
			
			if(conditionsDactivation.Length != 0)
			{
				for(var i = 0; i<conditionsDactivation.Length; i++)
				{
					if(conditionsDactivation[i].GetComponent<Lookable>().done == false)
					{
						break;
					}
					else if(conditionsDactivation[i].GetComponent<Lookable>().done == true && i == conditionsDactivation.Length-1)
					{
						canAct = true;
					}
				}
			}
			else
			{
				canAct = true;
			}
		}

		if (Input.GetKeyDown ("space")) // toggle hasGazeAware with space key
		{
			tempBool = !tempBool;
		}


		//Debug.Log(po);
		if (looked && canAct)
		{
			timeForDecrement = 0.0f;
			clock += Time.deltaTime;
			Stare ();
			looked = false;
		}
		else if (clock > 0)
		{
			if (isLooked)
			{
				QuitSee ();
				isLooked = false;
			}
			if (decrement)
			{
				timeForDecrement += Time.deltaTime;
				if (timeForDecrement >= timeBeforeDecrement)
				{
					clock -= decrementFactor * Time.deltaTime;
					if (clock < 0.0f)
						clock = 0.0f;
				}
			}
			else
			{
				clock = 0.0f;
			}
		}
	}
}
