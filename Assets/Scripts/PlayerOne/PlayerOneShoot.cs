using UnityEngine;

public class PlayerOneShoot : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 10f; // The speed of the bullet

    [SerializeField]
    private float fireInterval = 0.5f; // Time in seconds between each shot

    [SerializeField]
    private Transform bulletSpawner;

    [SerializeField]
    private AudioSource audioSource;
    private AudioClip shootSound;

    private float timeSinceLastFire;

    void Start()
    {
        // Get the AudioSource component on this GameObject
        audioSource = GetComponent<AudioSource>();
        shootSound = audioSource.clip;
    }

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
        GameObject bullet = BulletPooler.Instance.GetBullet();
        bullet.transform.position = bulletSpawner.position;
        bullet.transform.rotation = Quaternion.identity;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            return;
        }

        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // Adjust for sprite orientation
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Play the shooting sound
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}
