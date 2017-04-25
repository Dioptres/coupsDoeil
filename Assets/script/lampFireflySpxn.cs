using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lampFireflySpxn : Lookable {

	GameObject fireFly1;

	GameObject fireFly2;

	GameObject fireFly3;

	GameObject fireFly4;

	Vector3 move;

	bool exist = false;

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
		
		fireFly1 = Instantiate (new GameObject());
		fireFly1.AddComponent<Light>();
		fireFly1.name = "fireFly1";

		fireFly2 = Instantiate (new GameObject ());
		fireFly2.AddComponent<Light> ();
		fireFly2.name = "fireFly2";

		fireFly3 = Instantiate (new GameObject ());
		fireFly3.AddComponent<Light> ();
		fireFly3.name = "fireFly3";

		fireFly4 = Instantiate (new GameObject ());
		fireFly4.AddComponent<Light> ();
		fireFly4.name = "fireFly4";

		exist = true;

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
