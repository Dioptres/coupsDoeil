using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class layerOrder : MonoBehaviour {

	GameObject[] vegetables;
	GameObject[] characters;

	// Use this for initialization
	void Start () {
		vegetables = GameObject.FindGameObjectsWithTag ("vegetable");
		characters = GameObject.FindGameObjectsWithTag ("character");
	}
	
	// Update is called once per frame
	void Update () {
		foreach(GameObject character in characters)
		{
			foreach(GameObject vegetable in vegetables)
			{
				if(Vector3.Distance(vegetable.transform.position, character.transform.position)<1)
				{
					if(vegetable.transform.position.z < character.transform.position.z)
					{
						character.GetComponent<SpriteRenderer> ().sortingOrder = -1;
					}
					else
					{
						character.GetComponent<SpriteRenderer> ().sortingOrder = 15;
					}
				}
			}
		}
	}
}
