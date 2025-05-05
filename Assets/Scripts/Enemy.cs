using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float enemyMoveSpeed=1f;
    [SerializeField] protected float maxHp = 100f;
    [SerializeField] protected float currentHp;

    protected Player player;
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
            // Lấy vị trí hiện tại của enemy
            Vector2 currentPosition = transform.position;

            // Tạo target mới: x là của player, y giữ nguyên
            Vector2 targetPosition = new Vector2(player.transform.position.x, currentPosition.y);

            // Di chuyển từ vị trí hiện tại tới target theo trục x
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
    private void Die()
    {
        Destroy(gameObject);
    }

    
}
