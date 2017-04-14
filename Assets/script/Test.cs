using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : Lookable
{

	public override void DoAction()
	{
		Debug.Log (this.name);
	}
}
