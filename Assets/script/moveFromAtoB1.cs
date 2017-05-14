using UnityEngine;
using System.Collections;
using Tobii.EyeTracking;

public class moveFromAtoB1 : Lookable
{

    Vector3 goThere;
	Vector3 previousLookPoint;
    GazePoint gazePoint;
    UnityEngine.AI.NavMeshAgent agent;
	float i;
	bool doAct;
	bool isMoving;
	bool goingUp;
	bool goingRight;

	GameObject[] catchables;

	public bool attire;

	public Animator animator;

	public override void DoAction ()
	{
		doAct = false;
		if (!isMoving)
		{
			animator.SetInteger ("doing", 1);
		}
		else
		{
			animator.SetInteger ("doing", 2);
		}
	}

	public override void QuitSee()
    {
		base.QuitSee ();
		if (!isMoving)
		{
			doAct = true;
			animator.SetInteger ("doing", 0);
		}
		else
		{
			animator.SetInteger ("doing", 2);
		}
		isMoving = !isMoving;

    }

    IEnumerator moveMeThere()
    {
        yield return new WaitForSeconds(2);
        
        if (gazePoint.IsValid)
        {
            
        }
        
    }

	protected override void StartLookable () {
		base.StartLookable ();
		catchables = GameManager.lookables;
		isMoving = false;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		i = 0;
		doAct = false;
		previousLookPoint = Vector3.zero;
	}


	protected override void UpdateLookable ()
    {
		
			base.UpdateLookable ();
			gazePoint = EyeTracking.GetGazePoint ();
		

	}

	private void FixedUpdate ()
	{
		catchables = GameObject.FindGameObjectsWithTag ("Lookable");
		if (doAct)
		{
			goThere = Camera.main.ScreenToWorldPoint (new Vector3 (gazePoint.Screen.x, gazePoint.Screen.y, 10));

			if(attire)
			{
				foreach(GameObject catched in catchables)
				{
					if (catched.layer == 8)
					{
						
						if (Vector3.Distance (this.transform.position, catched.transform.position) < 2)
						{
							catched.GetComponent<UnityEngine.AI.NavMeshAgent> ().destination = this.transform.position;
						}
					}
				}
			}

			

			agent.destination = goThere;
		}
	}
}
