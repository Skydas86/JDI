using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] protected GameManager gameManager;
    [SerializeField] private float moveSpeed = 5f;         
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float maxHp = 100f;
    [SerializeField] private float damageSlash = 50;
    [SerializeField] private float currentHp;

    public Slider slider;
    private Animator animator;
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private bool isGrounded;
    private bool isDeath=false;
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
        HandleMovement();
        HandleJump();
        UpdateAnimator();
        SwordSlash();
        CallArcher();
    }
    private void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); 
        }
        else if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); 
        }
    }
    private void HandleJump()
    {
        if(Input.GetButton("Jump")&&(isGrounded))
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundlayer);
    }
    private void UpdateAnimator()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        bool isJumping = !isGrounded;
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
    }
    private void SwordSlash()
    {
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetBool("isAttack", true);
            Invoke(nameof(ResetAttack), 0.3f);
        }    
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
    private void ResetAttack()
    {
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
    }
    private void CallArcher()
    {
        if (Input.GetKeyDown((KeyCode.R)))
        {
            gameManager.CallArcher();
        }
    }
}
