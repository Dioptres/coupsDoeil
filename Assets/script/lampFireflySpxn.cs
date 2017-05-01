using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lampFireflySpxn : Lookable {

	GameObject fireFly1;

	GameObject fireFly2;

	GameObject fireFly3;

	GameObject fireFly4;

	Vector3 move;

	Color fireflyColor;

	public float distanceActivationLampe;

	bool exist = false;

	GameObject[] lampes;

	public override void DoAction()
	{
		float randA = Random.Range(-1f,1f);
		float randB = Random.Range (-1f, 1f);

		if(randA == 0 && randB == 0)
		{
			randA = 1;
			randB = 1;
		}

		move = new Vector3(randA,0,randB);

		Debug.Log (move);

		if(exist)
		{
			Destroy (fireFly1);
			Destroy (fireFly2);
			Destroy (fireFly3);
			Destroy (fireFly4);
		}

		fireflyColor = new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f));

		fireFly1 = Instantiate (new GameObject (), this.transform.position, Quaternion.identity);
		fireFly1.AddComponent<Light>();
		fireFly1.GetComponent<Light> ().color = fireflyColor;
		fireFly1.name = "fireFly1";

		fireFly2 = Instantiate (new GameObject (), this.transform.position, Quaternion.identity);
		fireFly2.AddComponent<Light> ();
		fireFly2.GetComponent<Light> ().color = fireflyColor;
		fireFly2.name = "fireFly2";

		fireFly3 = Instantiate (new GameObject (), this.transform.position, Quaternion.identity);
		fireFly3.AddComponent<Light> ();
		fireFly3.GetComponent<Light> ().color = fireflyColor;
		fireFly3.name = "fireFly3";

		fireFly4 = Instantiate (new GameObject (), this.transform.position, Quaternion.identity);
		fireFly4.AddComponent<Light> ();
		fireFly4.GetComponent<Light> ().color = fireflyColor;
		fireFly4.name = "fireFly4";

		exist = true;

		lampes = GameObject.FindGameObjectsWithTag ("lampe");

		foreach (GameObject lampe in lampes)
		{
			if (Vector3.Distance (this.transform.position, lampe.transform.position) < distanceActivationLampe)
			{
				lampe.GetComponentInChildren<Light> ().intensity = 1;
				lampe.GetComponentInChildren<Light> ().color = fireflyColor;
			}
		}

	}

	public override void Update ()
	{
		base.Update ();

		if (Input.GetKeyDown ("space"))
		{
			DoAction ();
		}

			if (exist)
		{
			fireFly1.transform.Translate (move.normalized*Time.deltaTime);
			fireFly2.transform.Translate (-move.normalized * Time.deltaTime);
			fireFly3.transform.Translate ((Quaternion.AngleAxis (-90, Vector3.up) * move).normalized * Time.deltaTime);
			fireFly4.transform.Translate ((Quaternion.AngleAxis (90, Vector3.up) * move).normalized * Time.deltaTime);

		}
	}
}
