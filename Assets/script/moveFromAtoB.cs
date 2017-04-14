using UnityEngine;
using System.Collections;
using Tobii.EyeTracking;

public class moveFromAtoB : Lookable
{

    Vector3 goThere;
    GazePoint gazePoint;
    UnityEngine.AI.NavMeshAgent agent;

    public override void QuitSee()
    {
        StartCoroutine(moveMeThere());
    }

    IEnumerator moveMeThere()
    {
        yield return new WaitForSeconds(2);
        
        if (gazePoint.IsValid)
        {
            goThere = Camera.main.ScreenToWorldPoint(new Vector3(gazePoint.Screen.x, gazePoint.Screen.y));
            agent.destination = goThere;
        }
        
    }

    public override void Start()
    {
        base.Start();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public override void Update()
    {
        base.Update();
        gazePoint = EyeTracking.GetGazePoint();
        
    }
}
