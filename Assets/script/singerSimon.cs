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

		isWaiting = false;
		indexDaffichage = -1;

		indexChoosen = 0;

		whichChainIllDO = 1;
		chainOf2 = new int[2];
		chainOf3 = new int[3];
		chainOf4 = new int[4];

		badTime = gonnaHaveABadTime;
		state = State.badThings;
	}

	void badThingsHappen()
	{
		badTime -= Time.deltaTime;

		if(badTime<0)
		{
			state = State.none;
			chooseAchain();
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
				AkSoundEngine.PostEvent ("Tournelune_switch",gameObject);
				
				isWaiting = true;
				timeToWait = timer;
			}
		}
		else if (whichChainIllDO <= numberOfOne + numberOfTwo)
		{
			if (isWaiting)
			{
				Debug.Log(timeToWait);
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
		state = State.canShow;
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
		if(state == State.choose || state == State.show)
		{
			if (whichChainIllDO <= numberOfOne)
			{
				if(whichOne == chainOf1)
				{
					doCroa.good();
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
						doCroa.good ();
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
					stopMusician();
					indexChoosen = 0;
					state = State.none;
					state = State.badThings;
					badTime = gonnaHaveABadTime;
					doCroa.bad();
				}
			}
			else if (whichChainIllDO <= numberOfOne + numberOfTwo + numberOfthree)
			{
				if (whichOne == chainOf3[indexChoosen])
				{
					
					musicians[chainOf3[indexChoosen] - 1].play ();
					if (indexChoosen == 2)
					{
						doCroa.good ();
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
					stopMusician();
					indexChoosen = 0;
					state = State.none;
					state = State.badThings;
					badTime = gonnaHaveABadTime;
					doCroa.bad();
				}
			}
			else
			{
				if (whichOne == chainOf4[indexChoosen])
				{
					
					musicians[chainOf4[indexChoosen] - 1].play ();
					if (indexChoosen == 3)
					{
						doCroa.good ();
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
					stopMusician();
					indexChoosen = 0;
					state = State.badThings;
					badTime = gonnaHaveABadTime;
					doCroa.bad();
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

		else if(state == State.listen)
		{
			timeBeforeShuttingMusic -= Time.deltaTime;
			if (timeBeforeShuttingMusic <= 0)
			{
				stopMusician();
				whichChainIllDO++;
				state = State.none;
				chooseAchain ();
			}
		}
		else if(state == State.badThings)
		{
			badThingsHappen();
		}
	}
}
