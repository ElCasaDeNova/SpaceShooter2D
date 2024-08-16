using UnityEngine;

public class EnemyMineExplose : MonoBehaviour
{
    [SerializeField]
    private GameObject mine;

    [SerializeField]
    private float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If Player collides
        if (other.gameObject.TryGetComponent<PlayerOneHealth>(out PlayerOneHealth player))
        {
            player.TakeDamage(damage);
            Destroy(mine);
        }
    }
}
