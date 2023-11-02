using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class playerDetect : MonoBehaviour
{

    private bool playerInside = false;
    public Enemy enemy;
    private Coroutine resumeCoroutine;
    private SpriteRenderer sprite;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playerInside = true;
            enemy.PauseMovement(); 
            if (resumeCoroutine != null)
            {
                StopCoroutine(resumeCoroutine);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playerInside = false;
            resumeCoroutine = StartCoroutine(ResumeEnemyMovementAfterDelay(5f)); 
        }
    }

    private IEnumerator ResumeEnemyMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        enemy.ResumeMovement(); 
    }

    public bool IsPlayerInside()
    {
        return playerInside;
    }

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (enemy.currentWaypointIndex == 1)
        {
            Vector3 newPosition = enemy.transform.position + new Vector3(2f, 0f, 0f);
            sprite.transform.position = newPosition;
        }
        else if (enemy.currentWaypointIndex < 1)
        {
            Vector3 newPosition = enemy.transform.position - new Vector3(2f, 0f, 0f);
            sprite.transform.position = newPosition;
        }
    }

}
