using UnityEngine;

public class FloorSpawner : MonoBehaviour
{
    public GameObject floorPrefab;

    public int initialSpawn = 3;

    public float nextSpawnDistance;
    public float nextSpawnDistanceFactor = 18;

    private int floorCounter;
    private Floor lastFloor;

    void Start()
    {
        // Initial spawn
        for (int i = 0; i < initialSpawn; i++)
        {
            Spawn();
        }
    }

    /**
     * Spawn a floor
     */
    public void Spawn()
    {
        // Spawn the floor
        Floor floor = Instantiate(floorPrefab).GetComponent<Floor>();

        // Move it next to the last floor
        floor.transform.position = new Vector3(0, 0, nextSpawnDistance);

        // Increase the distance for next spawn
        nextSpawnDistance += nextSpawnDistanceFactor;

        // Set name of floor with ID
        floor.transform.name = "Floor " + floorCounter;

        // This is not the first floor
        if (lastFloor)
        {
            // Store it as last floor
            lastFloor.nextFloor = floor;
        }

        // This is the first floor
        else
        {
            // No hit spawning for first floor
            floor.spawnHits = false;

            // No obstacle spawning for the first floor
            floor.spawnObstacles = false;
        }

        // Store this floor as last floor
        lastFloor = floor;

        // Increase floor counter
        floorCounter++;
    }
}
