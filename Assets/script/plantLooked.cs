using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantLooked : Lookable
{
    public int crowdType = 1;

    public Animator[] foules;

    public scarabBehavior behaOfAgi;

    public float waitThisBeforeNext = 1;

    bool hasBeenLooked = false;
    public bool active;
    float timer;
    public float timeOpenWhenLooked = 3;

    protected override void UpdateLookable()
    {
        base.UpdateLookable();
        if (hasBeenLooked)
        {
            timer -= Time.deltaTime;

            Debug.Log("timer  " + timer);

            if (timer <= 0)
            {
                behaOfAgi.lampOKEY = false;
                foreach (Animator foule in foules)
                {
                    foule.SetBool("isDancing", false);
                }
                this.GetComponent<Animator>().SetInteger("moonColor", 0);
            }
            if (timer <= -waitThisBeforeNext)
            {
                hasBeenLooked = false;
                this.transform.parent.GetComponent<plantManager>().chooseAlamp();
            }
        }
    }

        // Use this for initialization
        void Start()
    {
        foreach (Animator foule in foules)
        {
            foule.SetInteger("villagerType", crowdType);
        }

    }

    public override void DoAction()
    {
        base.DoAction();
        Debug.Log("DAYUM !!!!!!!!!!!!!!!!!!!!");
        if(active)
        {
            active = false;
            timer = timeOpenWhenLooked;
            hasBeenLooked = true;
            behaOfAgi.lampOKEY = true;
            foreach (Animator foule in foules)
            {
                foule.SetBool("isDancing", true);
				AkSoundEngine.PostEvent ("foule", this.gameObject);
			}
        }
    }
}
