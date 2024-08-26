using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100f; // Maximum health of the enemy
    private float currentHealth;

    [SerializeField]
    private Image enemyHealthBarBackground;
    public Image EnemyHealthBarBackground { set { EnemyHealthBarBackground = value; } }

    [SerializeField]
    private Image enemyHealthBarFill; // Health bar displayed at the top of the screen
    public Image EnemyHealthBarFill { set { EnemyHealthBarFill = value; } }

    [SerializeField]
    private float enemyHealthBarVisibleTime = 1f; // Time the health bar will be visible at the top of the screen


    private static EnemyHealth lastHitEnemy; // To store the last enemy hit

    private bool isHealthBarCoroutineRunning = false;

    [SerializeField]
    private AudioSource audioSource;
    private AudioClip explosionSound;

    // For Wining Conditions
    public GameObject parentShip;
    public GameObject parentCruiser;
    public string nextScene;

    void Start()
    {
        explosionSound = audioSource.clip;

        currentHealth = maxHealth;

        if (enemyHealthBarBackground == null || enemyHealthBarFill == null)
        {
            return;
        }

        // Initialize the top screen health bar
        enemyHealthBarBackground.gameObject.SetActive(false);
        enemyHealthBarFill.gameObject.SetActive(false);
    }

    private void UpdateHealthBar()
    {
        if (enemyHealthBarFill != null)
        {
            float fillAmount = currentHealth / maxHealth;
            enemyHealthBarFill.fillAmount = fillAmount;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        if (enemyHealthBarFill != null && enemyHealthBarBackground != null)
        {
            // Show the health bar
            enemyHealthBarBackground.gameObject.SetActive(true);
            enemyHealthBarFill.gameObject.SetActive(true);
            UpdateHealthBar();

            if (!isHealthBarCoroutineRunning)
            {
                if (gameObject.activeInHierarchy)  // Ensure the GameObject is active
                {
                    StartCoroutine(HideEnemyHealthBarAfterDelay());
                }
            }
        }

        lastHitEnemy = this;
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

    void Die()
    {
        // Hide the health bar immediately
        if (enemyHealthBarBackground != null && enemyHealthBarFill != null)
        {
            enemyHealthBarBackground.gameObject.SetActive(false);
            enemyHealthBarFill.gameObject.SetActive(false);
        }

        if (explosionSound != null)
        {
            GameObject tempAudioSource = new GameObject("TempAudioSource");
            AudioSource tempSource = tempAudioSource.AddComponent<AudioSource>();
            tempSource.clip = explosionSound;
            tempSource.Play();
            Destroy(tempAudioSource, explosionSound.length);
        }

        // Return to the pool
        ShipPooler.Instance.ReturnShip(gameObject);
    }

    private void CheckIfNoEnemiesLeft()
    {
        if (parentShip != null && parentCruiser != null)
        {
            Debug.Log("Checking for remaining enemies...");
            Debug.Log("Ships count: " + parentShip.transform.childCount);
            Debug.Log("Cruisers count: " + parentCruiser.transform.childCount);

            if (parentShip.transform.childCount == 1 && parentCruiser.transform.childCount == 0)
            {
                Debug.Log("Victory condition met!");
                SceneManager.LoadScene(nextScene);
            }
        }
    }

    public void AssignHealthBar(Image background, Image fill)
    {
        enemyHealthBarBackground = background;
        enemyHealthBarFill = fill;

        // Mettre à jour la barre de santé au départ
        UpdateHealthBar();
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public static EnemyHealth GetLastHitEnemy()
    {
        return lastHitEnemy;
    }
}