using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] protected GameManager gameManager;
    [SerializeField] private float maxHp = 100f;
    [SerializeField] private float damageSlash = 50;
    [SerializeField] private float currentHp;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private float attackCooldown = 0.5f; // thời gian hồi chiêu giữa 2 đòn

    public Slider slider;
    private Animator animator;
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private bool isGrounded;
    private bool isDeath=false;
    private bool canAttack = true;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    void Start()
    {
        DisableAttackCollider();
        currentHp = maxHp;
        slider.maxValue = maxHp;
        slider.value = currentHp;
    }
    void Update()
    {
        if (isDeath)
        {
            return;
        }
        
        SwordSlash();
        CallArcher();
        PauseGame();
    }


    private void SwordSlash()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            canAttack = false;
            animator.SetBool("isAttack", true);
            EnableAttackCollider();
            audioManager.PlaySlashSound();
            Invoke(nameof(ResetAttack), 0.3f); // reset animation
            Invoke(nameof(ResetAttackCooldown), attackCooldown); // reset khả năng chém
        }
    }
    private void ResetAttack()
    {
        animator.SetBool("isAttack", false);
        DisableAttackCollider();
    }
    private void ResetAttackCooldown()
    {
        canAttack = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (capsuleCollider.enabled && collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damageSlash);
            }
        }
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
    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        slider.value = currentHp;
        currentHp = Mathf.Max(currentHp, 0);
        if (currentHp <= 0)
            Die();
    }
    private void Die()
    {
        isDeath = true;
        animator.SetBool("isDeath", true);
        gameManager.GameOverMenu();
    }
    private void CallArcher()
    {
        if (Input.GetKeyDown((KeyCode.R)))
        {
            gameManager.CallArcher();
        }
    }
    private void PauseGame()
    {
        if (Input.GetKeyDown((KeyCode.Escape)))
        {
            gameManager.PauseGameMenu();
        }
    }
}
