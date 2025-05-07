using UnityEngine;

public class FlyingEyeEnemy : Enemy
{
    [SerializeField] private GameObject fEProjectilePrefabs;
    [SerializeField] private Transform firePoi;
    [SerializeField] private float speedProjectile = 20f;
    [SerializeField] private float cooldownProjectile = 2f;
    private bool isAttacking = false;
    private float nextProjectileTime = 0;
    protected override void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= 5f)
        {
            if (Time.time >= nextProjectileTime)
            {
                Shoot();
            }

            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

        if (!isAttacking)
        {
            Movement();
        }
    }
    private void Shoot()
    {
        if (player != null)
        {
            isAttacking = true;
            nextProjectileTime = Time.time + cooldownProjectile;
            Vector3 directionToPlayer = player.transform.position-firePoi.position;
            directionToPlayer.Normalize();
            GameObject projectile = Instantiate(fEProjectilePrefabs,firePoi.position,Quaternion.identity);
            FEProjectile fEProjectile = projectile.GetComponent<FEProjectile>();
            fEProjectile.SetMovementDirection(directionToPlayer*speedProjectile);
        }
    }
    protected override void Movement()
    {

        Vector2 direction = new Vector2(player.transform.position.x - transform.position.x, 0f);
        direction.Normalize();

        transform.Translate(direction * enemyMoveSpeed * Time.deltaTime);
        FlipEnemy();
    }

}
