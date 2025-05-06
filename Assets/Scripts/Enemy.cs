using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float enemyMoveSpeed=1f;
    [SerializeField] protected float maxHp = 100f;
    [SerializeField] protected float currentHp;


    protected CapsuleCollider2D capsuleCollider;
    protected Animator animator;
    protected Player player;
    protected void Awake()
    {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    protected virtual void Start()
    {
        player = FindAnyObjectByType<Player>();
        currentHp = maxHp;
    }
    protected virtual void Update()
    {
        MoveToPlayer();
    }

    protected virtual void MoveToPlayer()
    {
        if (player != null) 
        {
            Vector2 currentPosition = transform.position;

            Vector2 targetPosition = new Vector2(player.transform.position.x, currentPosition.y);

            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, enemyMoveSpeed * Time.deltaTime);
            FlipEnemy();
        }
        return;
        
    }
    protected void FlipEnemy()
    {
        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public virtual void TakeDamage(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);
        if (currentHp <= 0)
            Die();
    }
    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    
}
