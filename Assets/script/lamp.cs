using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lamp : MonoBehaviour {

	
	Light mine;

	Animator anim;

	bool lighten;

	private AudioSource source;

	// Use this for initialization
	void Start () {
		anim = GetComponentInChildren<Animator> ();
		source = GetComponent<AudioSource> ();
		lighten = false;
		mine = GetComponentInChildren<Light> ();
	}
	
	// Update is called once per frame
	public void Update () {
		if(lighten && mine.intensity == 0)
		{
			lighten = false;
			anim.SetTrigger ("lightOff");
		}
		else if (!lighten && mine.intensity >0)
		{
			lighten = true;
			anim.SetTrigger ("lightOn");
		}
	}
}
