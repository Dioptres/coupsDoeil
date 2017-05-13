using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyFirefly : MonoBehaviour {

    public float timeToDeath;

    private float timer = 0;
	
    public AudioClip[] fireflySound;
    private AudioSource fireflySource;

	private void Start ()
	{
		fireflySource = GetComponent<AudioSource> ();

		fireflySource.loop = true;
		
        fireflySource.clip = fireflySound[Random.Range(0, fireflySound.Length)];
        fireflySource.Play();
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
