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
        // Check if an enemy was hit last
        if (lastHitEnemy != null)
        {
            // Calculate the current health ratio of the last hit enemy
            float healthRatio = lastHitEnemy.currentHealth / lastHitEnemy.maxHealth;

            // Update the width of the health bar to reflect the current health of the last hit enemy
            RectTransform rectTransform = enemyHealthBarFill.rectTransform;
            float originalWidth = enemyHealthBarBackground.rectTransform.sizeDelta.x;
            rectTransform.sizeDelta = new Vector2(originalWidth * healthRatio, rectTransform.sizeDelta.y);
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
        else if (enemyHealthBarFill != null && enemyHealthBarBackground != null)
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
            AudioSource audioSourceInstance = AudioSourcePooler.Instance.GetAudioSource();
            audioSourceInstance.clip = explosionSound;
            audioSourceInstance.Play();

            // Return AudioSource to pool after the sound has finished playing
            StartCoroutine(ReturnAudioSourceToPoolAfterPlay(audioSourceInstance));
        }

        // Return to the pool
        ShipPooler.Instance.ReturnShip(gameObject);
    }

    private IEnumerator ReturnAudioSourceToPoolAfterPlay(AudioSource audioSource)
    {
        // Wait for the duration of the clip
        yield return new WaitForSeconds(audioSource.clip.length);
        // Return the AudioSource to the pool
        AudioSourcePooler.Instance.ReturnAudioSource(audioSource);
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

    public void SetCurrentHealth(float value)
    {
        currentHealth = value;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetMaxHealth(float value)
    {
        maxHealth=value;
    }

    public static EnemyHealth GetLastHitEnemy()
    {
        return lastHitEnemy;
    }
}