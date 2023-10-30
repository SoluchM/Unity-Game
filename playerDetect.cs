using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDetect : MonoBehaviour
{
    private bool playerInside = false;
    public Enemy enemy; 

    private Coroutine resumeCoroutine;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("detect");
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
            Debug.Log("lost");
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
}
