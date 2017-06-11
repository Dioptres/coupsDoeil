using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doCroaCroa : MonoBehaviour {
	public Animator anim;

	public void neutral()
	{
		anim.SetTrigger("isCroakingNeutral");
		AkSoundEngine.PostEvent("Chanteur_neutre", gameObject); // to change to neutral
	}
	public void good()
	{
		anim.SetTrigger("isCroakingGood");
		AkSoundEngine.PostEvent("Chanteur_positif", gameObject);
	}
	public void bad()
	{
		anim.SetTrigger("isCroakingBad");
		AkSoundEngine.PostEvent("Chanteur_negatif", gameObject);
	}
}
