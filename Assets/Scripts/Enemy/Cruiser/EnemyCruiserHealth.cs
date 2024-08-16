using UnityEngine;

public class EnemyCruiserHealth : MonoBehaviour
{
    // Variable for health points
    [SerializeField]
    private float maxHealth = 100f;
    private float currentHealth;

    // Variables for spawning ships
    [SerializeField]
    private GameObject shipPrefab; // Prefab for the ship to spawn
    [SerializeField]
    private Transform[] spawnPoints; // Possible spawn points
    [SerializeField]
    private int baseNumberOfShipsToSpawn = 3; // Base number of ships to spawn

    // Static counter for the number of destroyed cruisers
    public static int destroyedCruisers = 0;

    [SerializeField]
    private ScriptSpawnersManagement spawnersManager;

    // Reference to the player
    [SerializeField]
    private Transform playerOne;

    [SerializeField]
    private Transform shipParentRoot;

    [SerializeField]
    private Transform bulletParent;

    // Initialize health points
    void Start()
    {
        currentHealth = maxHealth;

        // Set spawnPoints from ScriptSpawnersManagement
        if (spawnersManager != null)
        {
            spawnPoints = spawnersManager.spawners;
        }
        else
        {
            Debug.LogWarning("ScriptSpawnersManagement reference not set.");
        }
    }

    // Method to take damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    // Method called when health reaches zero
    void Die()
    {
        // Increment the number of destroyed cruisers
        destroyedCruisers++;

        // Spawn ships
        SpawnShips();

        // Destroy the cruiser object
        Destroy(gameObject);
    }

    // Method to spawn ships at random positions
    private void SpawnShips()
    {
        if (shipPrefab == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("Ship prefab or spawn points not set.");
            return;
        }

        // Calculate the number of ships to spawn based on the number of destroyed cruisers
        int numberOfShipsToSpawn = baseNumberOfShipsToSpawn + destroyedCruisers;

        for (int i = 0; i < numberOfShipsToSpawn; i++)
        {
            // Choose a random spawn point
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instantiate the ship at the chosen spawn point
            GameObject shipInstance = Instantiate(shipPrefab, spawnPoint.position, spawnPoint.rotation);

            // Assign the playerOne reference to the EnemyFollow script on the ship
            EnemyFollow enemyFollow = shipInstance.GetComponent<EnemyFollow>();
            if (enemyFollow != null)
            {
                enemyFollow.playerOne = playerOne;
            }

            // Assign the Ship parent to the Ship
            shipInstance.transform.SetParent(shipParentRoot);

            // Assign the parent to the Ship bullets
            EnemyShipShoot enemyShipShoot = shipInstance.GetComponent<EnemyShipShoot>();
            if (enemyShipShoot != null)
            {
                enemyShipShoot.parentRoot = bulletParent;
            }
        }
    }
}
