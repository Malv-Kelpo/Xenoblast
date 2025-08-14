using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ExplosiveBullet : BulletBase
{
    // Blast Radius of 2f, Damage increased to 3
    [Header("Explosive Bullet Settings")]
    [SerializeField] private float blastDuration = 0.05f;
    [SerializeField] private float blastRadius = 2f;
    public string blastSFXName;
    private bool hitEnemy = false;

    public GameObject explosionPrefab;


    [Header("Components")]
    [SerializeField] private CircleCollider2D impactCollider; // Collider to check initial impact with Enemy
    [SerializeField] private CircleCollider2D blastCollider; // Larger Collider that contains the blast area
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    
    protected override void Awake()
    {
        // Assigning SFX
        launchSFXName = "ExplosiveBulletLaunch";
        blastSFXName = "ExplosiveBulletBlast";

        blastCollider.enabled = false;
        blastCollider.radius = blastRadius;

        base.Awake();
    }

    void Start()
    {
        damage = 3;
        speed = 10f;
        duration = 3f;
    }

    public override void Launch(Vector2 direction)
    {
        AudioManager.Instance.PlaySFX(launchSFXName);
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
        // Explosion SFX
        AudioManager.Instance.PlaySFX(blastSFXName);

        // Explosion animation
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Destroys Explosion GameObject when animation is done
        float animLength = explosion.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
        Destroy(explosion, animLength);

        // Enables blast collider to damage nearby enemies when bullet hits an enemy
        hitEnemy = true;
        blastCollider.enabled = true;

        // Stop bullet movement
        rb.linearVelocity = Vector2.zero;

        // Hide bullet sprite
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
        
        // Disable impact collider so it doesn't re-trigger
        impactCollider.enabled = false;

        Destroy(gameObject, blastDuration);
    }

}
