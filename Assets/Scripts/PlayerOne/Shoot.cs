using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab; // The prefab of the bullet to instantiate
    public float bulletSpeed = 10f; // The speed of the bullet
    public float fireInterval = 0.5f; // Time in seconds between each shot

    [SerializeField]
    private Transform bulletSpawner;

    private float timeSinceLastFire;

    [SerializeField]
    private Transform parentRoot;

    void Update()
    {
        timeSinceLastFire += Time.deltaTime;

        if (Input.GetButton("Fire1") && timeSinceLastFire >= fireInterval)
        {
            timeSinceLastFire = 0f;
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawner.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        bullet.transform.SetParent(parentRoot);

        if (rb == null)
        {
            return;
        }

        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        rb.velocity = transform.up * bulletSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // Adjust for sprite orientation
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
