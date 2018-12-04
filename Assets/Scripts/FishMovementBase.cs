using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class FishMovementBase : MonoBehaviour
{
    protected Vector2 targetDirection;
    public float acceleration = 24f;
    public float deceleration = 64f;
    public float dashSpeed = 64f;
    public float maxSpeed = 8f;

    public float rotationSpeed = 720f;

    protected SpriteRenderer rend;
    protected Collider2D coll;
    protected Rigidbody2D rbody;

    protected Vector2 lastVelocityDirection;

    protected virtual void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        UpdateTargetDirection();
    }

    protected abstract void UpdateTargetDirection();

    protected virtual void FixedUpdate()
    {
        Move();
        RotateSprite();
    }


    protected Vector2 GetWantedVector()
    {
        return targetDirection.normalized * maxSpeed;
    }

    protected virtual void RotateSprite()
    {
        Vector2 vel = rbody.velocity;

        if (vel.magnitude > 0.1f)
        {
            lastVelocityDirection = rbody.velocity.normalized;
            rend.flipY = lastVelocityDirection.x < 0 ? true : false;
            float ang = Mathf.Atan2(lastVelocityDirection.y, lastVelocityDirection.x) * Mathf.Rad2Deg;
            rbody.MoveRotation(SmoothRotateTowards(ang));
        }
    }

    protected virtual float SmoothRotateTowards(float target)
    {
        float current = rbody.rotation;
        return Mathf.MoveTowardsAngle(current, target, rotationSpeed * Time.deltaTime);
    }

    protected virtual void Move()
    {
        rbody.velocity = rbody.velocity.magnitude > maxSpeed
            ? Vector2.MoveTowards(rbody.velocity, GetWantedVector(), deceleration * Time.deltaTime)
            : Vector2.MoveTowards(rbody.velocity, GetWantedVector(), acceleration * Time.deltaTime);
    }

}
