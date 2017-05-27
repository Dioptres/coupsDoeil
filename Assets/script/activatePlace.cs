using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activatePlace : MonoBehaviour
{

	GameObject lampWakingMeUp;
	GameObject[] lampes;

	// Use this for initialization
	void Start ()
	{
		lampes = GameObject.FindGameObjectsWithTag ("lampe");
		lampWakingMeUp = lampes[0];
		for (int i = 1; i < lampes.Length; i++)
		{
			if (Vector3.Distance (lampes[i].transform.position, this.transform.position) < Vector3.Distance (lampWakingMeUp.transform.position, this.transform.position))
			{
				lampWakingMeUp = lampes[i];
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (lampWakingMeUp.transform.GetChild (0).GetComponent<Light> ().intensity > 0)
		{
			this.tag = "place";
		}
	}
}
