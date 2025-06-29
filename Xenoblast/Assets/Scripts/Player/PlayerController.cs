using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    // =========== Movement ===========
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    private Vector2 moveInput;

    // =========== Shooting ===========
    [Header("Shooting")]
    [SerializeField] private GameObject bulletPrefab;
    private Vector2 lookDirection = Vector2.down; // Player looking down by default

    // =========== Health ===========
    [Header("Health")]
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;
    public bool iFrameActive = false;
    [SerializeField] private float iFrameDuration = 3f;
    private float iFrameTimer = 0f;
    

    // =========== Animation ===========
    // variables for animation

    // =========== Components ===========
    private Rigidbody2D rb;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Update look direction if the player is moving
        if (moveInput.sqrMagnitude > 0.01f)
        {
            lookDirection = moveInput.normalized;
        }

        if (iFrameActive)
        {
            iFrameTimer -= Time.deltaTime;
            if (iFrameTimer <= 0f)
            {
                iFrameActive = false;
            }
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Prevents player from shooting if they have iframes active
            if (iFrameActive)
            {
                return;
            }
            
            // Spawns bullet slightly in front of player
            Vector2 bulletPosition = rb.position + lookDirection * 0.5f;

            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);

            BulletBase bulletScript = bullet.GetComponent<BulletBase>();

            if (bulletScript != null)
            {
                bulletScript.Launch(lookDirection);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        if (iFrameActive)
        {
            return;
        }

        currentHealth -= 1;
        Debug.Log("Player took 1 damage. Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }

        iFrameActive = true;
        iFrameTimer = iFrameDuration;

        // Flashing effect
    }

    private void Die()
    {
        Destroy(gameObject);
        Debug.Log("Player is dead");
    }

    
}
