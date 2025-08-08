using UnityEngine;

public class ExplosiveBullet : BulletBase
{
    // Blast Radius of 2f, Damage increased to 3
    //public ItemBase item;
    [SerializeField] private float blastDuration = 0.05f;
    [SerializeField] private CircleCollider2D impactCollider;
    [SerializeField] private CircleCollider2D blastCollider;
     [SerializeField] private SpriteRenderer spriteRenderer;
    private bool hitEnemy = false;

    protected override void Awake()
    {
        base.Awake();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        damage = 3;
        speed = 10f;
        duration = 3f;

        blastCollider.enabled = false;
    }

    public override void Launch(Vector2 direction)
    {
        rb.linearVelocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hitEnemy && (other.CompareTag("Enemy") || other.CompareTag("Boundary")))
        {
            Explode();
        }

    }

    private void Explode()
    {
        hitEnemy = true;
        blastCollider.enabled = true;

        // Stop bullet movement
        rb.linearVelocity = Vector2.zero;

        // Hide bullet sprite
        if (spriteRenderer != null)
            spriteRenderer.enabled = false;

        // Disable impact collider so it doesn't re-trigger
        impactCollider.enabled = false;

        Destroy(gameObject, blastDuration);

    }

}
