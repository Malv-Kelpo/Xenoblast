using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BulletBase : MonoBehaviour
{
    [Header("Bullet Stats")]
    public int damage;
    [SerializeField] protected float speed;
    [SerializeField] protected float duration;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Bullet destroys itself once the duration ends
        Destroy(gameObject, duration);
    }
    
    public abstract void Launch(Vector2 direction);
}
