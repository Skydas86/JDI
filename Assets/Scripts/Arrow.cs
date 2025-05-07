using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float damage = 25f;
    private Vector3 movementToDirection;
    private void Awake()
    {

    }
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        if (movementToDirection == Vector3.zero)
            return;
        transform.position += movementToDirection * Time.deltaTime;
    }
    public void SetMovementDirection(Vector3 direction)
    {
        movementToDirection = direction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            movementToDirection = Vector3.zero;
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

    }
}
