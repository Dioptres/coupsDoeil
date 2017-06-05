using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fouleManager : MonoBehaviour {

	public float tempsMinBeforeAct = 1;
	public float tempsMaxBeforeAct = 4;
	float timeBeforeAction;

	public int nbrDeFoule;
	int fouleToActivate;

	// Use this for initialization
	void Start () {
		timeBeforeAction = 0;

		timeBeforeAction = Random.Range (tempsMinBeforeAct, tempsMaxBeforeAct);

		fouleToActivate = Random.Range (0, nbrDeFoule);
	}
	
	// Update is called once per frame
	void Update () {
		if(timeBeforeAction<=0)
		{
			Debug.Log ("ACTIIIIII   " + fouleToActivate);

			this.transform.GetChild (fouleToActivate).GetComponent<crowdBehavior> ().action ();

			timeBeforeAction = Random.Range (tempsMinBeforeAct, tempsMaxBeforeAct);
			fouleToActivate = Random.Range (0, nbrDeFoule);
		}
		else
		{
			timeBeforeAction -= Time.deltaTime;
		}
	}
}
