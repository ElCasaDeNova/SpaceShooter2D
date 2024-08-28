using UnityEngine;
using System.Collections.Generic;

public class AudioSourcePooler : MonoBehaviour
{
    public static AudioSourcePooler Instance;

    public GameObject audioSourcePrefab; // Un prefab contenant un AudioSource
    public int poolSize = 10;

    private Queue<AudioSource> audioSourcePool = new Queue<AudioSource>();

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize the pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(audioSourcePrefab);
            AudioSource audioSource = obj.GetComponent<AudioSource>();
            audioSource.transform.SetParent(transform);
            obj.SetActive(false);
            audioSourcePool.Enqueue(audioSource);
        }
    }

    public AudioSource GetAudioSource()
    {
        if (audioSourcePool.Count > 0)
        {
            AudioSource audioSource = audioSourcePool.Dequeue();
            audioSource.gameObject.SetActive(true);
            audioSource.transform.SetParent(transform);
            return audioSource;
        }
        else
        {
            // Optionnel: Étendre le pool si nécessaire
            GameObject obj = Instantiate(audioSourcePrefab);
            return obj.GetComponent<AudioSource>();
        }
    }

    public void ReturnAudioSource(AudioSource audioSource)
    {
        audioSource.Stop(); // Arrête la lecture du son
        audioSource.gameObject.SetActive(false); // Désactive le GameObject
        audioSource.transform.SetParent(transform);
        audioSourcePool.Enqueue(audioSource); // Retourne l'AudioSource au pool
    }
}
