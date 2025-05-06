using UnityEngine;

public class MushroomEnemy : Enemy
{
    [SerializeField] private float damage = 10f;
    private bool isAttacking = false;
    private bool isDeath = false;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isAttacking = true;
            animator.SetBool("isAttack", true);
        }
        if (capsuleCollider.enabled)
            player.TakeDamage(damage);
        Invoke(nameof(ResetAttack),1.5f);
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
        Destroy(gameObject, 5f);
    }
}
