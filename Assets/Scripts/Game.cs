using UnityEngine;

public class Game : MonoBehaviour
{
    public int score = 0;
    public float time = 60;

    public FloorSpawner floorSpawner;

    private void Update()
    {
        // Decrease time
        time -= Time.deltaTime;
    }

    public void OnHit(Hit hit)
    {
        // Spawn another floor
        floorSpawner.Spawn();

        // Handle score and time
        score += hit.score;
        time += hit.time;
    }
}
