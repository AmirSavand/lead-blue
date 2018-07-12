using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : MonoBehaviour
{
    public GameObject floorPrefab;

    public float nextSpawnDistance;

    void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        GameObject floor = Instantiate(floorPrefab);

        floor.transform.position = new Vector3(0, 0, nextSpawnDistance);

        nextSpawnDistance += 18;
    }
}
