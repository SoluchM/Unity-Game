using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float maxDistance = 10f;
    public float cooldownTime = 1f;
    private float lastShotTime;

    private Vector3 initialPlayerPosition;
    private GameObject currentBullet;

    void Start()
    {
        initialPlayerPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time - lastShotTime >= cooldownTime)
        {
            lastShotTime = Time.time;
            Shoot();
        }

        if (currentBullet != null)
        {
            ReturnBullet();
        }
    }

    void Shoot()
    {
        Vector3 shootingDirection = transform.right; // Strzelanie w kierunku, w którym zwrócony jest gracz.

        currentBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRigidbody = currentBullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = shootingDirection * bulletSpeed;
    }

    void ReturnBullet()
    {
        if (currentBullet == null)
        {
            return;
        }

        // Sprawdzanie, czy pocisk przekroczy³ maksymalny dystans.
        if (Vector2.Distance(currentBullet.transform.position, initialPlayerPosition) >= maxDistance)
        {
            Destroy(currentBullet); // Zniszcz pocisk, gdy przekroczy maksymalny dystans.
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Buildings"))
        {
            Destroy(currentBullet); // Zniszcz pocisk po zderzeniu z budynkiem.
        }
    }
}
