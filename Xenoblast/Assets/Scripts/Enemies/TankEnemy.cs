using UnityEngine;

public class TankEnemy : EnemyBase
{
    // ==============================================================
    // Slower Speed of 1.5f, Increased Health of 3
    // ==============================================================

    protected override void Awake()
    {
        enemyLabel = "Tank";
        base.Awake();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        maxHealth = 3;
        speed = 1.5f;

        base.Start();
    }
}
