using System.Collections;
using System.Collections.Generic;
using Tobii.EyeTracking;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public float thatsAllFolks = 120;

	public enum fadeState
	{
		None,
		FadeIn,
		FadeOut
	}

	public static fadeState fadeToDo = fadeState.None;

	public float timeBeforeFadeIn = 2;

	public float speedFading = 1;

	public SpriteRenderer toBeFaded;
	public TextMesh toBeFaded2;

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

	bool fadeStart;

	// Use this for initialization
	void Awake ()
	{

		fadeStart = false;


		lookables = GameObject.FindGameObjectsWithTag ("Lookable");
		lookables2 = GameObject.FindGameObjectsWithTag ("luciole");
		TimeSincelastMusicianSeen = 99999999;
		numberMaxMusiGroupTogether = 1;
		ready = true;

	}

	// Update is called once per frame
	void Update ()
	{
		if(timeBeforeFadeIn <= 0 && !fadeStart)
		{
			fadeStart = true;
			fadeToDo = fadeState.FadeIn;
		}
		else if(!fadeStart)
		{
			timeBeforeFadeIn -= Time.deltaTime;
		}


		if(thatsAllFolks <= 0)
		{
			fadeToDo = fadeState.FadeOut;
		}
		thatsAllFolks -= Time.deltaTime;

		if (fadeToDo == fadeState.FadeIn)
		{
			Color tempColor = toBeFaded.color;
			Color tempColor2 = toBeFaded2.color;
			tempColor.a -= speedFading * Time.deltaTime;
			tempColor2.a -= speedFading * Time.deltaTime;
			toBeFaded.color = tempColor;
			toBeFaded2.color = tempColor2;
			if (tempColor.a <= 0)
			{
				toBeFaded2.color = tempColor;
				fadeToDo = fadeState.None;
			}
		}
		else if (fadeToDo == fadeState.FadeOut)
		{
			Color tempColor = toBeFaded.color;
			tempColor.a += speedFading * Time.deltaTime;
			toBeFaded.color = tempColor;
			Debug.Log (tempColor.a);
			if (tempColor.a >= 1)
			{
				fadeToDo = fadeState.None;
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);

			}
		}

		TimeSincelastMusicianSeen += Time.deltaTime;
		lookables = GameObject.FindGameObjectsWithTag ("Lookable");
		lookables2 = GameObject.FindGameObjectsWithTag ("luciole");

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
