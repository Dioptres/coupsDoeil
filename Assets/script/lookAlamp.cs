using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAlamp : Lookable
{

	GameObject Manager;

	GameObject lampist;

	float TimeBeforeMusicianGoes = 0.3f;

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
			timerlamp = 0.4f;
			act = false;
		
		
	}

	public override void DoAction ()
	{
		timer += Time.deltaTime;
		if (Manager.GetComponent<GameManager>().TimeSincelastMusicianSeen < 3 && timer > TimeBeforeMusicianGoes)
		{
			if(Manager.GetComponent<GameManager> ().lastMusicianSeen.name == "Singer")
			{
				Manager.GetComponent<GameManager> ().lastMusicianSeen.GetComponent<SingerBehavior> ().MoveThere (this.gameObject);
			}
			else
			{
				Manager.GetComponent<GameManager> ().lastMusicianSeen.GetComponent<MusicianBehavior> ().MoveThere (this.gameObject);
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
