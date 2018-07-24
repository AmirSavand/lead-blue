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
    [Header("Game")]
    public GameState gameState = GameState.Menu;
    public int score = 0;
    public float time = 60;
    private float initialTime;

    [Header("Game UIs")]
    public Text scoreText;
    public Text finalScoreText;
    public Slider timeSlider;

    [Header("State UIs")]
    public GameObject uiMenu;
    public GameObject uiRun;
    public GameObject uiPause;
    public GameObject uiOver;

    [Header("Platform UIs")]
    public GameObject mobileUI;

    [Header("Ref")]
    public Cam cam;
    public Player player;
    public FloorSpawner floorSpawner;
    public GameObject[] editorOnlyGameObjects;

    void Awake()
    {
        // Destroy all editor only game objects
        foreach (GameObject editorOnlyGameObject in editorOnlyGameObjects)
        {
            Destroy(editorOnlyGameObject);
        }

        // Not android
        if (Application.platform != RuntimePlatform.Android)
        {
            // Destroy mobile UI
            Destroy(mobileUI);
        }
    }

    void Start()
    {
        // Store initial time
        initialTime = time;

        // Reset time
        Time.timeScale = 1;
    }

    void Update()
    {
        // Game running
        if (gameState == GameState.Run)
        {
            // Decrease time
            time = Mathf.Clamp(time - Time.deltaTime, 0, time);

            // Update time text (UI)
            timeSlider.value = time / (initialTime * 1.5f);

            // Ran out of time or player is dead
            if (time == 0 || player == null)
            {
                // Game over
                SetGameState(GameState.Over);

                // Destroy player
                if (player)
                {
                    player.Kill();
                }

                // Set final score text
                finalScoreText.text = "+" + score;
            }
        }

        // Pressed pause/back button
        if (Input.GetButtonUp("Cancel"))
        {
            // Game is runningm pause it
            if (gameState == GameState.Run)
            {
                Pause();
            }

            // Game is paused, resume it
            else if (gameState == GameState.Pause)
            {
                Resume();
            }
        }
    }

    /**
     * Find the Game instance and return it
     */
    public static Game Get()
    {
        return GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
    }

    /**
     * Callback when player hits a hit
     */
    public void OnPlayerHit(Hit hit)
    {
        // Spawn another floor
        floorSpawner.Spawn();

        // Handle score and time
        score = Mathf.Clamp(score + hit.score, 0, score + hit.score);
        time = Mathf.Clamp(time + hit.time, 0, time + hit.time);

        // Update score text (UI)
        scoreText.text = score.ToString();
    }

    /**
     * Callback when player moves
     */
    public void OnPlayerMove(int index)
    {
        // Set the camer side movement factor based on player move index
        // Moving right
        if (index == 0)
        {
            cam.sideLookFactor = -1;
        }
        // Moving to middle
        else if (index == 1)
        {
            cam.sideLookFactor = 0;
        }
        // Moving to left
        else if (index == 2)
        {
            cam.sideLookFactor = 1;
        }
    }

    /**
     * Set game state and update state UIs
     */
    public void SetGameState(GameState state)
    {
        // Update game state
        gameState = state;

        // Update game UIs
        uiMenu.SetActive(gameState == GameState.Menu);
        uiRun.SetActive(gameState == GameState.Run);
        uiPause.SetActive(gameState == GameState.Pause);
        uiOver.SetActive(gameState == GameState.Over);
    }

    /**
     * Go to main menu
     */
    public void Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /**
     * Resume game
     */
    public void Resume()
    {
        // Resume gameplay
        Time.timeScale = 1;

        // Update game state
        SetGameState(GameState.Run);
    }

    /**
     * Pause game
     */
    public void Pause()
    {
        // Stop gameplay
        Time.timeScale = 0;

        // Update game state
        SetGameState(GameState.Pause);
    }
}
