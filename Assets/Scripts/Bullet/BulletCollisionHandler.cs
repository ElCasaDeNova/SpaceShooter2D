using UnityEngine;

public class BulletCollisionHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other object has a Health component
        if (other.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
        {
            // Now you can interact with the Health component
            enemy.TakeDamage(damage); // Example of dealing damage
        }

        //Delete the bullet
        Destroy(bullet);
    }
}
