using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantManager : MonoBehaviour {

    Animator animPlant1;
    Animator animPlant2;
    Animator animPlant3;

    public float timeBeforeActi = 1f;
    public float timeOfLook;

    int actualLamp;

    float timer;

    // Use this for initialization
    void Start()
    {
        timer = 0;
        animPlant1 = transform.GetChild(0).GetComponent<Animator>();
        animPlant2 = transform.GetChild(1).GetComponent<Animator>();
        animPlant3 = transform.GetChild(2).GetComponent<Animator>();

        animPlant1.SetInteger("moonColor", 0);
        animPlant2.SetInteger("moonColor", 0);
        animPlant3.SetInteger("moonColor", 0);

        chooseAlamp();
    }

    // Update is called once per frame
    public void chooseAlamp () {
        int temp;
        do
        {
            temp = Random.Range(0, 3);
        }
        while (temp == actualLamp);

        actualLamp = temp;
        Debug.Log(temp + "       lamp ACTIVATED");
        switch(actualLamp)
        {
            case 0:
                animPlant1.SetInteger("moonColor", 3);
                break;
            case 1:
                animPlant2.SetInteger("moonColor", 2);
                break;
            case 2:
                animPlant3.SetInteger("moonColor", 1);
                break;
        }
        transform.GetChild(actualLamp).GetComponent<plantLooked>().active = true;


    }
}
