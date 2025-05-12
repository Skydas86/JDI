using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected GameManager gameManager;
    [SerializeField] protected float enemyMoveSpeed=1f;
    [SerializeField] protected float maxHp = 100f;
    [SerializeField] protected float currentHp;


    protected CapsuleCollider2D capsuleCollider;
    protected Animator animator;
    protected Player player;
    protected Archer archer;
    protected void Awake()
    {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    protected virtual void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        player = FindAnyObjectByType<Player>();
        archer = FindAnyObjectByType<Archer>();
        currentHp = maxHp;
    }
    protected virtual void Update()
    {
        Movement();
    }

    protected virtual void Movement()
    {
        transform.Translate(Vector2.left * enemyMoveSpeed * Time.deltaTime);
        FlipEnemy();
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
        FindFirstObjectByType<EnemyPool>().ReturnPooledEnemy(gameObject);
        gameManager.AddCoin(Random.Range(1, 6));
        
    }

    
}
