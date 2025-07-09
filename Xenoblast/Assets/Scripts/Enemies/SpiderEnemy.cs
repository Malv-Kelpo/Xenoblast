using UnityEngine;

public class SpiderEnemy : EnemyBase
{
    [Header("Movement Settings")]
    [SerializeField] private float zigZagDuration = 0.75f;
    [SerializeField] private float zigZagAmount = 1f;
    private float zigZagTimer = 0f;
    private int zigZagDirection = 1;

    protected override void Awake()
    {
        enemyLabel = "Spider";
        base.Awake();
    }
    protected override void Start()
    {
        maxHealth = 1;
        speed = 4f;
    }

    protected override void EnemyMovement()
    {
        if (player)
        {
            // Zig zag movement toward the player
            Vector3 direction = (player.transform.position - transform.position).normalized;
            Vector3 perpendicular = new Vector3(direction.y, -direction.x, 0);


            zigZagTimer -= Time.deltaTime;
            if (zigZagTimer <= 0f)
            {
                zigZagTimer = zigZagDuration;
                zigZagDirection = -zigZagDirection;
            }

            // If the player has iFrames, reverse the direction
            if(player.iFrameActive)
            {
                direction = -direction;
            }

            Vector3 zigzagOffset = perpendicular * zigZagAmount * zigZagDirection;
            Vector3 finalDirection = (direction + zigzagOffset).normalized;

            moveDirection = finalDirection;
        }
    }
    
}
