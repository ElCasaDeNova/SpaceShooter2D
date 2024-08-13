using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField]
    private Transform myTransform;

    public Transform playerOne;

    private Vector3 playerPosition;

    void Start()
    {

    }

    void Update()
    {
        // Get World position of Enemy
        playerPosition = playerOne.position;

        // Calculate direction from Enemy to player position
        Vector3 direction = (playerPosition - transform.position).normalized;

        // Apply rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90 )); // Add 90 degres because ennemy spawn upside down
    }
}