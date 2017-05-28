using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAlamp : Lookable
{

	public bool lastOne = false;

	GameObject Manager;

	GameObject lampist;


	float timerlamp;

	bool act;

	bool activate;

	

	float timer;

	protected override void StartLookable ()
	{
		timer = 0;
		base.StartLookable ();

		Manager = GameObject.FindGameObjectWithTag ("Game_Manager");
		
			activate = false;
			timerlamp = 0.2f;
			act = false;
		
		
	}

	public override void DoAction ()
	{
		timer += Time.deltaTime;
		if (Manager.GetComponent<GameManager>().TimeSincelastMusicianSeen < 3)
		{
			if(Manager.GetComponent<GameManager> ().lastMusicianSeen.name == "Singer" && Vector3.Distance(Manager.GetComponent<GameManager> ().lastMusicianSeen.transform.position, this.transform.position)>2)
			{
				Manager.GetComponent<GameManager> ().lastMusicianSeen.GetComponent<SingerBehavior> ().stopSing ();
				Manager.GetComponent<GameManager> ().lastMusicianSeen.GetComponent<SingerBehavior> ().MoveThere (this.gameObject);
			}
			else
			{
				Manager.GetComponent<GameManager> ().lastMusicianSeen.GetComponent<MusicianBehavior> ().MoveThere (this.gameObject);
				Manager.GetComponent<GameManager> ().lastMusicianSeen.GetComponent<MusicianBehavior> ().lastPlace = lastOne;
				
			}
			
			activate = true;
		}
	}

	public override void QuitSee ()
	{
		base.QuitSee ();
		timer = 0;
	}



}
