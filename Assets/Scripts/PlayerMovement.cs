using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;         
    [SerializeField] private float jumpForce = 10f; 
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private transform groundCheck;
    private Rigidbody2D rb;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
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
            transform.localScale = new Vector3(1, 1, 1); // Mặt về phải
        }
    }

    private void HandleJump()
    {
        if(Input.GetButton("Jump"))
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        isGrounded = Physics2D.OverCircle(groundCheck.po)
    }

}
