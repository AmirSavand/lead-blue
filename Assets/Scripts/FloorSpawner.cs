using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : MonoBehaviour
{
    public GameObject floorPrefab;

    public float nextSpawnDistance;
    public float nextSpawnDistanceFactor = 18;

    void Start()
    {
        // Spawn 3 floors right away
        Spawn();
        Spawn();
        Spawn();
    }

    public void Spawn()
    {
        // Spawn the floor
        GameObject floor = Instantiate(floorPrefab);

        // Move it next to the last floor
        floor.transform.position = new Vector3(0, 0, nextSpawnDistance);

        // Increase the distance for next spawn
        nextSpawnDistance += nextSpawnDistanceFactor;
    }
}
