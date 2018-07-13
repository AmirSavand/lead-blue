using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

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
    public Text textScoreFinal;
    public Text textTime;

    public GameObject uiMenu;
    public GameObject uiRun;
    public GameObject uiPause;
    public GameObject uiOver;

    public Cam cam;
    public Player player;
    public FloorSpawner floorSpawner;

    void Update()
    {
        // Game running
        if (gameState == GameState.Run)
        {
            // Decrease time
            time = Mathf.Clamp(time - Time.deltaTime, 0, time);

            // Update time text (UI)
            textTime.text = Mathf.Round(time).ToString();

            // Ran out of time
            if (time == 0)
            {
                // Game over
                SetGameState(GameState.Over);

                // Destroy player
                Destroy(player.gameObject);

                // Face camera up
                cam.faceUp = true;

                // Set final score text
                textScoreFinal.text = "+" + score;
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
        textScore.text = score.ToString();
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

        SetGameState(GameState.Run);
    }

    /**
     * Pause game
     */
    public void Pause()
    {
        // Stop gameplay
        Time.timeScale = 0;

        SetGameState(GameState.Pause);
    }
}
