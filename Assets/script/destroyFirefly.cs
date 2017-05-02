using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyFirefly : MonoBehaviour {

    public float timeToDeath;

    private float timer = 0;

	
	void Update ()
    {
        timer += Time.deltaTime;

        if (timer >= timeToDeath)
        {
            Destroy(gameObject);
        }
	}
}
