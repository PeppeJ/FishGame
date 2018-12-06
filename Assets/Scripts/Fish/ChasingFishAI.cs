using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingFishAI : FishMovementBase
{
    public float chaseRange = 4f;
    public float aggroRange = 6f;

    public bool chasing;

    private GameObject player;

    protected override void Awake()
    {
        base.Awake();
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
    }

    protected override void UpdateTargetDirection()
    {
        if (chasing)
        {
            if (Vector2.Distance(player.transform.position, transform.position) > chaseRange)
            {
                chasing = false;
            }
            Vector2 deltaPos = player.transform.position - transform.position;
            targetDirection = deltaPos.normalized;
        }
        else
        {
            if (Vector2.Distance(player.transform.position, transform.position) < aggroRange)
            {
                chasing = true;
            }
            targetDirection = Vector2.left;
        }
    }
}
