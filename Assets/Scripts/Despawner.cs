using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour
{
    public Transform target;
    public float distance;

    private void Start()
    {
        if (target == null)
        {
            target = GameController.instance.player.transform;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) > distance)
        {
            Destroy(gameObject);
        }
    }
}
