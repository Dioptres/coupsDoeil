using System.Collections;
using System.Collections.Generic;
using Tobii.EyeTracking;
using UnityEngine;

public class GameManager : MonoBehaviour {

	Vector3 whereIlook;
	GazePoint gazePoint;

	GameObject[] lookables;

	// Use this for initialization
	void Start () {
		lookables = GameObject.FindGameObjectsWithTag ("Lookable");
	}
	
	// Update is called once per frame
	void Update () {
		gazePoint = EyeTracking.GetGazePoint ();
		if (gazePoint.IsValid)
		{
			whereIlook = Camera.main.ScreenToWorldPoint (new Vector3 (gazePoint.Screen.x, gazePoint.Screen.y, 10));
			foreach(GameObject toLook in lookables)
			{
				if(Vector3.Distance(whereIlook,toLook.transform.position) < toLook.GetComponent<Lookable>().distanceDeVision)
				{
					toLook.GetComponent<Lookable> ().looked = true;

				}
			}
		}
		
	}
}
