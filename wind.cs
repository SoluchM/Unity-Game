using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                playerRigidbody.AddForce(Vector2.up * 20000 * Time.deltaTime);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            
        }
    }
}
