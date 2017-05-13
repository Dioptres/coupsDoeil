using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followFromDist : MonoBehaviour {

	public float dist;
	public Transform followed;
	public Animator anim;

	Vector3 move;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(this.transform.position, followed.position)> dist)
		{
			move = this.transform.position - followed.position;
			move = move.normalized;


			this.transform.position = followed.transform.position + move * dist;

			if(Mathf.Abs(move.x)>Mathf.Abs(move.z))
			{
				anim.SetInteger ("direction", 2);
				
			}
			else
			{
				if (move.z < 0)
				{
					anim.SetInteger ("direction", 1);
				}
				else if (move.z > 0)
				{
					anim.SetInteger ("direction", 0);
				}
			}
		}
	}
}
