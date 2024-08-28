using System.Collections;
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
            AudioSource audioSourceInstance = AudioSourcePooler.Instance.GetAudioSource();
            audioSourceInstance.clip = explosionSound;
            audioSourceInstance.Play();

            // Return AudioSource to pool after the sound has finished playing
            StartCoroutine(ReturnAudioSourceToPoolAfterPlay(audioSourceInstance));
        }

        // TODO Make MinePooler
        MinePooler.Instance.ReturnMine(mine);
    }

    private IEnumerator ReturnAudioSourceToPoolAfterPlay(AudioSource audioSource)
    {
        // Wait for the duration of the clip
        yield return new WaitForSeconds(audioSource.clip.length);
        // Return the AudioSource to the pool
        AudioSourcePooler.Instance.ReturnAudioSource(audioSource);
    }
}
