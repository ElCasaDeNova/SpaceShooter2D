using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private Image enemyHealthBarBackground;

    [SerializeField]
    private Image enemyHealthBarFill; // Health bar displayed at the top of the screen


    [SerializeField]
    private float enemyHealthBarVisibleTime = 1f; // Time the health bar will be visible at the top of the screen

    private static EnemyCruiserHealth lastHitEnemy; // To store the last enemy hit

    private bool isHealthBarCoroutineRunning = false;

    [SerializeField]
    private AudioSource audioSource;
    private AudioClip explosionSound;

    // For Wining Conditions
    [SerializeField]
    private GameObject parentShip;
    [SerializeField]
    private GameObject parentCruiser;
    [SerializeField]
    private string nextScene;

    // Initialize health points
    void Start()
    {
        currentHealth = maxHealth;

        // Get the AudioSource component on this GameObject
        explosionSound = audioSource.clip;

        // Set spawnPoints from ScriptSpawnersManagement
        if (spawnersManager != null)
        {
            spawnPoints = spawnersManager.spawners;
        }
        else
        {
            Debug.LogWarning("ScriptSpawnersManagement reference not set.");
        }

        if (enemyHealthBarBackground == null || enemyHealthBarFill == null)
        {
            return;
        }

        // Initialize the top screen health bar
        enemyHealthBarBackground.gameObject.SetActive(false);
        enemyHealthBarFill.gameObject.SetActive(false);
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

        // Update and show the health bar at the top of the screen
        if (enemyHealthBarFill != null && enemyHealthBarBackground != null)
        {
            // Show the health bar at the top of the screen
            enemyHealthBarBackground.gameObject.SetActive(true);
            enemyHealthBarFill.gameObject.SetActive(true);
            UpdateHealthBar();

            if (!isHealthBarCoroutineRunning)
            {
                StartCoroutine(HideEnemyHealthBarAfterDelay());
            }
        }

        // Update the last hit enemy
        lastHitEnemy = this;
    }

    // Method called when health reaches zero
    void Die()
    {
        // Increment the number of destroyed cruisers
        destroyedCruisers++;

        // Spawn ships
        SpawnShips();

        // Hide the health bar immediately
        if (enemyHealthBarBackground != null && enemyHealthBarFill != null)
        {
            enemyHealthBarBackground.gameObject.SetActive(false);
            enemyHealthBarFill.gameObject.SetActive(false);
        }

        if (explosionSound != null)
        {
            // Create GameObject so the Cruiser Destruction doesn't block or wait for the explosion sound
            GameObject tempAudioSource = new GameObject("TempAudioSource");
            AudioSource tempSource = tempAudioSource.AddComponent<AudioSource>();
            tempSource.clip = explosionSound;
            tempSource.Play();

            // Destroy GameObject when sound is done
            Destroy(tempAudioSource, explosionSound.length);
        }

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
            GameObject shipInstance = ShipPooler.Instance.GetShip();

            // Set the position and rotation
            shipInstance.transform.position = spawnPoint.position;
            shipInstance.transform.rotation = spawnPoint.rotation;

            // Set the parent
            shipInstance.transform.SetParent(shipParentRoot);

            // Assign the playerOne reference to the EnemyFollow script on the ship
            EnemyFollow enemyFollow = shipInstance.GetComponent<EnemyFollow>();
            if (enemyFollow != null)
            {
                enemyFollow.playerOne = playerOne;
            }

            // Assign the parent to the Ship bullets
            EnemyShipShoot enemyShipShoot = shipInstance.GetComponent<EnemyShipShoot>();
            if (enemyShipShoot != null)
            {
                enemyShipShoot.parentRoot = bulletParent;
            }

            //Assign the HealthBar to the Ship
            EnemyHealth enemyHealth = shipInstance.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.AssignHealthBar(enemyHealthBarBackground, enemyHealthBarFill);
                enemyHealth.parentShip = parentShip;
                enemyHealth.parentCruiser = parentCruiser;
                enemyHealth.nextScene = nextScene;
            }
        }
    }

    private void UpdateHealthBar()
    {
        if (enemyHealthBarFill != null)
        {
            float fillAmount = currentHealth / maxHealth;
            enemyHealthBarFill.fillAmount = fillAmount;
        }
    }

    private IEnumerator HideEnemyHealthBarAfterDelay()
    {
        isHealthBarCoroutineRunning = true;
        yield return new WaitForSeconds(enemyHealthBarVisibleTime);

        if (enemyHealthBarFill != null && enemyHealthBarBackground != null)
        {
            // Hide the health bar after the delay
            enemyHealthBarBackground.gameObject.SetActive(false);
            enemyHealthBarFill.gameObject.SetActive(false);
        }

        isHealthBarCoroutineRunning = false;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public static EnemyCruiserHealth GetLastHitEnemy()
    {
        return lastHitEnemy;
    }
}
