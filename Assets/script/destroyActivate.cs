using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyActivate : MonoBehaviour
{

	public GameObject toActivate;

	private void OnDestroy ()
	{
		if (toActivate)
			toActivate.tag = "place";
	}
}
