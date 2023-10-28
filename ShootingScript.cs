using UnityEngine;
using UnityEngine.U2D;
using System.Collections;

public class ShootingScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    private SpriteRenderer sprite;
    private int bulletSpeed = 20;

    private GameObject playerObj;
    private GameObject currentBullet;
    private Vector3 initialPosition;
    private bool isBulletReturning = false;
    private bool isBulletFired = false;
    private Vector2 shootDirection;

    private PlayerMovement playerMovement;

    void Start()
    {
        playerObj = GameObject.Find("Player");
        sprite = GetComponent <SpriteRenderer>();
        playerMovement = playerObj.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!isBulletFired && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (currentBullet != null)
        {
            if (isBulletReturning)
            {
                Vector2 returnDirection = (playerObj.transform.position - currentBullet.transform.position).normalized;
                currentBullet.GetComponent<Rigidbody2D>().velocity = returnDirection * bulletSpeed;

                float distanceToInitial = Vector3.Distance(initialPosition, currentBullet.transform.position);
                if (distanceToInitial < 0.1f || Vector3.Distance(playerObj.transform.position, currentBullet.transform.position) < 0.1f)
                {
                    Destroy(currentBullet);
                    isBulletReturning = false;
                    isBulletFired = false;
                }
            }
            else
            {
                float distance = Vector3.Distance(initialPosition, currentBullet.transform.position);
                if (distance >= 30f)
                {
                    isBulletReturning = true;
                }
            }
        }
    }

    void Shoot()
    {
        currentBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRigidbody = currentBullet.GetComponent<Rigidbody2D>();
        initialPosition = transform.position;

        shootDirection = playerMovement.GetFacingDirection() == PlayerMovement.FacingDirection.Left ? Vector2.left : Vector2.right;

        bulletRigidbody.velocity = shootDirection * bulletSpeed;

        isBulletFired = true;
    }
}
