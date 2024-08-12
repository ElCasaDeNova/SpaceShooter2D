using UnityEngine;

public class EnemyShoot : MonoBehaviour
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

    private Camera mainCamera;
    private float timeSinceLastFire;
    private Renderer enemyRenderer;

    void Start()
    {
        // Get the Renderer component from the enemy
        enemyRenderer = enemy.GetComponent<Renderer>();

        // Find the main camera in the scene
        mainCamera = Camera.main;
    }

    // Update is called once per frame
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
        // Calculate the direction from the enemy to the player
        Vector2 baseDirection = (playerOne.position - transform.position).normalized;

        // Convert the base direction to an angle
        float baseAngle = Mathf.Atan2(baseDirection.y, baseDirection.x) * Mathf.Rad2Deg;

        // Calculate the angle between each bullet in the spread
        float angleStep = spreadAngle / (numberOfBullets - 1);

        // Calculate the starting angle for the first bullet (leftmost in the spread)
        float startAngle = baseAngle - (spreadAngle / 2);

        for (int i = 0; i < numberOfBullets; i++)
        {
            // Calculate the angle for this bullet
            float currentAngle = startAngle + (i * angleStep);

            // Convert the angle to a direction vector
            Vector2 bulletDirection = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad)).normalized;

            // Create the bullet
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawner.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            bullet.transform.SetParent(parentRoot);

            if (rb != null)
            {
                // Set the velocity of the bullet based on the direction and speed
                rb.velocity = bulletDirection * bulletSpeed;

                // Set the rotation of the bullet to point towards the player
                float bulletAngle = Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg - 90f; // Adjust for sprite orientation
                bullet.transform.rotation = Quaternion.Euler(0, 0, bulletAngle);
            }
        }
    }
    private bool IsVisibleToCamera(Camera camera)
    {
        // Calculer les plans du frustum de la caméra
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        // Tester si les limites du Renderer sont à l'intérieur des plans du frustum
        return GeometryUtility.TestPlanesAABB(planes, enemyRenderer.bounds);
    }
}
