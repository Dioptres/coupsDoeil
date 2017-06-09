using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letThemCome : MonoBehaviour {

	UnityEngine.AI.NavMeshAgent agent;
	Animator anim;
	Animator animLune;



	public int indexLune = 1;

	int index;
	bool selected;
	bool wait;

	public GameObject[] chanteurs;


	// Use this for initialization
	void Start () {
		index = -1;
		anim = GameObject.Find ("Bassiste").GetComponentInChildren<Animator> ();
		animLune = GetComponentInChildren<Animator> ();
		animLune.SetInteger ("moonColor", 1);
		Debug.Log ("light me ip");
		selected = false;
		wait = false;
	}

	void Update ()
	{
		if(!wait && selected)
		{
			wait = true;
		}
		else if (selected)
		{
			if (agent.remainingDistance == 0)
			{
				selected = false;
				wait = false;
				index++;
				anim.SetBool ("isWalking", false);
				switch (index)
				{
					case 0:
						animLune.SetInteger ("moonColor", 2);
						break;
					case 1:
						animLune.SetInteger ("moonColor", 4);
						break;
					case 2:
						animLune.SetInteger ("moonColor", 3);
						break;
					case 3:
						animLune.SetInteger ("moonColor", 0);
						break;
				}
				if (index == 3)
				{
					foreach(GameObject chanteur in chanteurs)
					{
						chanteur.GetComponent<musicianSimon>().becomeSimon();
					}

					this.GetComponent<singerSimon> ().enabled = true;
					this.GetComponent<letThemCome> ().enabled = false;
				}
			}
		}
	}

	// Update is called once per frame
	public void moveToDestination (int musicosNbr)
	{
			

		if (index == -1 && musicosNbr == 1 && !selected)
		{
			selected = true;

			animLune = GetComponentInChildren<Animator> ();
			agent = GameObject.Find ("Bassiste").GetComponent<UnityEngine.AI.NavMeshAgent> ();
			agent.destination = new Vector3 (-3.6f, 0, -1.2f);

			anim.SetBool ("isWalking", true);
		}
		else if (index == 0 && musicosNbr == 2 && !selected)
		{
			selected = true;
			agent = GameObject.Find ("Flûtiste").GetComponent<UnityEngine.AI.NavMeshAgent> ();
			agent.destination = new Vector3 (-1.1f, 0, -1.8f);

			anim = GameObject.Find ("Flûtiste").GetComponentInChildren<Animator> ();
			anim.SetBool ("isWalking", true);
		}
		else if (index == 1 && musicosNbr == 4 && !selected)
		{
			selected = true;
			agent = GameObject.Find ("Guitariste").GetComponent<UnityEngine.AI.NavMeshAgent> ();
			agent.destination = new Vector3 (-1.5f, 0, 2.2f);

			anim = GameObject.Find ("Guitariste").GetComponentInChildren<Animator> ();
			anim.SetBool ("isWalking", true);
		}
		else if (index == 2 && musicosNbr == 3 && !selected)
		{
			selected = true;
			agent = GameObject.Find ("Ukulele").GetComponent<UnityEngine.AI.NavMeshAgent> ();
			agent.destination = new Vector3 (0.2f, 0, 1.1f);

			anim = GameObject.Find ("Ukulele").GetComponentInChildren<Animator> ();
			anim.SetBool ("isWalking", true);
		}
		 
	}
}

