using UnityEngine;

public class EnemyMineExplose : MonoBehaviour
{
    [SerializeField]
    private GameObject mine;

    [SerializeField]
    private float damage;

    [SerializeField]
    private AudioSource audioSource;
    private AudioClip explosionSound;

    private void Start()
    {
        // Get the AudioSource component on this GameObject
        explosionSound = audioSource.clip;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If Player collides
        if (other.gameObject.TryGetComponent<PlayerOneHealth>(out PlayerOneHealth player))
        {
            // TODO Explosion has a radius
            player.TakeDamage(damage);
            Explose();
        }
    }

    public void Explose()
    {
        // TODO Explosion has an Animation

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

        // TODO Make MinePooler
        Destroy(mine);
    }
}
