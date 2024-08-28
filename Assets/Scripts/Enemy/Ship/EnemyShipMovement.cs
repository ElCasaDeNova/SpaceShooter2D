using UnityEngine;

public class EnemyShipMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyShip;

    [SerializeField]
    private float speed = 5f; // Speed at which the object moves

    [SerializeField]
    private float collisionDamage = 25f;

    void Update()
    {
        // Move the object forward in the direction it's facing
        transform.position += -transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If Player collides
        if (other.gameObject.TryGetComponent<PlayerOneHealth>(out PlayerOneHealth player))
        {
            player.TakeDamage(collisionDamage);
            ShipPooler.Instance.ReturnShip(enemyShip);
        }
    }
}
