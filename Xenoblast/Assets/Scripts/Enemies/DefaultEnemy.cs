using UnityEngine;

public class DefaultEnemy : EnemyBase
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private PlayerController player;
    Vector2 moveDirection;

    private Rigidbody2D rb;
    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        maxHealth = 1;

        // Find the player automatically if not assigned
        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.GetComponent<PlayerController>();
            }
        }

        base.Start();
    }

    void Update()
    {
        // movement toward player
        if (player)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;

            // If the player has iFrames, reverse the direction
            if (player.iFrameActive)
            {
                direction = -direction;
            }
            
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
    }
}