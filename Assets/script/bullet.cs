using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

	public Vector3 dir;

	GameObject[] lampes;

	// Use this for initialization
	void Start ()
	{
		lampes = GameObject.FindGameObjectsWithTag ("lampe");
	}

	void OnBecameInvisible ()
	{
		Destroy (gameObject);
	}


	// Update is called once per frame
	void Update () {
		transform.Translate (dir * Time.deltaTime);

		foreach(GameObject lampe in lampes)
		{
			if(Vector3.Distance(this.transform.position, lampe.transform.position)<1 && lampe.transform.GetChild (0).GetComponent<Light> ().intensity == 0)
			{
				lampe.transform.GetChild(0).GetComponent<Light> ().intensity = 1;
				Destroy (gameObject);
			}
		}
	}
}
