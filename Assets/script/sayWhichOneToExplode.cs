using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sayWhichOneToExplode : MonoBehaviour {

	public float minTimeBeforeChange = 3;
	public float maxTimeBeforeChange = 10;
	float timerBeforeChange;

	public float destXleft = -2;
	public float destXright = 3;
	public float destYleft = -2;
	public float destYright = 3;

	public int numberOfLuciolForEachColor = 30;

	public int chanceOfAppearingLeft = 2;
	public int chanceOfAppearinRight = 2;
	public int chanceOfAppearinUp = 8;
	public int chanceOfAppearinDown = 8;

	public float distMin = 2;

	int numberUp;
	int numberDown;
	int numberLeft;
	int numberRight;

	int numberOfMagenta;
	int numberOfCyan;
	int numberOfYellow;

	public GameObject luciole;
	GameObject[] lucioles;
	public float compteur;

	public bool lastFireworks;
	public int[] paliers;
	public int[] fireflyAdded;


	int whichColor;

	public void addFirefly ()
	{
		compteur++;

		for (int i = 0; i < paliers.Length; i++)
		{
			if (compteur == paliers[i])
			{
				Debug.Log (i + "    " + (paliers.Length-2));
				numberOfLuciolForEachColor += fireflyAdded[i];
				if (i == paliers.Length - 2)
				{
					lastFireworks = true;
					changeColor ();
				}
				else if(i == paliers.Length - 1)
				{
					GameManager.fadeToDo = GameManager.fadeState.FadeOut;
				}
			}
		}
	}


	public void removeOne(Color colorSent)
	{
		if(colorSent == Color.magenta)
		{
			numberOfMagenta--;
		}
		else if (colorSent == Color.yellow)
		{
			numberOfYellow--;
		}
		else if (colorSent == Color.cyan)
		{
			numberOfCyan--;
		}
	}

	// Use this for initialization
	void Start ()
	{
		lastFireworks = false;
		compteur = 0;

		numberDown = chanceOfAppearinDown;
		numberLeft = chanceOfAppearingLeft;
		numberRight = chanceOfAppearinRight;
		numberUp = chanceOfAppearinUp;

		numberOfMagenta = 0;
		numberOfYellow = 0;
		numberOfCyan = 0;

		whichColor = Random.Range(0, 3);
		changeColor ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(numberOfCyan != numberOfLuciolForEachColor)
		{
			throwLuciol (Color.cyan);
		}
		else if ( numberOfMagenta != numberOfLuciolForEachColor)
		{
			throwLuciol (Color.magenta);
		}
		else if (numberOfYellow != numberOfLuciolForEachColor)
		{
			throwLuciol (Color.yellow);
		}

		if (numberUp == 0 && numberRight == 0 && numberLeft == 0 && numberDown == 0)
		{
			numberDown = chanceOfAppearinDown;
			numberLeft = chanceOfAppearingLeft;
			numberRight = chanceOfAppearinRight;
			numberUp = chanceOfAppearinUp;
		}

		timerBeforeChange -= Time.deltaTime;

		if(timerBeforeChange <= 0)
		{
			changeColor ();
		}
	}

	void changeColor ()
	{
		
		int tempChange = Random.Range (0, 2);
		switch(tempChange)
		{
			case 0:
				whichColor--;
				break;
			case 1:
				whichColor++;
				break;
		}

		if(lastFireworks)
		{
			whichColor = 3;
		}
		else
		{
			AkSoundEngine.PostEvent ("Lampadaire_On", gameObject);
			whichColor = whichColor % 3;
			if (whichColor == -1)
			{
				whichColor = 2;
			}
		}


		switch (whichColor)
		{
			case 0:
				this.GetComponentInChildren<Light> ().color = Color.cyan;
				break;
			case 1:
				this.GetComponentInChildren<Light> ().color = Color.yellow;
				break;
			case 2:
				this.GetComponentInChildren<Light> ().color = Color.magenta;
				break;
			case 3:
				this.GetComponentInChildren<Light> ().color = Color.white;
				break;
		}



		timerBeforeChange = Random.Range (minTimeBeforeChange, maxTimeBeforeChange + 1);
	}

	public void throwLuciol (Color color)
	{
		bool canPop = true;

		lucioles = GameObject.FindGameObjectsWithTag ("luciole");
		Vector3 pos;

		
			GameObject tempLuciol;
			int tempSideOfScreen = Random.Range (1, numberDown + numberLeft + numberRight + numberUp + 1);
			if (tempSideOfScreen <= numberDown)
			{
				float tempPos = Random.Range (-9, 9);
				pos = new Vector3 (tempPos, 0, -7);
			}
			else if (tempSideOfScreen <= numberDown + numberLeft)
			{
				float tempPos = Random.Range (-5, 5);
				pos = new Vector3 (-13, 0, tempPos);
			}
			else if (tempSideOfScreen <= numberDown + numberLeft + numberRight)
			{
				float tempPos = Random.Range (-5, 5);
				pos = new Vector3 (12, 0, tempPos);
			}
			else
			{
				float tempPos = Random.Range (-9, 9);
				pos = new Vector3 (tempPos, 0, 8);
			}

		foreach(GameObject luciole in lucioles)
		{
			if(Vector3.Distance(pos, luciole.transform.position) < distMin)
			{
				canPop = false;
			}
		}

		if (canPop)
		{
			if (tempSideOfScreen <= numberDown)
			{
				numberDown--;
			}
			else if (tempSideOfScreen <= numberDown + numberLeft)
			{
				numberLeft--;
			}
			else if (tempSideOfScreen <= numberDown + numberLeft + numberRight)
			{
				numberRight--;
			}
			else
			{
				numberUp--;
			}

			tempLuciol = Instantiate (luciole, pos, Quaternion.identity);
			tempLuciol.GetComponentInChildren<Light> ().color = color;

			if (color == Color.cyan)
			{
				numberOfCyan++;
			}
			else if (color == Color.yellow)
			{
				numberOfYellow++;
			}
			else if (color == Color.magenta)
			{
				numberOfMagenta++;
			}

			tempLuciol.transform.LookAt (new Vector3 (Random.Range (destXleft, destXright), 0, Random.Range (destYleft, destYright)));
			tempLuciol.GetComponent<fireflyBehavior> ().myColor = color;
			tempLuciol.GetComponent<fireflyBehavior> ().lampe = this.gameObject;
		}
	}
}
