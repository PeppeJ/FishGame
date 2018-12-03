using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserCamera : MonoBehaviour
{
    public Transform target;

    public enum ChaseMode { Linear, Square, Cubic, Exponential }
    public ChaseMode chase;
    public float speed = 1f;

    private new Camera camera;
    private Vector3 offset;

    public Vector3 CameraPosition
    {
        get { return transform.position - offset; }
        set { transform.position = value + offset; }
    }

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Start()
    {
        offset = new Vector3(0, 0, transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null)
        {
            float dist = Vector3.Distance(CameraPosition, target.position);
            CameraPosition = Vector3.MoveTowards(CameraPosition, target.position, DistanceValue(dist, chase));
        }
    }

    private float DistanceValue(float distance, ChaseMode mode)
    {
        switch (mode)
        {
            case ChaseMode.Linear:
                return speed * Time.deltaTime;
            case ChaseMode.Square:
                return Mathf.Pow(distance, 2) * Time.deltaTime;
            case ChaseMode.Cubic:
                return Mathf.Pow(distance, 3) * Time.deltaTime;
            case ChaseMode.Exponential:
                return Mathf.Pow(2, distance) * Time.deltaTime;
            default:
                return 0;
        }
    }
}
