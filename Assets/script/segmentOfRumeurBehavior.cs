using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class segmentOfRumeurBehavior : MonoBehaviour {

	Animator anim;
	Vector3 myPreviousPos;
	bool isWalking;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		isWalking = false;
		myPreviousPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(myPreviousPos == this.transform.position && isWalking)
		{
			isWalking = false;
			anim.SetBool ("isWalking", false);
		}
		else if (myPreviousPos != this.transform.position)
		{
			if(Mathf.Abs(myPreviousPos.x - this.transform.position.x) > Mathf.Abs (myPreviousPos.z - this.transform.position.z))
			{
				if(myPreviousPos.x - this.transform.position.x > 0)
				{
					anim.SetInteger ("segmentDirection", 1);
				}
				else
				{
					anim.SetInteger ("segmentDirection", 3);
				}
			}
			else
			{
				if (myPreviousPos.z - this.transform.position.z > 0)
				{
					anim.SetInteger ("segmentDirection", 0);
				}
				else
				{
					anim.SetInteger ("segmentDirection", 2);
				}
			}

			if (!isWalking)
			{
				isWalking = true;
				anim.SetBool ("isWalking", true);
			}
		}

		myPreviousPos = this.transform.position;
	}
}
