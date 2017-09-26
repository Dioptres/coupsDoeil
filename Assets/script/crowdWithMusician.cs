using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crowdWithMusician : MonoBehaviour {

	public Transform cible;
	public bool lookAt = false;
	Animator anim;

	void lookAtMusi (Transform musician)
	{
		if(this.transform.position.x - musician.position.x > 0)
		{
			if(Vector3.Angle (new Vector3 (0, 0, -1), this.transform.position - musician.position) < 60)
			{
				anim.SetInteger ("direction", 3);
			}
			else if(Vector3.Angle (new Vector3 (0, 0, -1), this.transform.position - musician.position) < 120)
			{
				anim.SetInteger ("direction", 2);
			}
			else if(Vector3.Angle (new Vector3 (0, 0, -1), this.transform.position - musician.position) < 180)
			{
				anim.SetInteger ("direction", 1);
			}
		}
		else if(this.transform.position.x - musician.position.x < 0)
		{
			if (Vector3.Angle (new Vector3 (0, 0, -1), this.transform.position - musician.position) < 60)
			{
				anim.SetInteger ("direction", 4);
			}
			else if (Vector3.Angle (new Vector3 (0, 0, -1), this.transform.position - musician.position) < 120)
			{
				anim.SetInteger ("direction", 5);
			}
			else if (Vector3.Angle (new Vector3 (0, 0, -1), this.transform.position - musician.position) < 180)
			{
				anim.SetInteger ("direction", 0);
			}
		}
	}

	// Use this for initialization
	void Start () {
		int temp = Random.Range (0, 4);
		Debug.Log (temp);

		anim = GetComponentInChildren<Animator> ();

		anim.SetInteger ("villagerType", temp);

		anim = GetComponentInChildren<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(lookAt)
		{
			lookAtMusi (cible);
		}
	}
}
