using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letThemCome : MonoBehaviour {

	UnityEngine.AI.NavMeshAgent agent;
	Animator anim;
	Animator animLune;

	int index;


	// Use this for initialization
	void Start () {
		index = 0;
		animLune = GetComponentInChildren<Animator> ();
		agent = GameObject.Find ("Bassiste").GetComponent<UnityEngine.AI.NavMeshAgent> ();
		agent.destination = new Vector3 (-3.6f,0,-1.2f);

		anim = GameObject.Find ("Bassiste").GetComponentInChildren<Animator> ();
		anim.SetBool ("isWalking", true);
		animLune.SetInteger ("moonColor", 1);
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log (agent.remainingDistance);
		if(agent.remainingDistance == 0)
		{
			anim.SetBool ("isWalking", false);
			if (index == 0)
			{
				index++;
				agent = GameObject.Find ("Flûtiste").GetComponent<UnityEngine.AI.NavMeshAgent> ();
				agent.destination = new Vector3 (-1.1f, 0, -1.8f);

				anim = GameObject.Find ("Flûtiste").GetComponentInChildren<Animator> ();
				anim.SetBool ("isWalking", true);
				animLune.SetInteger ("moonColor", 2);
			}
			else if (index == 1)
			{
				index++;
				agent = GameObject.Find ("Guitariste").GetComponent<UnityEngine.AI.NavMeshAgent> ();
				agent.destination = new Vector3 (-1.5f, 0, 2.2f);

				anim = GameObject.Find ("Guitariste").GetComponentInChildren<Animator> ();
				anim.SetBool ("isWalking", true);
				animLune.SetInteger ("moonColor", 4);
			}
			else if (index == 2)
			{
				index++;
				agent = GameObject.Find ("Ukulele").GetComponent<UnityEngine.AI.NavMeshAgent> ();
				agent.destination = new Vector3 (0.2f, 0, 1.1f);

				anim = GameObject.Find ("Ukulele").GetComponentInChildren<Animator> ();
				anim.SetBool ("isWalking", true);
				animLune.SetInteger ("moonColor", 3);
			}
			else if (index == 3)
			{
				animLune.SetInteger ("moonColor", 0);
				this.GetComponent<singerSimon> ().enabled = true;
				this.GetComponent<letThemCome> ().enabled = false;
			}
		}
	}
}

