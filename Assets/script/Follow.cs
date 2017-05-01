using System.Collections;
using UnityEngine;

public class Follow : MonoBehaviour
{

    public Transform targetTransform;
    public float speed = 0;
    public float distanceToStop = 1.5f;

    void Update()
    {
        Vector3 displacementFromTarget = targetTransform.position - transform.position;
        Vector3 directionToTarget = displacementFromTarget.normalized;
        Vector3 velocity = directionToTarget * speed;
        velocity.y = 0;

        float distanceToTarget = displacementFromTarget.magnitude;

        if (distanceToTarget > distanceToStop)
        {
            transform.Translate(velocity * Time.deltaTime);
        }
    }
}