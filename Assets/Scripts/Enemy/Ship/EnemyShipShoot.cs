using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab; // The prefab of the bullet to instantiate

    [SerializeField]
    private float bulletSpeed = 10f; // The speed of the bullet

    [SerializeField]
    private float fireInterval = 0.5f; // Time in seconds between each shot

    [SerializeField]
    private Transform bulletSpawner;

    private float timeSinceLastFire;

    [SerializeField]
    private Transform parentRoot;

    void Update()
    {
        timeSinceLastFire += Time.deltaTime;

        if (timeSinceLastFire >= fireInterval)
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

        // Change transform.up to -transform.up to shoot in the correct direction
        rb.velocity = -transform.up * bulletSpeed;

        // Calculate the rotation based on the shooting direction
        float angle = Mathf.Atan2(-transform.up.y, -transform.up.x) * Mathf.Rad2Deg - 90f; // Adjust for sprite orientation
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
