using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singerSimon : MonoBehaviour {

	private enum State : byte
	{
		show,
		choose,
		listen,
		none
	}

	private State state = State.none;

	public int numberOfOne = 1;
	public int numberOfTwo = 1;
	public int numberOfthree = 1;
	public int numberOffour = 1;

	int chainOf1;
	int[] chainOf2;
	int[] chainOf3;
	int[] chainOf4;

	int indexChoosen;

	bool isWaiting;
	int indexDaffichage;
	public float timer = 4;
	float timeToWait;
	public float timerListeningMusic = 5;
	float timeBeforeShuttingMusic;

	public musicianSimon[] musicians;

	public SpriteRenderer tourneLune;

	int whichChainIllDO;

	// Use this for initialization
	void Start ()
	{
		isWaiting = false;
		indexDaffichage = 0;

		indexChoosen = 0;

		whichChainIllDO = 1;
		chainOf2 = new int[2];
		chainOf3 = new int[3];
		chainOf4 = new int[4];

		chooseAchain ();
	}
	
	Color gimmeColor(int colorIndex)
	{
		Color futurColor = Color.white;
		switch (colorIndex)
		{
			case 1:
				futurColor = Color.red;
				break;
			case 2:
				futurColor = Color.yellow;
				break;
			case 3:
				futurColor = new Color(0.58f, 0.44f, 0.86f);
				break;
			case 4:
				futurColor = Color.blue;
				break;
		}
		//tourneLune.material.SetColor ("_Color", Color.red);
		return futurColor;
	}

	void showAchain ()
	{
		if (whichChainIllDO <= numberOfOne)
		{
			if(isWaiting)
			{
				timeToWait -= Time.deltaTime;
				if(timeToWait <= 0)
				{
					//go to choose musicos
					isWaiting = false;
					state = State.choose;
				}
			}
			else
			{
				tourneLune.material.SetColor ("_Color", gimmeColor (chainOf1));
				isWaiting = true;
				timeToWait = timer;
			}
		}
		else if (whichChainIllDO <= numberOfOne + numberOfTwo)
		{
			if (isWaiting)
			{
				
				timeToWait -= Time.deltaTime;
				if (timeToWait <= 0 && indexDaffichage < 1)
				{
					indexDaffichage++;
					isWaiting = false;
				}
				else if (timeToWait <= 0)
				{
					//go to choose musicos
					indexDaffichage = 0;
					state = State.choose;
				}
			}
			else
			{
				tourneLune.material.SetColor ("_Color", gimmeColor (chainOf2[indexDaffichage]));
				isWaiting = true;
				timeToWait = timer;
			}
		}
		else if (whichChainIllDO <= numberOfOne + numberOfTwo + numberOfthree)
		{
			if (isWaiting)
			{

				timeToWait -= Time.deltaTime;
				if (timeToWait <= 0 && indexDaffichage < 2)
				{
					indexDaffichage++;
					isWaiting = false;
				}
				else if (timeToWait <= 0)
				{
					//go to choose musicos
					indexDaffichage = 0;
					state = State.choose;
				}
			}
			else
			{
				tourneLune.material.SetColor ("_Color", gimmeColor (chainOf3[indexDaffichage]));
				isWaiting = true;
				timeToWait = timer;
			}
		}
		else
		{
			if (isWaiting)
			{

				timeToWait -= Time.deltaTime;
				if (timeToWait <= 0 && indexDaffichage < 3)
				{
					indexDaffichage++;
					isWaiting = false;
				}
				else if (timeToWait <= 0)
				{
					//go to choose musicos
					indexDaffichage = 0;
					state = State.choose;
				}
			}
			else
			{
				tourneLune.material.SetColor ("_Color", gimmeColor (chainOf4[indexDaffichage]));
				isWaiting = true;
				timeToWait = timer;
			}
		}
	}

	void chooseAchain()
	{
		if (whichChainIllDO <= numberOfOne)
		{
			chainOf1 = Random.Range (1, 5);
		}
		else if (whichChainIllDO <= numberOfOne + numberOfTwo)
		{
			for (int i = 0; i < chainOf2.Length; i++)
			{
				chainOf2[i] = Random.Range (1, 5);
				for (int j = 0; j < chainOf2.Length; j++)
				{
					if (j < i)
					{
						if (chainOf2[i] == chainOf2[j])
						{
							i--;
						}
					}
				}
			}
		}
		else if (whichChainIllDO <= numberOfOne + numberOfTwo + numberOfthree)
		{
			for (int i = 0; i < chainOf3.Length; i++)
			{
				chainOf3[i] = Random.Range (1, 5);
				for (int j = 0; j < chainOf3.Length; j++)
				{
					if (j < i)
					{
						if (chainOf3[i] == chainOf3[j])
						{
							i--;
						}
					}
				}
			}
		}
		else
		{
			for (int i = 0; i < chainOf4.Length; i++)
			{
				chainOf4[i] = Random.Range (1, 5);
				for (int j = 0; j < chainOf4.Length; j++)
				{
					if (j < i)
					{
						if (chainOf4[i] == chainOf4[j])
						{
							i--;
						}
					}
				}
			}
		}
		state = State.show;
	}

	public void choose (int whichOne)
	{
		if(state == State.choose)
		{
			if (whichChainIllDO <= numberOfOne)
			{
				if(whichOne == chainOf1)
				{
					musicians[chainOf1 - 1].play();
					timeBeforeShuttingMusic = timerListeningMusic;
					state = State.listen;
				}
			}
			else if (whichChainIllDO <= numberOfOne + numberOfTwo)
			{
				if (whichOne == chainOf2[indexChoosen])
				{
					musicians[chainOf2[indexChoosen] - 1].play ();
					if (indexChoosen == 1)
					{
						indexChoosen = 0;
						timeBeforeShuttingMusic = timerListeningMusic;
						state = State.listen;
					}
					else
					{
						indexChoosen++;
					}
				}
				else
				{
					// fail
				}
			}
			else if (whichChainIllDO <= numberOfOne + numberOfTwo + numberOfthree)
			{
				if (whichOne == chainOf3[indexChoosen])
				{
					musicians[chainOf3[indexChoosen] - 1].play ();
					if (indexChoosen == 2)
					{
						// sucess then go to next
						timeBeforeShuttingMusic = timerListeningMusic;
						state = State.listen;
					}
					else
					{
						indexChoosen++;
					}
				}
				else
				{
					// fail
				}
			}
			else
			{
				if (whichOne == chainOf4[indexChoosen])
				{
					musicians[chainOf4[indexChoosen] - 1].play ();
					if (indexChoosen == 3)
					{
						// sucess then go to next
						timeBeforeShuttingMusic = timerListeningMusic;
						state = State.listen;
					}
					else
					{
						indexChoosen++;
					}
				}
				else
				{
					// fail
				}
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if(state == State.show)
		{
			showAchain ();
		}

		if(state == State.listen)
		{
			timeBeforeShuttingMusic -= Time.deltaTime;
			if (timeBeforeShuttingMusic <= 0)
			{
				if (whichChainIllDO <= numberOfOne)
				{
					musicians[chainOf1 - 1].stop ();
				}
				else if (whichChainIllDO <= numberOfOne + numberOfTwo)
				{
					for(int i = 0; i<chainOf2.Length; i++)
					{
						musicians[chainOf2[i] - 1].stop ();
					}
				}
				else if (whichChainIllDO <= numberOfOne + numberOfTwo + numberOfthree)
				{
					for (int i = 0; i < chainOf3.Length; i++)
					{
						musicians[chainOf3[i] - 1].stop ();
					}
				}
				else
				{
					for (int i = 0; i < chainOf4.Length; i++)
					{
						musicians[chainOf4[i] - 1].stop ();
					}
				}
				whichChainIllDO++;
				state = State.none;
				chooseAchain ();
			}
		}
	}
}
