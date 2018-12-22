using UnityEngine;

public class PlatformSpecific : MonoBehaviour
{
    public bool desktop;
    public bool mobile;
    public bool console;

    void Awake()
    {
        /**
         * This game object is not available for mobile.
         * Destroy if running on mobile platform.
         */
        if (!mobile && Application.isMobilePlatform)
        {
            Destroy(gameObject);
        }

        /**
         * This game object is not available for desktop.
         * Destroy if not running on mobile or console platform.
         */
        if (!desktop && !Application.isMobilePlatform && !Application.isConsolePlatform)
        {
            Destroy(gameObject);
        }

        /**
         * This game object is not available for console.
         * Destroy if running on console platform.
         */
        if (!console && Application.isConsolePlatform)
        {
            Destroy(gameObject);
        }
    }
}
