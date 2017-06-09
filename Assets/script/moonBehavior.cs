using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moonBehavior : Lookable {

	public singerSimon singer;

	public override void DoAction ()
	{
		base.DoAction ();

		singer.willYouShow ();
	}

}
