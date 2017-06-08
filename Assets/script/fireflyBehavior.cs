using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireflyBehavior : Lookable {

	public Color myColor;
	public GameObject lampe;
	float timer;

	Animator anim;

	public float speedMin = 0.5f;
	public float speedMax = 1.5f;

	public GameObject fireWorks;

	public float speed;

	// Use this for initialization
	protected override void StartLookable ()
	{
		base.StartLookable ();

		anim = GetComponentInChildren<Animator> ();
		timer = 0;

		speed = Random.Range (speedMin, speedMax);

		if(myColor == Color.cyan)
		{
			anim.SetBool ("isCyan", true);
		}
		else if (myColor == Color.magenta)
		{
			anim.SetBool ("isMagenta", true);
		}
		else if (myColor == Color.yellow)
		{
			anim.SetBool ("isYellow", true);
		}

		
	}



	// Update is called once per frame
	protected override void UpdateLookable ()
	{
		base.UpdateLookable ();
	
		timer += Time.deltaTime;
		transform.Translate (Vector3.forward * Time.deltaTime * speed);


		if(this.transform.position.x < -13 || this.transform.position.x > 12 || this.transform.position.z < -7 || this.transform.position.z > 8)
		{
			Destroy (gameObject);
		}
	}

	public override void DoAction ()
	{
		if (myColor == lampe.GetComponent<sayWhichOneToExplode> ().GetComponentInChildren<Light> ().color || lampe.GetComponent<sayWhichOneToExplode> ().GetComponentInChildren<sayWhichOneToExplode> ().lastFireworks)
		{
			lampe.GetComponent<sayWhichOneToExplode> ().addFirefly ();
			Instantiate (fireWorks, this.transform.position, Quaternion.identity);
		}
		else
		{
			Debug.Log ("bad");
		}
		Destroy (gameObject);
	}

	private void OnDestroy ()
	{
		lampe.GetComponent<sayWhichOneToExplode> ().removeOne (myColor);
	}
}
