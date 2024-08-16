using UnityEngine;

public class BulletCollisionHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If Enemy is touched
        if (other.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
        {
            enemy.TakeDamage(damage);
            Destroy(bullet);
        }

        // If EnemyCruiser is touched
        if (other.gameObject.TryGetComponent<EnemyCruiserHealth>(out EnemyCruiserHealth enemyCruiser))
        {
            enemyCruiser.TakeDamage(damage);
            Destroy(bullet);
        }

        // If Player is touched
        if (other.gameObject.TryGetComponent<PlayerOneHealth>(out PlayerOneHealth player))
        {
            player.TakeDamage(damage);
            Destroy(bullet);
        }


    }
}
