using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer sprite;

    [SerializeField] private GameObject[] waypoints;
    public int currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;
    private bool shouldMove = true;


    public void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

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


        if (currentWaypointIndex == 1)
        {
            sprite.flipX = false;
        }
        else if (currentWaypointIndex < 1)
        {
            sprite.flipX = true;
        }
        //do obrócenia jeszcze fieldofview nastêpnym razem

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