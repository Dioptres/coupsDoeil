using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CROWDmANAGER : MonoBehaviour {

	public static GameObject[] crowds;

	// Use this for initialization
	void Start () {
		crowds = GameObject.FindGameObjectsWithTag ("crowd");
	}
	
	// Update is called once per frame
	public static void giveCible (Transform cible)
	{
		foreach (GameObject crowd in crowds)
		{
			crowd.GetComponent<crowdWithMusician> ().lookAt = true;
			crowd.GetComponent<crowdWithMusician> ().cible = cible;
		}
	}

	public static void removeCibe ()
	{
		foreach (GameObject crowd in crowds)
		{
			crowd.GetComponent<crowdWithMusician> ().lookAt = false;
		}
	}
}
