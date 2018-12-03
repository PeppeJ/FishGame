using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FishTint : MonoBehaviour
{
    protected SpriteRenderer rend;
    protected Color tint;

    protected virtual void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    protected virtual void Start()
    {
        ApplyTint();
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
