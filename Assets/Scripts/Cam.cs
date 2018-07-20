using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform target;

    public float smoothTime = 0.1f;
    public float distance = 10f;
    public float faceUp = 30;
    public float faceUpSpeed = 5;

    private Game game;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        // Init vars
        game = Game.Get();
    }

    void FixedUpdate()
    {
        // Follow if there's a target and game is running
        if (target != null && game.gameState == GameState.Run)
        {
            // Position to smoothly move to (only on Z axis)
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, target.position.z - distance);

            // Smoothly move the camera
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }

        // Face up if game is over
        else if (game.gameState == GameState.Over)
        {
            // Face up smoothly
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-faceUp, 0, 0), Time.deltaTime * faceUpSpeed);
        }
    }
}
