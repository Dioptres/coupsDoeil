using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicianBehavior : Lookable
{

	public float distanceOtherMusician;
	int nbrOfMusician;

	public GameObject[] Musician;

	public AudioClip son1;
	public AudioClip son2;
	public AudioClip superSon;

	private AudioSource source;

	private void Awake ()
	{
		nbrOfMusician = 0;
		source = GetComponent<AudioSource> ();
	}

	public override void Start ()
	{
		base.Start ();
		GameObject[] temps;
		int i = 0;
		temps = GameObject.FindGameObjectsWithTag ("Lookable");
		foreach (GameObject go in temps)
		{
			if (go.layer == 8)
			{
				Musician[i] = go;
				i++;
			}
		}
	}

	public override void Update ()
	{
		base.Update ();

		Debug.Log (nbrOfMusician + "   " + this.name);

		if (Input.GetKeyDown ("space"))
		{
			DoAction ();
		}


		nbrOfMusician = 0;
		foreach (GameObject musicos in Musician)
		{
			if(Vector3.Distance(this.transform.position, musicos.transform.position) < distanceOtherMusician)
			{
				nbrOfMusician++;
			}
		}
		if (nbrOfMusician == 5)
		{
			source.loop = false;
			if (superSon != null)
			{
				if(source.loop != true && source.clip != superSon)
				{
					source.loop = true;
					source.clip = superSon;
					source.Play ();
					Debug.Log ("do3  " + this.name);
				}
				
			}
			else
			{
				source.loop = false;
				source.clip = null;
			}
		}
		else if (nbrOfMusician > 1)
		{
			if (source.loop != true && source.clip != son2)
			{
				source.loop = true;
				source.clip = son2;
				source.Play ();
				Debug.Log ("do2 " + this.name);
			}
		}
		else
		{
			if (source.loop)
			{
				source.Stop ();
			}
		}
	}

	public override void DoAction ()
	{
		if(nbrOfMusician == 1)
		{
			source.PlayOneShot (son1);
			Debug.Log ("do1 " + this.name);
		}
	}
}
