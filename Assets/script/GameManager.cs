using System.Collections;
using System.Collections.Generic;
using Tobii.EyeTracking;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	public GameObject lastMusicianSeen;
	public float TimeSincelastMusicianSeen;
	float timeBeforeSelecMusic;

	bool ready;

	public static int numberMaxMusiGroupTogether = 1;

	bool haveSeenMusician;

	public static Vector3 whereIlook;
	public static GazePoint gazePoint;

	public static GameObject[] lookables;
	public GameObject[] lookables2;

	// Use this for initialization
	void Awake ()
	{
		lookables = GameObject.FindGameObjectsWithTag ("Lookable");
		lookables2 = GameObject.FindGameObjectsWithTag ("place");
		TimeSincelastMusicianSeen = 99999999;
		numberMaxMusiGroupTogether = 1;
		ready = true;

	}

	// Update is called once per frame
	void Update ()
	{
		TimeSincelastMusicianSeen += Time.deltaTime;
		lookables = GameObject.FindGameObjectsWithTag ("Lookable");
		lookables2 = GameObject.FindGameObjectsWithTag ("place");

		gazePoint = EyeTracking.GetGazePoint ();
		if (true)
		{
			//whereIlook = Camera.main.ScreenToWorldPoint (new Vector3 (gazePoint.Screen.x, gazePoint.Screen.y, 5.5f));
			whereIlook = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 5.5f));
			haveSeenMusician = false;
			foreach (GameObject toLook in lookables)
			{
				if (Vector3.Distance (whereIlook, toLook.transform.position) < toLook.GetComponent<Lookable> ().distanceDeVision)
				{

					if (toLook.layer == 8)
					{
						haveSeenMusician = true;
						if (ready)
						{
							lastMusicianSeen = toLook;
						}
						ready = !ready;


						TimeSincelastMusicianSeen = 0;
					}
					Lookable[] lo = toLook.GetComponents<Lookable> ();
					foreach (Lookable loo in lo)
					{
						loo.looked = Lookable.StareState.Looking;
					}
					//Debug.Log (toLook.GetComponent<Lookable> ().looked);
				}
				else
				{
					if (toLook.GetComponent<Lookable> ().looked == Lookable.StareState.Looking)
					{
						Lookable[] lo = toLook.GetComponents<Lookable> ();
						foreach (Lookable loo in lo)
						{
							loo.looked = Lookable.StareState.LosingSight;
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
						loo.looked = Lookable.StareState.Looking;
					}
					//Debug.Log (toLook.GetComponent<Lookable> ().looked);
				}
				else
				{
					if (toLook.GetComponent<Lookable> ().looked == Lookable.StareState.Looking)
					{
						Lookable[] lo = toLook.GetComponents<Lookable> ();
						foreach (Lookable loo in lo)
						{
							loo.looked = Lookable.StareState.LosingSight;
						}
					}
				}
			}
		}
	}
}
