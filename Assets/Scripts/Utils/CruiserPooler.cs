using System.Collections.Generic;
using UnityEngine;

public class CruiserPooler : MonoBehaviour
{

    public static CruiserPooler Instance;

    public GameObject cruiserPrefab; // The cruiser prefab to pool
    public int initialPoolSize = 20; // Initial pool size

    private Queue<GameObject> cruiserPool;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize the pool
        cruiserPool = new Queue<GameObject>();
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject cruiser = Instantiate(cruiserPrefab);
            cruiser.transform.SetParent(transform);
            cruiser.SetActive(false);
            cruiserPool.Enqueue(cruiser);
        }
    }

    public GameObject GetCruiser()
    {
        if (cruiserPool.Count > 0)
        {
            GameObject cruiser = cruiserPool.Dequeue();
            cruiser.SetActive(true);
            return cruiser;
        }
        else
        {
            // Optionally create a new cruiser if the pool is empty
            GameObject cruiser = Instantiate(cruiserPrefab);
            return cruiser;
        }
    }

    public void ReturnCruiser(GameObject cruiser)
    {
        cruiser.SetActive(false);
        cruiser.transform.SetParent(transform);
        cruiserPool.Enqueue(cruiser);
    }
}
