using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    // =========== Movement ===========
    [Header("Movement")]
    [SerializeField] public float defaultSpeed = 5f;
    private float currentSpeed;
    private Vector2 moveInput;

    // =========== Shooting ===========
    [Header("Shooting")]
    [SerializeField] private GameObject defaultBulletPrefab;
    private GameObject currentBulletPrefab;
    private Vector2 lookDirection = Vector2.down; // Player looking down by default

    // =========== Health ===========
    [Header("Health")]
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;
    public bool iFrameActive = false;
    [SerializeField] private float iFrameDuration = 3f;
    private float iFrameTimer = 0f;
    [SerializeField] private HealthDisplay healthDisplay;

    // =========== Items ===========
    [Header("Items")]
    public bool itemEquipped = false;
    private float itemTimer = 0f;
    private ItemBase activeItem;

    // =========== Sprites ===========
    [Header("Sprites")]
    // Order: Up, Up-Left, Left, Down-Left, Down, Down-Right, Right, Up-Right
    public Sprite[] bulletSprites;

    // =========== Components ===========
    private Rigidbody2D rb;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        currentSpeed = defaultSpeed;
        currentBulletPrefab = defaultBulletPrefab;

        if (healthDisplay != null)
        {
            healthDisplay.UpdateHealthDisplay(currentHealth);
        }
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

        if (itemEquipped)
        {
            itemTimer -= Time.deltaTime;
            if (itemTimer <= 0f)
            {
                SetPlayerDefaultSettings();
                Debug.Log("Item Duration done");
            }
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * currentSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private int GetDirectionIndex(Vector2 dir)
    {
        dir.Normalize();

        if (dir == Vector2.up) // Up
        {
            return 0;
        }
        else if (dir == new Vector2(-1, 1).normalized) // Up-Left
        {
            return 1;
        }
        else if (dir == Vector2.left) // Left
        {
            return 2;
        }
        else if (dir == new Vector2(-1, -1).normalized) // Down-Left
        {
            return 3;
        }
        else if (dir == Vector2.down) // Down
        {
            return 4;
        }
        else if (dir == new Vector2(1, -1).normalized) // Down-Right
        {
            return 5;
        }
        else if (dir == Vector2.right) // Right
        {
            return 6;
        }
        else // Up-Right
        {
            return 7;
        }
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

            GameObject bullet = Instantiate(currentBulletPrefab, bulletPosition, Quaternion.identity);

            // Select correct sprite
            SpriteRenderer sr = bullet.GetComponent<SpriteRenderer>();
            int dirIndex = GetDirectionIndex(lookDirection);

            if (sr != null && bulletSprites.Length == 8)
            {
                sr.sprite = bulletSprites[dirIndex];
            }

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

    public void UseItem(ItemBase item, float duration)
    {
        itemEquipped = true;
        if (activeItem != null)
        {
            Debug.Log("Replacing previous item");
        }

        activeItem = item;
        itemTimer = duration;

        item.ItemAbility();  // Call the item's ability logic

    }
    private void TakeDamage()
    {
        if (iFrameActive)
        {
            return;
        }

        currentHealth -= 1;
        Debug.Log("Player took 1 damage. Current Health: " + currentHealth);

        if (healthDisplay != null)
        {
            healthDisplay.UpdateHealthDisplay(currentHealth);
        }

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

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    public void SetCurrentSpeed(float amount)
    {
        currentSpeed = amount;
    }

    public GameObject GetBulletPrefab()
    {
        return currentBulletPrefab;
    }

    public void setBulletPrefab(GameObject bullet)
    {
        currentBulletPrefab = bullet;
    }

    public void SetPlayerDefaultSettings()
    {
        // Will add on when more settings are completed
        SetCurrentSpeed(defaultSpeed);
        setBulletPrefab(defaultBulletPrefab);
        itemEquipped = false;
    }
}
