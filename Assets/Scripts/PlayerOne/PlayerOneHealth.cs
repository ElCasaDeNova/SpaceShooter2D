using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerOneHealth : MonoBehaviour
{
    // Variable for health points
    [SerializeField]
    private float maxHealth = 100f;
    private float currentHealth;

    [SerializeField]
    private Image healthBarFill;

    [SerializeField]
    private AudioSource damageAudioSource;
    private AudioClip damageSound;

    [SerializeField]
    private AudioSource deathAudioSource;
    private AudioClip deathSound;

    [SerializeField]
    private Image damageFlashImage;

    [SerializeField]
    private float flashDuration = 0.5f;

    // Initialize health points
    void Start()
    {
        currentHealth = maxHealth;
        damageSound = damageAudioSource.clip;
        deathSound = deathAudioSource.clip;

        if (damageFlashImage != null)
        {
            damageFlashImage.color = new Color(damageFlashImage.color.r, damageFlashImage.color.g, damageFlashImage.color.b, 0);
        }
    }

    // Method to take damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();

        if (damageFlashImage != null)
        {
            StartCoroutine(FlashDamageScreen());
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    // Method called when health reaches zero
    void Die()
    {
        if (deathSound != null)
        {
            // Create GameObject so the Enemy Destruction doesn't block or wait for the explosion sound
            GameObject tempAudioSource = new GameObject("TempAudioSource");
            AudioSource tempSource = tempAudioSource.AddComponent<AudioSource>();
            tempSource.clip = deathSound;
            tempSource.Play();

            // Destroy GameObject when sound is done
            Destroy(tempAudioSource, deathSound.length);
        }

        // Debug.Log("You are dead");
        SceneManager.LoadScene("GameOver");
    }

    void UpdateHealthBar()
    {
        float fillAmount = currentHealth / maxHealth;
        healthBarFill.fillAmount = fillAmount; // This updates the fill of the health bar

        //LUMIERE ROUGE ICI

        // Play the damage sound
        if (damageAudioSource != null && damageSound != null)
        {
            damageAudioSource.PlayOneShot(damageSound);
        }
    }

    private IEnumerator FlashDamageScreen()
    {
        // Display Red flash
        float elapsedTime = 0f;

        while (elapsedTime < flashDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / flashDuration);
            damageFlashImage.color = new Color(damageFlashImage.color.r, damageFlashImage.color.g, damageFlashImage.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Image is transparent at the end
        damageFlashImage.color = new Color(damageFlashImage.color.r, damageFlashImage.color.g, damageFlashImage.color.b, 0f);
    }
}
