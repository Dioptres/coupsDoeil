using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singerSimon : MonoBehaviour {

	private enum State : byte
	{
		show,
		choose,
		listen,
		badThings,
		canShow,
		none
	}

	public doCroaCroa doCroa;

	private State state;

	int indexCibleMusi;
	bool shouldChoose = true;
	public float timerLeft = 10;
	float timerOfChoice;
	public int numberOfAct = 10;
	int numOfActPast = 0;
	public float removeToIncreaseDiff = 0.2f;

	public int numberOfOne = 1;
	public int numberOfTwo = 1;
	public int numberOfthree = 1;
	public int numberOffour = 1;

	Animator anim;

	float badTime;
	public float gonnaHaveABadTime = 2;

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

		doCroa.neutral ();

		anim = GetComponentInChildren<Animator> ();

		
	}

	void chooseMusi()
	{
		doCroa.neutral ();
		indexCibleMusi = Random.Range (1, 4);
		Debug.Log (indexCibleMusi + "   indexCibleMusi");
		setCible (indexCibleMusi); 
	}

	void setCible(int index)
	{
		Debug.Log ("index" + index);
		switch(index)
		{
			case 0:
				CROWDmANAGER.giveCible (GameObject.Find ("Bassiste").transform);
				break;
			case 1:
				CROWDmANAGER.giveCible (GameObject.Find ("Flûtiste").transform);
				break;
			case 2:
				CROWDmANAGER.giveCible (GameObject.Find ("Guitariste").transform);
				break;
			case 3:
				CROWDmANAGER.giveCible (GameObject.Find ("Ukulele").transform);
				break;
		}
	}


	void stopMusician()
	{
		if (whichChainIllDO <= numberOfOne)
		{
			musicians[chainOf1 - 1].stop();
		}
		else if (whichChainIllDO <= numberOfOne + numberOfTwo)
		{
			for (int i = 0; i < chainOf2.Length; i++)
			{
				musicians[chainOf2[i] - 1].stop();
			}
		}
		else if (whichChainIllDO <= numberOfOne + numberOfTwo + numberOfthree)
		{
			for (int i = 0; i < chainOf3.Length; i++)
			{
				musicians[chainOf3[i] - 1].stop();
			}
		}
		else
		{
			if (whichChainIllDO == numberOfOne + numberOfTwo + numberOfthree + numberOffour)
			{
				GameManager.fadeToDo = GameManager.fadeState.FadeOut;
			}
			for (int i = 0; i < chainOf4.Length; i++)
			{
				musicians[chainOf4[i] - 1].stop();
			}
		}
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
					anim.SetInteger ("moonColor", 0);
					AkSoundEngine.PostEvent ("Tournelune_switch",gameObject);
				}
			}
			else
			{
				anim.SetInteger ("moonColor", chainOf1);
				setCible (chainOf1);
				AkSoundEngine.PostEvent ("Tournelune_switch",gameObject);
				
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
					Debug.Log("index ++");
					indexDaffichage++;
					isWaiting = false;
				}
				else if (timeToWait <= 0)
				{
					//go to choose musicos
					indexDaffichage = -1;
					state = State.choose;
					anim.SetInteger ("moonColor", 0);
					AkSoundEngine.PostEvent ("Tournelune_switch",gameObject);
					isWaiting = false;
					Debug.Log ("reset d'affichage   " + indexDaffichage);
				}
			}
			else
			{
				if(indexDaffichage == -1)
				{
					doCroa.neutral();
				}
				else
				{
					anim.SetInteger("moonColor", chainOf2[indexDaffichage]);
					setCible (chainOf2[indexDaffichage]);
					Debug.Log("SWITCH");
					AkSoundEngine.PostEvent ("Tournelune_switch",gameObject);
				}
				
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
					indexDaffichage = -1;
					isWaiting = false;
					state = State.choose;
					anim.SetInteger ("moonColor", 0);
					AkSoundEngine.PostEvent ("Tournelune_switch",gameObject);
				}
			}
			else
			{
				if (indexDaffichage == -1)
				{
					doCroa.neutral();
				}
				else
				{
					anim.SetInteger("moonColor", chainOf3[indexDaffichage]);
					setCible (chainOf3[indexDaffichage]);
					AkSoundEngine.PostEvent ("Tournelune_switch",gameObject);
				}
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
					indexDaffichage = -1;
					isWaiting = false;
					state = State.choose;
					anim.SetInteger ("moonColor", 0);
					AkSoundEngine.PostEvent ("Tournelune_switch",gameObject);
				}
			}
			else
			{
				if (indexDaffichage == -1)
				{
					doCroa.neutral();
				}
				else
				{
					anim.SetInteger("moonColor", chainOf4[indexDaffichage]);
					setCible (chainOf4[indexDaffichage]);
					AkSoundEngine.PostEvent ("Tournelune_switch",gameObject);
				}
				isWaiting = true;
				timeToWait = timer;
			}
		}
	}

	void chooseAchain()
	{
		if (whichChainIllDO <= numberOfOne)
		{
			Debug.Log ("chain 1");
			chainOf1 = Random.Range (1, 5);
		}
		else if (whichChainIllDO <= numberOfOne + numberOfTwo)
		{
			Debug.Log ("chain 2");
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
			Debug.Log ("chain 3");
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
			Debug.Log ("chain 4");
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

	public void willYouShow()
	{
		if(state == State.canShow)
		{
			state = State.show;
		}
	}

	public void choose (int whichOne)
	{
		Debug.Log (whichOne);
		if(!shouldChoose)
		{
			if(whichOne == indexCibleMusi)
			{
				Debug.Log ("success  "  + indexCibleMusi);
				doCroa.good();
				musicians[indexCibleMusi].play();
				numOfActPast++;
				if(numOfActPast == numberOfAct)
				{
					GameManager.fadeToDo = GameManager.fadeState.FadeOut;
				}
				timerLeft -= removeToIncreaseDiff;
				shouldChoose = true;
			}
			
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if(shouldChoose)
		{
			chooseMusi ();
			shouldChoose = false;
			timerOfChoice = timerLeft;
			Debug.Log ("temps to do   " + timerOfChoice);
		}
		else
		{
			timerOfChoice -= Time.deltaTime;
			if(timerOfChoice <= 0)
			{
				Debug.Log ("fail");
				shouldChoose = true;
				doCroa.bad ();
				musicians[indexCibleMusi].stop ();
			}
		}
	}
}
