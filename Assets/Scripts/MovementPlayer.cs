using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float jumpForce = 13f;
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private Transform groundCheck;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool jumpReleased = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        UpdateAnimator();

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
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundlayer);

        if (Input.GetButton("Jump"))
        {
            if (isGrounded && jumpReleased)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpReleased = false;
            }
        }

        // Khi người chơi thả phím Jump → cho phép nhảy lại
        if (Input.GetButtonUp("Jump"))
        {
            jumpReleased = true;
        }
    }
    private void UpdateAnimator()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        bool isJumping = !isGrounded;
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
    }
}
