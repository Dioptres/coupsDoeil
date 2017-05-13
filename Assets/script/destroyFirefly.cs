using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyFirefly : MonoBehaviour {

    public float timeToDeath;

    private float timer = 0;

	private AudioSource source;

	public AudioClip fireFly;

	private void Start ()
	{
		source = GetComponent<AudioSource> ();

		source.loop = true;
		source.clip = fireFly;
		source.Play ();
	}

	void Update ()
    {

        timer += Time.deltaTime;

        if (timer >= timeToDeath)
        {
            Destroy(gameObject);
        }
	}
}
