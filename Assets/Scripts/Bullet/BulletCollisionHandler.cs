using UnityEngine;

public class BulletCollisionHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If Enemy is touched
        if (other.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
        {
            // Now you can interact with the Health component
            enemy.TakeDamage(damage); // Example of dealing damage
                                      //Delete the bullet
            Destroy(bullet);
        }

        // If Player is touched// If Enemy is touched
        if (other.gameObject.TryGetComponent<PlayerOneHealth>(out PlayerOneHealth player))
        {
            // Now you can interact with the Health component
            player.TakeDamage(damage); // Example of dealing damage
                                       //Delete the bullet
            Destroy(bullet);
        }

        
    }
}
