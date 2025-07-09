using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    [Header("Item Stats")]
    [SerializeField] public string itemLabel;
    protected float itemTimer = 0f;
    [SerializeField] public float itemDuration;

    [Header("Player")]
    [SerializeField] protected PlayerController player;

    protected virtual void Awake()
    {
        // Find the player automatically if not assigned
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.UseItem(this, itemDuration);
            Destroy(gameObject);
            Debug.Log(itemLabel + " Item used");
        }
    }

    public abstract void ItemAbility();
}
