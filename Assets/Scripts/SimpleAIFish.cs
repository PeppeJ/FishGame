using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAIFish : FishMovementBase
{
    protected enum MoveDirection { Left, Right }
    protected MoveDirection move = MoveDirection.Right;

    protected override void UpdateTargetDirection()
    {
        if (move == MoveDirection.Right)
        {
            targetDirection = Vector2.right;
        }
        else
        {
            targetDirection = Vector2.left;
        }
    }
}
