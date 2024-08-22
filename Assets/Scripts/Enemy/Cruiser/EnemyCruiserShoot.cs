using UnityEngine;

public class EnemyCruiserShoot : MonoBehaviour
{
    [SerializeField]
    private Transform enemy;

    [SerializeField]
    private Transform playerOne;

    [SerializeField]
    private GameObject bulletPrefab; // The prefab of the bullet to instantiate

    [SerializeField]
    private float bulletSpeed = 10f; // The speed of the bullet

    [SerializeField]
    private float fireInterval = 0.5f; // Time in seconds between each shot

    [SerializeField]
    private Transform bulletSpawner;

    [SerializeField]
    private int numberOfBullets = 5; // The number of bullets in the shotgun spread

    [SerializeField]
    private float spreadAngle = 10f; // The spread angle in degrees

    [SerializeField]
    private Transform parentRoot;

    [SerializeField]
    private AudioClip shootSound;

    [SerializeField]
    private AudioSource audioSource;

    private Camera mainCamera;
    private float timeSinceLastFire;
    private Renderer enemyRenderer;

    void Start()
    {
        enemyRenderer = enemy.GetComponent<Renderer>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        timeSinceLastFire += Time.deltaTime;

        if (IsVisibleToCamera(mainCamera) && timeSinceLastFire >= fireInterval)
        {
            timeSinceLastFire = 0f;
            ShootBullets();
        }
    }

    void ShootBullets()
    {
        Vector2 baseDirection = (playerOne.position - transform.position).normalized;
        float baseAngle = Mathf.Atan2(baseDirection.y, baseDirection.x) * Mathf.Rad2Deg;
        float angleStep = spreadAngle / (numberOfBullets - 1);
        float startAngle = baseAngle - (spreadAngle / 2);

        for (int i = 0; i < numberOfBullets; i++)
        {
            float currentAngle = startAngle + (i * angleStep);
            Vector2 bulletDirection = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad)).normalized;

            GameObject bullet = BulletPooler.Instance.GetBullet();
            bullet.transform.position = bulletSpawner.position;
            bullet.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg - 90f);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            bullet.transform.SetParent(parentRoot, true); // Maintain world position

            if (rb != null)
            {
                rb.velocity = bulletDirection * bulletSpeed;
            }
        }

        // Play the shooting sound
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    private bool IsVisibleToCamera(Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, enemyRenderer.bounds);
    }
}
