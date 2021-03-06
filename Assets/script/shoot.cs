﻿using System.Collections;
using System.Collections.Generic;
using Tobii.EyeTracking;
using UnityEngine;

public class shoot : Lookable {

	Vector3 dir;
	GazePoint gazePoint;
	public GameObject bullet;

	public bool active = false;

	protected override void UpdateLookable () {
		base.UpdateLookable ();
		gazePoint = EyeTracking.GetGazePoint ();
	}

	public override void QuitSee () {
		if(active)
		{
			
			dir = GameManager.whereIlook - this.transform.position;
			dir = dir.normalized;
			dir = new Vector3 (dir.x, 0, dir.z);

			Debug.Log (dir);

			GameObject firedBullet = Instantiate (bullet, this.transform.position, Quaternion.identity);
			firedBullet.GetComponent<bullet> ().dir = dir;
		}
	}

	
}
