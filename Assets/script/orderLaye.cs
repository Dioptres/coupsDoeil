using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orderLaye : MonoBehaviour {

	GameObject[] vegetables;
	GameObject[] characters;
	bool alreadyMoved;

	// Use this for initialization
	void Start ()
	{
		vegetables = GameObject.FindGameObjectsWithTag ("vegetable");
		characters = GameObject.FindGameObjectsWithTag ("character");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		foreach(GameObject character in characters)
		{
			foreach(GameObject vegetable in vegetables)
			{
				if(Vector3.Distance(character.transform.position, vegetable.transform.position)<2)
				{
					if (!character.GetComponent<alreadyMoved> ().alreadyMove)
					{
						if (character.transform.position.z < vegetable.transform.position.z)
						{
							if(character.GetComponent<SpriteRenderer> ().sortingOrder < vegetable.GetComponent<SpriteRenderer> ().sortingOrder)
							{
								character.GetComponent<SpriteRenderer> ().sortingOrder = vegetable.GetComponent<SpriteRenderer> ().sortingOrder += 2;
								character.GetComponent<alreadyMoved> ().alreadyMove = true;
							}
						}
						else
						{
							if (character.GetComponent<SpriteRenderer> ().sortingOrder > vegetable.GetComponent<SpriteRenderer> ().sortingOrder)
							{
								character.GetComponent<SpriteRenderer> ().sortingOrder = vegetable.GetComponent<SpriteRenderer> ().sortingOrder -= 2;
								character.GetComponent<alreadyMoved> ().alreadyMove = true;
							}
						}
					}
					else
					{
						if (character.transform.position.z < vegetable.transform.position.z)
						{
							if (character.GetComponent<SpriteRenderer> ().sortingOrder < vegetable.GetComponent<SpriteRenderer> ().sortingOrder)
							{
								vegetable.GetComponent<SpriteRenderer> ().sortingOrder = character.GetComponent<SpriteRenderer> ().sortingOrder--;
							}
						}
						else
						{
							if (character.GetComponent<SpriteRenderer> ().sortingOrder > vegetable.GetComponent<SpriteRenderer> ().sortingOrder)
							{
								vegetable.GetComponent<SpriteRenderer> ().sortingOrder = character.GetComponent<SpriteRenderer> ().sortingOrder++;
							}
						}
					}
				}
			}
		}
	}

	private void LateUpdate ()
	{
		alreadyMoved = false;
	}
}
