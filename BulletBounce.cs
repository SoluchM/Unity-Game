using UnityEngine;

public class BulletBounce : MonoBehaviour
{
    private bool hasBounced = false;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Buildings") && !hasBounced)
        {
            Vector2 reflectDirection = Vector2.Reflect(rb.velocity, Vector2.left);
            rb.velocity = reflectDirection;
            hasBounced = true;

            
            ShootingScript shootingScript = FindObjectOfType<ShootingScript>();
            if (shootingScript != null)
            {
                shootingScript.BulletBounced();
            }
        }
    }
}
