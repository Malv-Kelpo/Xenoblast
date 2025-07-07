using UnityEngine;

public class DefaultEnemy : EnemyBase
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        maxHealth = 1;
        speed = 2f;

        base.Start();
    }

}