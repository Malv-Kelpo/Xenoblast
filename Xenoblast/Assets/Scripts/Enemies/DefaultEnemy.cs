using UnityEngine;

public class DefaultEnemy : EnemyBase
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform player;
    Vector2 moveDirection;

    private Rigidbody2D rb;
    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        maxHealth = 1;
        base.Start();
    }

    void Update()
    {
        // movement toward player
        if (player)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
    }
}
