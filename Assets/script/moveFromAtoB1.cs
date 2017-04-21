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

	public Animator animator;

	public override void DoAction ()
	{
		doAct = false;
	}

	public override void QuitSee()
    {
		base.QuitSee ();
		if (!isMoving)
		{
			doAct = true;
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

    public override void Start()
    {
        base.Start();
		isMoving = false;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
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
		//Debug.Log (doAct);
		if (doAct)
		{
			goThere = Camera.main.ScreenToWorldPoint (new Vector3 (gazePoint.Screen.x, gazePoint.Screen.y, 10));
			if(transform.position.x < goThere.x)
			{
				animator.SetBool ("right", true);
			}
			else
			{
				animator.SetBool ("right", false);
			}

			if (transform.position.z + 1 < goThere.z)
			{
				animator.SetInteger ("upMiddleDown", 0);
			}
			else if (transform.position.z - 1 > goThere.z)
			{
				animator.SetInteger ("upMiddleDown", 2);
			}
			else
			{
				animator.SetInteger ("upMiddleDown", 1);
			}

			agent.destination = goThere;
		}
	}
}
