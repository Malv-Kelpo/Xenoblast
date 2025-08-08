using UnityEngine;

public class ExplosiveGun : ItemBase
{
    [SerializeField] private GameObject explosiveBulletPrefab;

    protected override void Awake()
    {
        itemLabel = "Explosive Gun";
        itemAbilityDuration = 15f;
        base.Awake();
    }

    public override void ItemAbility()
    {
        // Changes Player bulletPrefab to the explosiveBulletPrefab
        PlayerController playerScript = player.GetComponent<PlayerController>();
        playerScript.setBulletPrefab(explosiveBulletPrefab);
    }
}
