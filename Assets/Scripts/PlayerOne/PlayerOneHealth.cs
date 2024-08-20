using UnityEngine;
using UnityEngine.UI;

public class PlayerOneHealth : MonoBehaviour
{
    // Variable for health points
    [SerializeField]
    private float maxHealth = 100f;
    private float currentHealth;

    [SerializeField]
    private Image healthBarFill;

    // Initialize health points
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Method to take damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    // Method called when health reaches zero
    void Die()
    {
        Debug.Log("You are dead");
    }

    void UpdateHealthBar()
    {
        float fillAmount = currentHealth / maxHealth;
        healthBarFill.fillAmount = fillAmount; // This updates the fill of the health bar
    }
}
