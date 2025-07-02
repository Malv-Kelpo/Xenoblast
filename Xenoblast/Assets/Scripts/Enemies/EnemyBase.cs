using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public int maxHealth;
    protected int currentHealth;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
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
