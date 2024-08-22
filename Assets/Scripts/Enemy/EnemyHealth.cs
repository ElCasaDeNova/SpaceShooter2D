using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100f; // Maximum health of the enemy
    private float currentHealth;

    [SerializeField]
    private Image enemyHealthBarBackground;

    [SerializeField]
    private Image enemyHealthBarFill; // Health bar displayed at the top of the screen

    [SerializeField]
    private float enemyHealthBarVisibleTime = 1f; // Time the health bar will be visible at the top of the screen

    private static EnemyHealth lastHitEnemy; // To store the last enemy hit

    private bool isHealthBarCoroutineRunning = false;

    [SerializeField]
    private AudioClip explosionSound;

    [SerializeField]
    private AudioSource audioSource;

    void Start()
    {
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
            // Create GameObject so the Enemy Destruction doesn't block or wait for the explosion sound
            GameObject tempAudioSource = new GameObject("TempAudioSource");
            AudioSource tempSource = tempAudioSource.AddComponent<AudioSource>();
            tempSource.clip = explosionSound;
            tempSource.Play();

            // Destroy GameObject when sound is done
            Destroy(tempAudioSource, explosionSound.length);
        }

        // Destroy the enemy GameObject
        Destroy(gameObject);
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