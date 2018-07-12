using UnityEngine;

public class FloorSpawner : MonoBehaviour
{
    public GameObject floorPrefab;

    public float nextSpawnDistance;
    public float nextSpawnDistanceFactor = 18;

    private Floor lastFloor;

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
        Floor floor = Instantiate(floorPrefab).GetComponent<Floor>();

        // Move it next to the last floor
        floor.transform.position = new Vector3(0, 0, nextSpawnDistance);

        // Increase the distance for next spawn
        nextSpawnDistance += nextSpawnDistanceFactor;

        // Set this floor as next floor to last floor
        if (lastFloor)
        {
            lastFloor.nextFloor = floor;
        }

        // Store this floor as last floor
        lastFloor = floor;
    }
}
