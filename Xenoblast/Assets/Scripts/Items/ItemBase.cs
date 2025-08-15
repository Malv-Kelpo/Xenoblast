using Unity.VisualScripting;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    // ========= Item Stats =========
    [Header("Item Stats")]
    [SerializeField] public string itemLabel;
    [SerializeField] public float itemAbilityDuration;
    [SerializeField] public float itemDespawnDuration = 10f;
    private float itemDespawnTimer = 0f;

    // ========= Player =========
    [Header("Player")]
    [SerializeField] protected PlayerController player;

    protected virtual void Awake()
    {
        // Find the player automatically if not assigned
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        itemDespawnTimer = itemDespawnDuration;
    }

    protected virtual void Update()
    {
        ItemDespawn();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.UseItem(this, itemAbilityDuration);
            Destroy(gameObject);
            Debug.Log(itemLabel + " Item used");
        }
    }

    void ItemDespawn()
    {
        itemDespawnTimer -= Time.deltaTime;
        if (itemDespawnTimer <= 0f)
        {
            Destroy(gameObject);
            Debug.Log(itemLabel + " Item despawned");
        } 
    }

    public abstract void ItemAbility();
}
