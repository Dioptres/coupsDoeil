using UnityEngine;
using System.Collections;
using Tobii.EyeTracking;

public class moveFromAtoB2 : Lookable
{

    Vector3 goThere;
	Vector3 previousGoThere;
	Vector3 previousLookPoint;
    GazePoint gazePoint;
	float i;
	bool doAct;
	bool isMoving;
	bool canMove;
	bool goingUp;
	bool goingRight;

	float timerMove;
	float timerCanMove;

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
			timerCanMove -= Time.deltaTime;
			if(timerCanMove < 0)
			{
				isMoving = false;
			}
			animator.SetInteger ("doing", 2);
		}
	}

	public override void QuitSee()
    {
		timerCanMove = 0.6f;
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

    public override void Start()
    {
		timerCanMove = 0.6f;
		timerMove = 0;
		catchables = GameObject.FindGameObjectsWithTag ("Lookable");
        base.Start();
		isMoving = false;
		i = 0;
		doAct = false;
		previousLookPoint = Vector3.zero;
	}


	public override void Update()
    {
		
			base.Update ();
			gazePoint = EyeTracking.GetGazePoint ();
		

	}

	private void FixedUpdate ()
	{
		catchables = GameObject.FindGameObjectsWithTag ("Lookable");
		if (doAct)
		{

			previousGoThere = goThere;
			goThere = Camera.main.ScreenToWorldPoint (new Vector3 (gazePoint.Screen.x, gazePoint.Screen.y, 10));



			if(Vector3.Distance(goThere,previousGoThere) < 0.7f)
			{
				timerMove += Time.deltaTime;
			}
			else
			{
				timerMove = 0;
			}



			if(timerMove>0.4f)
			{
				if (attire)
				{
					i = 0;
					foreach (GameObject catched in catchables)
					{
						if (catched.layer == 8)
						{
							
							if (Vector3.Distance (this.transform.position, catched.transform.position) < 2)
							{
								i++;
								
								if(i == 1)
								{
									catched.transform.position = new Vector3 (goThere.x + 1, 0, goThere.z);
								}
								else if (i == 2)
								{
									catched.transform.position = new Vector3 (goThere.x - 1, 0, goThere.z);
								}
								else if (i == 3)
								{
									catched.transform.position = new Vector3 (goThere.x, 0, goThere.z-1);
								}
								Debug.Log ("MOVE      " + new Vector3 (this.transform.position.x + 1, 0, this.transform.position.z));
							}
						}
					}
				}

				this.transform.position = new Vector3 (goThere.x, 0, goThere.z);
			}
			

			

			

		}
	}
}
