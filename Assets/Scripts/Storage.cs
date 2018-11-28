using UnityEngine;

public class Storage : MonoBehaviour
{
    public static int HighScore;

    public static bool EnableSound;
    public static bool EnableMusic;

    void Awake()
    {
        // Keep it between scenes
        DontDestroyOnLoad(gameObject);

        // Destroy it if it's a duplicate
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        // Load stats
        HighScore = PlayerPrefs.GetInt("stat-high-score");

        // Load switches (bools)
        EnableSound = PlayerPrefs.GetInt("enable-sound", 1) == 1;
        EnableMusic = PlayerPrefs.GetInt("enable-music", 1) == 1;
    }

    /**
     * Commit all data to storage.
     */
    public static void Save()
    {
        // Set stats
        PlayerPrefs.SetInt("stat-high-score", HighScore);

        // Set switches
        PlayerPrefs.SetInt("enable-sound", EnableSound ? 1 : 0);
        PlayerPrefs.SetInt("enable-music", EnableMusic ? 1 : 0);

        // Commit
        PlayerPrefs.Save();
    }
}
