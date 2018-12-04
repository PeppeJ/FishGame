using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject spawnObject;
    public float timeBetweenSpawn = 1f;
    
    // Use this for initialization
    void Start()
    {
        StartCoroutine(Spawn(spawnObject, timeBetweenSpawn));
    }

    public IEnumerator Spawn(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(obj, GetRandomSpawnPosition(), Quaternion.identity);
        StartCoroutine(Spawn(spawnObject, time));
    }

    public Vector2 GetRandomSpawnPosition()
    {
        Vector2 vec = Vector2.zero;

        Vector3 offset = new Vector3(-0.2f, Random.value);

        vec = Camera.main.ViewportToWorldPoint(offset);

        print(vec);

        return vec;
    }
}
