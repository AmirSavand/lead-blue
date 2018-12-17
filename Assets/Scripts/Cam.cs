using UnityEngine;

public class Cam : MonoBehaviour
{
    [Header("Target Movement")]
    public Transform target;
    public float smoothTime = 0.1f;
    public float distance = 10f;

    [Header("Face Movement")]
    public float faceUp = 30;
    public float faceUpSpeed = 5;

    [Header("Side Look")]
    public float sideRotation = 3;
    public float sideMovement = 3;
    public float sideRotationSpeed = 1;
    public float sideMovementSpeed = 1;
    public float sideLookFactor;

    private Game game;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        // Init vars
        game = Game.Get();
    }

    void FixedUpdate()
    {
        // There's a target
        if (target)
        {
            // Game is running
            if (game.gameState == GameState.Run)
            {
                // Handle target movement (follow on x axis only)
                Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, target.position.z - distance);
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            }

            // Handle side movement (on x axis only)
            Vector3 toPosition = transform.position;
            toPosition.x = sideMovement * sideLookFactor * -1;
            transform.position = Vector3.Slerp(transform.position, toPosition, Time.deltaTime * sideMovementSpeed);

            // Handle side rotation (on y axis only)
            Quaternion toRotation = Quaternion.Euler(
                x: transform.rotation.eulerAngles.x,
                y: sideRotation * sideLookFactor,
                z: transform.rotation.eulerAngles.z
            );
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * sideMovementSpeed);
        }

        // Game is over
        if (game.gameState == GameState.Over)
        {
            // Face up smoothly
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-faceUp, 0, 0), Time.deltaTime * faceUpSpeed);
        }
    }
}
