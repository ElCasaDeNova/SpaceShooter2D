using System.Collections.Generic;
using UnityEngine;

public class MinePooler : MonoBehaviour
{

    public static MinePooler Instance;

    public GameObject minePrefab; // The mine prefab to pool
    public int initialPoolSize = 20; // Initial pool size

    private Queue<GameObject> minePool;

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
        minePool = new Queue<GameObject>();
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject mine = Instantiate(minePrefab);
            mine.transform.SetParent(transform);
            mine.SetActive(false);
            minePool.Enqueue(mine);
        }
    }

    public GameObject GetMine()
    {
        if (minePool.Count > 0)
        {
            GameObject mine = minePool.Dequeue();
            mine.SetActive(true);
            return mine;
        }
        else
        {
            // Optionally create a new mine if the pool is empty
            GameObject mine = Instantiate(minePrefab);
            mine.transform.SetParent(transform);
            return mine;
        }
    }

    public void ReturnMine(GameObject mine)
    {
        mine.SetActive(false);
        mine.transform.SetParent(transform);
        minePool.Enqueue(mine);
    }
}
