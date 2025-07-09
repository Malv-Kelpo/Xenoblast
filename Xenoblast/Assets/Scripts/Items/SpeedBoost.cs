using UnityEngine;

public class SpeedBoost : ItemBase
{
    protected override void Awake()
    {
        itemLabel = "Speed Boost";
        itemDuration = 15f;
        base.Awake();
    }
    public override void ItemAbility()
    {
        PlayerController playerScript = player.GetComponent<PlayerController>();
        playerScript.SetCurrentSpeed(8f);
    }
}
