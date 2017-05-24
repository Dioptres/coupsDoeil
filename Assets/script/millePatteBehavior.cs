﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class millePatteBehavior : MonoBehaviour {

	public float timer = 5;
	bool active;
	GameObject lampChoosen;
	GameObject actualSpwnPoint;

	int securite;

	GameObject[] lampes;

	GameObject[] spwnPoints;

	// Use this for initialization
	void Start ()
	{
		securite = 0;
		lampes = GameObject.FindGameObjectsWithTag ("lampe");
		spwnPoints = GameObject.FindGameObjectsWithTag ("spwnRumeur");
		foreach (Transform child in transform)
		{
			child.gameObject.SetActive(false);
		}
		active = false;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if(timer <= 0 && !active)
		{

			lampChoosen = lampes[0]; //DELETE
			do
			{
				lampChoosen = lampes[Random.Range (0, lampes.Length)];
				securite++;
			} while (lampChoosen.transform.GetChild(0).GetComponent<Light>().intensity == 0 && securite < 20);

			if(securite != 20)
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
				active = true;
			}
			else
			{
				timer = 5;
			}

			securite = 0;
		}
	}
}
