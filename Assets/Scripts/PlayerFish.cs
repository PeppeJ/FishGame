using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFish : FishMovementBase
{
    private bool doDash;

    protected override void UpdateTargetDirection()
    {
        targetDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetButtonDown("Dash"))
        {
            doDash = true;
        }
    }

    protected override void Update()
    {
        base.Update();
        Debug();
    }

    private void Debug()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {

        }
    }

    protected override void Move()
    {
        base.Move();
        if (doDash)
        {
            Dash();
        }
        rbody.angularVelocity = 0f;
    }

    protected virtual void Dash()
    {
        doDash = false;
        rbody.velocity = Vector2.up * dashSpeed;
    }
}
