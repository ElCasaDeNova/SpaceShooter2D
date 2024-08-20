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

    void Start()
    {
        currentHealth = maxHealth;

        // Initialize the top screen health bar
        if (enemyHealthBarFill != null)
        {
            enemyHealthBarFill.gameObject.SetActive(false); // Hide the health bar at the start
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

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        // Update and show the health bar at the top of the screen
        if (enemyHealthBarFill != null)
        {
            // Show the health bar at the top of the screen
            enemyHealthBarBackground.gameObject.SetActive(true);
            enemyHealthBarFill.gameObject.SetActive(true);
            UpdateHealthBar();
            StopAllCoroutines();
            StartCoroutine(HideEnemyHealthBarAfterDelay());
        }

        // Update the last hit enemy
        lastHitEnemy = this;
    }

    private IEnumerator HideEnemyHealthBarAfterDelay()
    {
        yield return new WaitForSeconds(enemyHealthBarVisibleTime);
        if (enemyHealthBarFill != null)
        {
            // Hide the health bar after the delay
            enemyHealthBarBackground.gameObject.SetActive(false);
            enemyHealthBarFill.gameObject.SetActive(false);
        }
    }

    void Die()
    {
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
