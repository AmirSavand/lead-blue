using UnityEngine;

public class Game : MonoBehaviour
{
    public int score = 0;

    public FloorSpawner floorSpawner;

    public void OnHit(Hit hit)
    {
        // Spawn another floor
        floorSpawner.Spawn();
    }
}
