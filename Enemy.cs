using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;
    private bool shouldMove = true; 

    private void Update()
    {
        if (shouldMove)
        {
            if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 0.1f)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex == waypoints.Length)
                {
                    currentWaypointIndex = 0;
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
        }
    }

    public void PauseMovement()
    {
        shouldMove = false;
    }

    public void ResumeMovement()
    {
        shouldMove = true;
    }
}