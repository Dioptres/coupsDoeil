using System.Collections;
using System.Collections.Generic;
using Tobii.EyeTracking;
using UnityEngine;

public class GameManager : MonoBehaviour {

	Vector3 whereIlook;
	GazePoint gazePoint;

	GameObject[] lookables;
	public GameObject[] lookables2;

	// Use this for initialization
	void Start () {
		lookables = GameObject.FindGameObjectsWithTag ("Lookable");
		lookables2 = GameObject.FindGameObjectsWithTag ("lampe");
	}
	
	// Update is called once per frame
	void Update () {

		lookables = GameObject.FindGameObjectsWithTag ("Lookable");
		lookables2 = GameObject.FindGameObjectsWithTag ("lampe");

		gazePoint = EyeTracking.GetGazePoint ();
		if (gazePoint.IsValid)
		{
			whereIlook = Camera.main.ScreenToWorldPoint (new Vector3 (gazePoint.Screen.x, gazePoint.Screen.y, 5.5f));
			foreach(GameObject toLook in lookables)
			{
				if(Vector3.Distance(whereIlook,toLook.transform.position) < toLook.GetComponent<Lookable>().distanceDeVision)
				{

					Lookable[] lo = toLook.GetComponents<Lookable> ();
					foreach (Lookable loo in lo)
					{
						loo.looked = 2;
					}
					//Debug.Log (toLook.GetComponent<Lookable> ().looked);
				}
				else
				{
					if(toLook.GetComponent<Lookable> ().looked == 2)
					{
						Lookable[] lo = toLook.GetComponents<Lookable> ();
						foreach (Lookable loo in lo)
						{
							loo.looked = 1;
						}
					}
				}
			}
			foreach (GameObject toLook in lookables2)
			{
				if (Vector3.Distance (whereIlook, toLook.transform.position) < toLook.GetComponent<Lookable> ().distanceDeVision)
				{

					Lookable[] lo = toLook.GetComponents<Lookable> ();
					foreach (Lookable loo in lo)
					{
						loo.looked = 2;
					}
					//Debug.Log (toLook.GetComponent<Lookable> ().looked);
				}
				else
				{
					if (toLook.GetComponent<Lookable> ().looked == 2)
					{
						Lookable[] lo = toLook.GetComponents<Lookable> ();
						foreach (Lookable loo in lo)
						{
							loo.looked = 1;
						}
					}
				}
			}
		}
		
	}
}
