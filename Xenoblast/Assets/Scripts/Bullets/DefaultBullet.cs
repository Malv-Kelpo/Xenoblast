using UnityEngine;
using UnityEngine.Rendering;

public class DefaultBullet : BulletBase
{
    protected override void Awake()
    {
        launchSFXName = "DefaultBullet";
        base.Awake();
    }
    void Start()
    {
        damage = 1;
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
        if (other.CompareTag("Enemy") || other.CompareTag("Boundary"))
        {
            // Destroys bullet when it hits an enemy or the boundaries of the map
            Destroy(gameObject);
            
        }
        
    }

}
