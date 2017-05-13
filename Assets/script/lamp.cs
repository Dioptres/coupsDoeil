using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lamp : MonoBehaviour {

	public AudioClip son1;
	public AudioClip son2;
	public AudioClip son3;
	public AudioClip son4;

	private AudioSource source;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	public void song () {
		int rand = Random.Range (1, 5);
		switch(rand)
		{
			case 1 :
				source.PlayOneShot (son1);
				break;
			case 2:
				source.PlayOneShot (son2);
				break;
			case 3:
				source.PlayOneShot (son3);
				break;
			case 4:
				source.PlayOneShot (son4);
				break;
		}
	}
}
