using UnityEngine;
using UnityEngine.Rendering;

public class DefaultBullet : BulletBase
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float duration = 3f;
    

    private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        damage = 1;
        // Bullet destroys itself once the duration ends
        Destroy(gameObject, duration);
    }

    public override void Launch(Vector2 direction)
    {
        rb.linearVelocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boundary"))
        {
            // Destroys bullet when it hits an enemy or the boundaries of the map
            Destroy(gameObject);
            
        }
        
    }

}
