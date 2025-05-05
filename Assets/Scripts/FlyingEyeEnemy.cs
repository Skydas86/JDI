using UnityEngine;

public class FlyingEyeEnemy : Enemy
{
    [SerializeField] private float damage = 10f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(damage);
        }
    }
}
