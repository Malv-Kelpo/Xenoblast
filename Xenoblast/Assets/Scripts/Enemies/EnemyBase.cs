using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] public int maxHealth;
    protected int currentHealth;
    [SerializeField] protected float speed;

    protected Rigidbody2D rb;
    protected Vector2 moveDirection;

    [SerializeField] protected PlayerController player;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        // Find the player automatically if not assigned
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
    }

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }
    
    protected virtual void Update()
    {
        EnemyMovement();
    }

    protected virtual void FixedUpdate()
    {
        ApplyMovement();
    }

    protected virtual void EnemyMovement()
    {
        if (player)
        {
            // Default movement where the enemy moves straight to the player
            Vector3 direction = (player.transform.position - transform.position).normalized;

            // If the player has iFrames, reverse the direction
            if (player.iFrameActive)
            {
                direction = -direction;
            }
            
            moveDirection = direction;
        }
    }

    protected virtual void ApplyMovement()
    {
        rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            BulletBase bullet = other.GetComponent<BulletBase>();
            TakeDamage(bullet.damage);
        }
    }
    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
        GameManager.gameManagerInstance.AddScore(1);
        Debug.Log("Enemy died");
    }
}
