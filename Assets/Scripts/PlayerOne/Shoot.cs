using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab; // The prefab of the bullet to instantiate
    public float bulletSpeed = 10f; // The speed of the bullet
    public float fireInterval = 0.5f; // Time in seconds between each shot

    private float timeSinceLastFire;

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
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            UnityEngine.Debug.LogError("The bullet prefab is missing a Rigidbody2D component.");
            return;
        }

        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // Adjust for sprite orientation
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        UnityEngine.Debug.Log($"Bullet Direction: {direction}, Bullet Velocity: {rb.velocity}, Bullet Rotation: {bullet.transform.rotation.eulerAngles}");
    }
}
