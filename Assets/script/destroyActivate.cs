﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyActivate : MonoBehaviour {

	public GameObject toActivate;

	private void OnDestroy ()
	{
		toActivate.tag = "place";
	}
}
