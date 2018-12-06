using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFish : FishMovementBase
{
    private bool doDash;
    private bool canDash = true;
    public float dashSpeed = 64f;
    public float dashCooldown = 1.4f;

    protected override void UpdateTargetDirection()
    {
        targetDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetButtonDown("Dash") && canDash)
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
        canDash = false;
        rbody.velocity = lastVelocityDirection * dashSpeed;
        StartCoroutine(DashCooldown());
    }

    public float dashReadyPercent = 1f;
    private IEnumerator DashCooldown()
    {
        float tick = 0;
        while (tick < dashCooldown)
        {
            tick += Time.deltaTime;
            dashReadyPercent = Mathf.Clamp01(tick / dashCooldown);
            yield return null;
        }
        canDash = true;
    }
}
