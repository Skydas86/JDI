using UnityEngine;

public class FlyingEyeEnemy : Enemy
{
    [SerializeField] private GameObject fEProjectilePrefabs;
    [SerializeField] private Transform firePoi;
    [SerializeField] private float speedProjectile = 20f;
    [SerializeField] private float cooldownProjectile = 2f;
    private float nextProjectileTime = 0;
    protected override void Update()
    {
        base.Update();
        if (Vector3.Distance(player.transform.position, transform.position) <= 5f && Time.time >= nextProjectileTime)
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        if (player != null)
        {
            nextProjectileTime = Time.time + cooldownProjectile;
            Vector3 directionToPlayer = player.transform.position-firePoi.position;
            directionToPlayer.Normalize();
            GameObject projectile = Instantiate(fEProjectilePrefabs,firePoi.position,Quaternion.identity);
            FEProjectile fEProjectile = projectile.GetComponent<FEProjectile>();
            fEProjectile.SetMovementDirection(directionToPlayer*speedProjectile);
        }
    }

    
}
