using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public int score = 0;
    public float time = 60;

    public Text textScore;
    public Text textTime;

    public FloorSpawner floorSpawner;

    private void Update()
    {
        // Decrease time
        time = Mathf.Clamp(time + Time.deltaTime, 0, Time.deltaTime);

        // Update time text (UI)
        textTime.text = Mathf.Round(time).ToString();
    }

    public void OnHit(Hit hit)
    {
        // Spawn another floor
        floorSpawner.Spawn();

        // Handle score and time
        score = Mathf.Clamp(score + hit.score, 0, score + hit.score);
        time = Mathf.Clamp(time + hit.time, 0, time + hit.time);

        // Update score text (UI)
        textScore.text = score.ToString();
    }
}
