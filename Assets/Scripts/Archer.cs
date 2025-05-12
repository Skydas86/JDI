using UnityEngine;

public class Archer : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float detectDistance = 10f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float maxHp = 50f;
    [SerializeField] private float currentHp;
    [SerializeField] private GameObject arrowPrefabs;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float speedArrow = 20f;
    [SerializeField] private float cooldown = 3f;
    [SerializeField] private GameObject ray;
    private float nextArrowTime = 0;

    private Animator animator;
    private Enemy detectedEnemy;
    private bool isDeath = false;
    private bool isAttacking = false;  

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        currentHp = maxHp;
    }

    void Update()
    {
        if (isDeath) return;

        if (isAttacking)
        {
            return;  
        }

        if (IsEnemyAhead() && Time.time >= nextArrowTime)
        {
            isAttacking = true;  
            animator.SetBool("isAttacking", true);
            Shoot();
        }
        else
        {
            Movement();
        }
    }

    private void Movement()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        animator.SetBool("isRunning", true);
    }

    private bool IsEnemyAhead()
    {
        Vector2 direction = Vector2.right;
        Debug.DrawRay(transform.position, direction * detectDistance, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectDistance, enemyLayer);

        Vector2 direction2 = Vector2.right;
        Debug.DrawRay(ray.transform.position, direction * detectDistance, Color.red);
        RaycastHit2D hit2 = Physics2D.Raycast(ray.transform.position, direction, detectDistance, enemyLayer);

        if (hit2.collider != null && hit2.collider.CompareTag("Enemy"))
        {
            detectedEnemy = hit2.collider.GetComponent<Enemy>();
            return true;
        }
        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            detectedEnemy = hit.collider.GetComponent<Enemy>();
            return true;
        }

        detectedEnemy = null;
        return false;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);
        if (currentHp <= 0)
            Die();
    }

    private void Die()
    {
        isDeath = true;
        animator.SetBool("isDeath", true);
        Destroy(gameObject, 1f);
    }

    private void Shoot()
    {
        if (detectedEnemy != null)
        {
            nextArrowTime = Time.time + cooldown;
            Vector3 directionToEnemy = (detectedEnemy.transform.position - firePoint.position).normalized;

            GameObject projectile = Instantiate(arrowPrefabs, firePoint.position, Quaternion.identity);
            Arrow arrow = projectile.GetComponent<Arrow>();
            arrow.SetMovementDirection(directionToEnemy * speedArrow);

            Invoke(nameof(ResetAttack), cooldown);  
        }
    }

    private void ResetAttack()
    {
        isAttacking = false; 
        animator.SetBool("isAttacking", false);
    }
}
