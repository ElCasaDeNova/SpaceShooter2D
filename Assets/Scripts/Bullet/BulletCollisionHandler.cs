using UnityEngine;

public class BulletCollisionHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private float damage;

    private bool fromPlayer;
    public bool FromPlayer { set { fromPlayer = value; } }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!fromPlayer)
        {
            // If Player is touched
            if (other.gameObject.TryGetComponent<PlayerOneHealth>(out PlayerOneHealth player))
            {
                player.TakeDamage(damage);
                BulletPooler.Instance.ReturnBullet(gameObject);
            }
        }
        else
        {
            // If Enemy is touched
            if (other.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
            {
                enemy.TakeDamage(damage);
                BulletPooler.Instance.ReturnBullet(gameObject);
            }

            // If Mine is touched
            if (other.gameObject.TryGetComponent<EnemyMineExplose>(out EnemyMineExplose enemyMine))
            {
                enemyMine.Explose();
                BulletPooler.Instance.ReturnBullet(gameObject);
            }


            // If EnemyCruiser is touched
            if (other.gameObject.TryGetComponent<EnemyCruiserHealth>(out EnemyCruiserHealth enemyCruiser))
            {
                enemyCruiser.TakeDamage(damage);
                BulletPooler.Instance.ReturnBullet(gameObject);
            }
        }

    }

}
