﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DancerFieldOfView : Lookable
{
    public float radiusRange;
    public float distanceFollowerToStop;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    private Animator dancerAnimator;

    public override void Start()
    {
        base.Start();
        dancerAnimator = GetComponentInChildren<Animator>();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown("space"))
        {
            DoAction();
        }
    }

    public override void DoAction()
    {
        dancerAnimator.SetTrigger("dancerIsDancing");

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, radiusRange, targetMask);
        Debug.Log(targetsInViewRadius[0]);

        foreach (Collider catched in targetsInViewRadius)
        {
            Transform target = catched.GetComponent<Transform>();
            Animator targetAnimator = catched.GetComponentInChildren<Animator>();
            NavMeshAgent targetAgent = catched.GetComponent<NavMeshAgent>();

            float distanceToDancer = Vector3.Distance(transform.position, target.position);
            
            Debug.Log("Distance" +  distanceToDancer);

            if (distanceToDancer > distanceFollowerToStop)
            targetAgent.destination = transform.position - new Vector3(1, 0, 1);
            Debug.Log(targetAgent.destination);

            targetAnimator.SetBool("CrowdIsDancing", true);
            Debug.Log(targetAnimator);

            Debug.Log(target);
        }
    }
}