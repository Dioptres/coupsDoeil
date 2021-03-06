﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class millePatteBehavior : MonoBehaviour {

	public float timer = 5;
	float trueTimer;
	public bool active;
	public bool hasStarted;
	GameObject lampChoosen;
	GameObject actualSpwnPoint;

	bool sleep;



	int securite;

	GameObject[] lampes;

	GameObject[] spwnPoints;


	public void deactivate()
	{
		trueTimer = timer;
		securite = 0;
		foreach (Transform child in transform)
		{
			child.gameObject.SetActive (false);
		}
		active = false;
		AkSoundEngine.PostEvent ("Rumeur_stopmarche", gameObject);
	}

	void OnDestroy ()
	{
		AkSoundEngine.PostEvent ("Rumeur_stopmarche", gameObject);
	}
	
	// Use this for initialization
	void Start ()
	{
		sleep = true;

		hasStarted = false;
		securite = 0;
		lampes = GameObject.FindGameObjectsWithTag ("lampe");
		spwnPoints = GameObject.FindGameObjectsWithTag ("spwnRumeur");
		foreach (Transform child in transform)
		{
			child.gameObject.SetActive(false);
		}
		transform.GetChild (0).gameObject.SetActive (true);
		active = false;
	}
	
	public void wakeMeUp()
	{
		sleep = false;
		AkSoundEngine.PostEvent ("Rumeur_reveil", gameObject);
		Destroy (this.transform.GetChild (0).gameObject);
		foreach (Transform child in this.transform)
		{
			child.gameObject.SetActive (true);
			AkSoundEngine.PostEvent ("Rumeur_marche", gameObject);
		}
	}

	// Update is called once per frame
	void Update () {

		
			
	



		if (hasStarted)
		{
			trueTimer -= Time.deltaTime;
			if (trueTimer <= 0 && !active)
			{

				lampChoosen = lampes[0]; //DELETE
				do
				{
					lampChoosen = lampes[Random.Range (0, lampes.Length)];
					securite++;
				} while (lampChoosen.transform.GetChild (0).GetComponent<Light> ().intensity == 0 && securite < 20);

				if (securite != 20)
				{
					actualSpwnPoint = spwnPoints[0];
					for (int i = 1; i < spwnPoints.Length; i++)
					{
						if (Vector3.Distance (lampChoosen.transform.position, spwnPoints[i].transform.position) < Vector3.Distance (lampChoosen.transform.position, actualSpwnPoint.transform.position))
						{
							actualSpwnPoint = spwnPoints[i];
						}
					}


					foreach (Transform child in transform)
					{
						child.position = actualSpwnPoint.transform.position;
						child.gameObject.SetActive (true);
					}


					this.transform.GetChild (0).gameObject.GetComponent<RumeurBehavior> ().terrier = actualSpwnPoint.transform;
					this.transform.GetChild (0).gameObject.GetComponent<RumeurBehavior> ().checkPoints = lampChoosen.transform;
					transform.GetChild (0).GetComponent<RumeurBehavior> ().launch ();
					AkSoundEngine.PostEvent ("Rumeur_marche", gameObject);
					active = true;
				}
				else
				{
					trueTimer = timer;
				}

				securite = 0;
			}
		}
	}
}
