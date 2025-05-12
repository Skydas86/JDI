using UnityEngine;

public class MushroomEnemy : Enemy
{
    [SerializeField] private float damage = 10f;
    private bool isAttacking = false;
    private bool isDeath = false;
    private bool hasDamaged = false;
    protected override void Start()
    {
        capsuleCollider.enabled = false;
        base.Start();
    }
    protected override void Update()
    {
        if (isAttacking||isDeath) return;
        base.Update();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isAttacking)
        {
            isAttacking = true;
            hasDamaged = false;
            animator.SetBool("isAttack", true);
            Invoke(nameof(ResetAttack), 1.5f);
        }

        if (collision.CompareTag("Player") && isAttacking && !hasDamaged && capsuleCollider.enabled)
        {
            player.TakeDamage(damage);
            hasDamaged = true;
        }

        if (collision.CompareTag("Archer") && !isAttacking)
        {
            isAttacking = true;
            hasDamaged = false;
            animator.SetBool("isAttack", true);
            Invoke(nameof(ResetAttack), 1.5f);
        }

        if (collision.CompareTag("Archer") && isAttacking && !hasDamaged && capsuleCollider.enabled)
        {
            archer.TakeDamage(damage);
            hasDamaged = true;
        }
    }
    private void ResetAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttack", false);
    }
    private void EnableAttackCollider()
    {
        capsuleCollider.enabled = true;
        Invoke(nameof(DisableAttackCollider), 0.1f);
    }
    private void DisableAttackCollider()
    {
        capsuleCollider.enabled = false;
    }
    protected override void Die()
    {
        isDeath = true;
        animator.SetBool("isDeath", true);
        FindFirstObjectByType<EnemyPool>().ReturnPooledEnemy(gameObject);
        gameManager.AddCoin(Random.Range(1, 11));
    }
    protected override void Movement()
    {

        Vector2 direction = new Vector2(player.transform.position.x - transform.position.x, 0f);
        direction.Normalize();

        transform.Translate(direction * enemyMoveSpeed * Time.deltaTime);
        FlipEnemy();
    }
}
