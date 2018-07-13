using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState
{
    Menu,
    Run,
    Pause,
    Over
}

public class Game : MonoBehaviour
{
    public GameState gameState = GameState.Menu;

    public int score = 0;
    public float time = 60;

    public Text textScore;
    public Text textTime;

    public GameObject uiMenu;
    public GameObject uiRun;
    public GameObject uiPause;
    public GameObject uiOver;

    public Cam cam;
    public Player player;
    public FloorSpawner floorSpawner;

    private void Start()
    {
        // Setup state UIs
        UpdateStateUis();
    }

    void Update()
    {
        // Decrease time
        time = Mathf.Clamp(time - Time.deltaTime, 0, time);

        // Update time text (UI)
        textTime.text = Mathf.Round(time).ToString();

        // Ran out of time (when running)
        if (time == 0 && gameState == GameState.Run)
        {
            // Game over
            gameState = GameState.Over;

            // Face camera up
            cam.faceUp = true;

            // Destroy player
            Destroy(player.gameObject);

            // Update UI as well
            UpdateStateUis();
        }
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

    /**
     * Update state of all UIs based on game state
     */
    public void UpdateStateUis()
    {
        uiMenu.SetActive(gameState == GameState.Menu);
        uiRun.SetActive(gameState == GameState.Run);
        uiPause.SetActive(gameState == GameState.Pause);
        uiOver.SetActive(gameState == GameState.Over);
    }

    /**
     * Restart game (scene)
     */
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
