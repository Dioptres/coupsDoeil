using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scarabBehavior : Lookable
{

	public Transform[] checkPoints;
	UnityEngine.AI.NavMeshAgent agent;
	int actualCheckPoint;
	bool stop;
	bool rumeurActive;
	bool porteLuneDestroy;
	public float speed = 1;
	public float modifFuite = 1.5f;

	public float timeToHide = 2;
	float timeHidden;

	public int nbrOfActivBeforeLeaving = 3;
	int actualNbrOfActiv;

	bool willLaunch;
	bool flee;

	public float distanceToActivateStuff;

	public float waitThisBeforeGoing = 4;
	float timerOfWaiting;


	public void Awake ()
	{

		actualCheckPoint = 0;
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();

	}

	protected override void StartLookable ()
	{
		flee = false;

		actualNbrOfActiv = 0;
		willLaunch = false;
		rumeurActive = false;
		porteLuneDestroy = false;
		base.StartLookable ();
		agent.destination = checkPoints[0].position;
		agent.speed = speed;
	}

	protected override void UpdateLookable ()
	{

		base.UpdateLookable ();

		if(willLaunch)
		{
			timerOfWaiting -= Time.deltaTime;
			if(timerOfWaiting <= 0)
			{
				agent.speed = speed;
				willLaunch = false;
			}
		}

		if (agent.remainingDistance == 0f)
		{
			if (actualCheckPoint < checkPoints.Length - 1)
			{
				if (!flee)
				{
					actualCheckPoint++;
					agent.destination = checkPoints[actualCheckPoint].position;
				}
				else
				{
					timeHidden -= Time.deltaTime;
					if(timeHidden <=0)
					{
						actualCheckPoint++;
						agent.destination = checkPoints[actualCheckPoint].position;
						flee = false;
						actualNbrOfActiv = 0;
						agent.speed = speed;
					}
				}
			}
			else if (actualCheckPoint == checkPoints.Length - 1)
			{
				actualCheckPoint++;
				GameManager.fadeToDo = GameManager.fadeState.FadeOut;
			}


		}
	}

	public override void DoAction ()
	{
		if (!flee)
		{
			AkSoundEngine.PostEvent ("Agitateur_regard", gameObject);
			agent.speed = 0;
			actualNbrOfActiv++;
			timerOfWaiting = waitThisBeforeGoing;
			willLaunch = true;

			if (actualNbrOfActiv == nbrOfActivBeforeLeaving)
			{
				actualCheckPoint--;
				flee = true;
				timeHidden = timeToHide;
				agent.destination = checkPoints[actualCheckPoint].position;
				agent.speed = speed * modifFuite;
			}
		}
	}

	

	public override void QuitSee ()
	{
		
			base.QuitSee ();



			AkSoundEngine.PostEvent ("Agitateur_marche", gameObject);
		
	}
}
