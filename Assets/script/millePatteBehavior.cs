using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class millePatteBehavior : MonoBehaviour {

	public float timer = 5;
	public bool active;
	public bool hasStarted;
	GameObject lampChoosen;
	GameObject actualSpwnPoint;
	bool sleep;

	int securite;

	GameObject[] lampes;

	GameObject[] spwnPoints;


	public float timeSpentSleeping = 5f;

	public void deactivate()
	{
		timer = 5;
		securite = 0;
		foreach (Transform child in transform)
		{
			child.gameObject.SetActive (false);
		}
		active = false;
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
	
	// Update is called once per frame
	void Update () {
		timeSpentSleeping -= Time.deltaTime;
		if(timeSpentSleeping < 0 && sleep)
		{
			Debug.Log ("WAKE UP");
			sleep = false;
			AkSoundEngine.PostEvent ("Rumeur_reveil", gameObject);
			Destroy (this.transform.GetChild (0).gameObject);
			foreach (Transform child in this.transform)
			{
				child.gameObject.SetActive (true);
			}
		}

		if (hasStarted)
		{
			timer -= Time.deltaTime;
			if (timer <= 0 && !active)
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
}
