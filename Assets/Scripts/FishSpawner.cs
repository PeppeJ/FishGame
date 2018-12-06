using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject[] spawnObject;

    public Collider2D spawnZone;

    public float initialSpawnDelay = 0f;
    public float repeatTime = 1f;

    public bool repeat = true;
    private bool lastRepeat = false;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(DelayedStart());
        CheckRepeat();
    }

    private IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(initialSpawnDelay);
        StartCoroutine(Spawn(spawnObject, repeatTime));
    }

    public virtual void Update()
    {
        CheckRepeat();
    }

    private void CheckRepeat()
    {
        if (lastRepeat != repeat)
        {
            StartCoroutine(Spawn(spawnObject, repeatTime));
        }
        lastRepeat = repeat;
    }

    public IEnumerator Spawn(GameObject[] obj, float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(obj[Random.Range(0, obj.Length)], GetRandomSpawnPosition(), Quaternion.identity);
        if (repeat)
        {
            StartCoroutine(Spawn(spawnObject, time));
        }
    }

    public Vector2 GetRandomSpawnPosition()
    {
        Vector2 point;
        do
        {
            point = new Vector2(Random.Range(spawnZone.bounds.min.x, spawnZone.bounds.max.x), Random.Range(spawnZone.bounds.min.y, spawnZone.bounds.max.y));
        } while (!spawnZone.OverlapPoint(point));

        return point;
    }
}
