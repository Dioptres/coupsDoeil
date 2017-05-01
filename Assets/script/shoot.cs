using System.Collections;
using System.Collections.Generic;
using Tobii.EyeTracking;
using UnityEngine;

public class shoot : Lookable {

	Vector3 dir;
	GazePoint gazePoint;
	public GameObject bullet;

	public override void Update ()
	{
		base.Update ();
		gazePoint = EyeTracking.GetGazePoint ();
	}

	public override void QuitSee ()
	{
		Debug.Log ("quitsee");
		dir = Camera.main.ScreenToWorldPoint (new Vector3 (gazePoint.Screen.x, gazePoint.Screen.y, 10)) - this.transform.position;
		dir = dir.normalized;
		GameObject firedBullet = Instantiate (bullet, this.transform.position, Quaternion.identity);
		firedBullet.GetComponent<bullet> ().dir = dir;
	}

	public override void DoAction ()
	{
		Debug.Log ("doAction");
	}


}
