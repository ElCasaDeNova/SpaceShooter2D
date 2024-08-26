using System.Collections.Generic;
using UnityEngine;

public class ShipPooler : MonoBehaviour
{
    public static ShipPooler Instance;

    public GameObject shipPrefab; // The ship prefab to pool
    public int initialPoolSize = 20; // Initial pool size

    private Queue<GameObject> shipPool;

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
        shipPool = new Queue<GameObject>();
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject ship = Instantiate(shipPrefab);
            ship.SetActive(false);
            shipPool.Enqueue(ship);
        }
    }

    public GameObject GetShip()
    {
        if (shipPool.Count > 0)
        {
            GameObject ship = shipPool.Dequeue();
            ship.SetActive(true);
            return ship;
        }
        else
        {
            // Optionally create a new ship if the pool is empty
            GameObject ship = Instantiate(shipPrefab);
            return ship;
        }
    }

    public void ReturnShip(GameObject ship)
    {
        ship.SetActive(false);
        ship.transform.SetParent(transform);
        shipPool.Enqueue(ship);
    }
}
