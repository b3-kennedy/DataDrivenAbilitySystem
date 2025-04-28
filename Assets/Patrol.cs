using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{

    public Transform patrolPointsParent;
    public List<Transform> patrolPoints = new List<Transform>();

    int index = 0;

    public Transform currentWaypoint;

    Rigidbody rb;

    public float patrolSpeed = 10;

    float pauseTimer;
    bool startPauseTimer;

    float pauseDuration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if(patrolPointsParent)
        {
            for (int i = 0; i < patrolPointsParent.childCount; i++)
            {
                patrolPoints.Add(patrolPointsParent.GetChild(i));
            }
            currentWaypoint = patrolPoints[0];
        }
    }

    public void PauseMovement(float duration)
    {
        pauseDuration = duration;
        pauseTimer = 0;
        startPauseTimer = true;
    }

    // Update is called once per frame
    void Update()
    {

        if(startPauseTimer)
        {
            pauseTimer += Time.deltaTime;
            if(pauseTimer >= pauseDuration)
            {
                startPauseTimer = false;
                pauseTimer = 0;
            }
        }

        if(startPauseTimer) return;
        if(patrolPoints.Count == 0) return;
        
        Vector3 dir = (currentWaypoint.position - transform.position).normalized;
        rb.linearVelocity = dir * patrolSpeed;

        if(Vector3.Distance(Action.GetCorrectVector(transform.position), Action.GetCorrectVector(currentWaypoint.position)) <= 1f)
        {
            index++;
            if(index >= patrolPoints.Count)
            {
                index = 0;
            }
            currentWaypoint = patrolPoints[index];
        }

    }
}
