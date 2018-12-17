using UnityEngine;

public class FloorSpawner : MonoBehaviour
{
    public int spawnInitial = 3;
    public GameObject floorPrefab;

    private int spawnCounter;
    private Floor lastFloor;

    void Start()
    {
        // Initial spawn
        for (int i = 0; i < spawnInitial; i++)
        {
            Spawn();
        }
    }

    /**
     * Spawn a floor
     */
    public void Spawn()
    {
        Vector3 spawnPosition = new Vector3();
        Quaternion spawnRotaiton = Quaternion.identity;

        // This is the first floor
        if (lastFloor != null)
        {
            // Get spawn position and rotation of next floor transform of last floor
            spawnPosition = lastFloor.nextFloorTransform.position;
            spawnRotaiton = lastFloor.nextFloorTransform.rotation;
        }

        // Spawn the floor
        Floor floor = Instantiate(floorPrefab, spawnPosition, spawnRotaiton).GetComponent<Floor>();

        // Set name of floor with ID
        floor.transform.name = "Floor " + spawnCounter;

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
        spawnCounter++;
    }
}
