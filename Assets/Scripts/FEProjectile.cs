using UnityEngine;

public class FEProjectile : MonoBehaviour
{
    private Vector3 movementToDirection;
    private Animator animator;
    protected Player player;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        Destroy(gameObject,5f);
        player = FindAnyObjectByType<Player>();
    }

    void Update()
    {
        if (movementToDirection == Vector3.zero)
            return;
        transform.position += movementToDirection*Time.deltaTime;
    }
    public void SetMovementDirection(Vector3 direction)
    {
        movementToDirection = direction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movementToDirection = Vector3.zero;
            animator.SetBool("isHit", true);
            player.TakeDamage(5f);
        }
        
    }
}
