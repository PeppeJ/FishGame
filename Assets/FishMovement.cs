using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class FishMovement : MonoBehaviour
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

    protected Color tint;

    protected virtual void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        rbody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    protected virtual void Start()
    {
        ApplyTint();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        targetDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Move();
        RotateSprite();
    }

    protected virtual Vector2 GetWantedVector()
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
            transform.rotation = SmoothRotateTo(Quaternion.Euler(0, 0, ang));
        }
    }

    private Quaternion SmoothRotateTo(Quaternion target)
    {

        Quaternion current = transform.rotation;

        return Quaternion.RotateTowards(current, target, rotationSpeed * Time.deltaTime);

    }

    protected virtual void Move()
    {
        if (rbody.velocity.magnitude > maxSpeed)
        {
            rbody.velocity = Vector2.MoveTowards(rbody.velocity, GetWantedVector(), deceleration * Time.deltaTime);
        }
        else
        {
            rbody.velocity = Vector2.MoveTowards(rbody.velocity, GetWantedVector(), acceleration * Time.deltaTime);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            rbody.velocity = lastVelocityDirection * dashSpeed;
        }
    }

    protected virtual void ApplyTint()
    {
        rend.color = GetTint();
    }

    protected virtual Color GetTint()
    {
        return Random.ColorHSV(0, 1, 0, 0.2f, 0.8f, 1f, 1f, 1f);
    }
}
