using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    public bool isDead;

    [Header("Push")]
    public float pushForce;
    public float pushFaceUp;
    public bool canPush;

    [Header("Jump")]
    public float jumpForce;
    public float jumpMoveDelay;

    [Header("Roll")]
    public float rollSpeed;

    [Header("Ref")]
    public ParticleSystem moveParticle;
    public Transform model;
    public AudioSource moveSound;
    public AudioSource deathSound;
    public GameObject deathEffectPrefab;

    private Game game;
    private Rigidbody rb;
    private Floor currentFloor;

    void Start()
    {
        // Init vars
        game = Game.Get();
        rb = GetComponent<Rigidbody>();

        // Find first floor to start
        currentFloor = GameObject.Find("Floor 0").GetComponent<Floor>();
    }

    void Update()
    {
        // No update for dead player
        if (isDead) return;

        // Game is running
        if (game.gameState == GameState.Run)
        {
            // Press right key
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                GoToTarget(0);
            }

            // Press up key
            else if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                GoToTarget(1);
            }

            // Press left key
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                GoToTarget(2);
            }
        }


        // If there's velocity
        if (rb.velocity != Vector3.zero)
        {
            // Role the ball
            model.Rotate(Time.deltaTime * rollSpeed, 0, 0);
        }
    }

    /**
     * Called when player hits a hit
     */
    public void OnHit(Hit hit)
    {
        // Store the floor
        currentFloor = hit.floor;

        // Game callback
        game.OnPlayerHit(hit);

        // Jump up
        Jump();
    }

    /**
     * Jump up
     */
    public void Jump()
    {
        // Stop move particle
        moveParticle.Stop();

        // Reset velocity
        rb.Sleep();

        // Push up
        rb.AddForce(Vector3.up * jumpForce);

        // Can move after a while
        Invoke("UpdateMoveStatus", jumpMoveDelay);
    }

    /**
     * Push twards a target
     */
    public void GoToTarget(int index)
    {
        // Check if can move and is not dead
        if (!canPush || isDead) return;

        // Find the hit of the floor
        Hit hit = currentFloor.nextFloor.hits[index];

        // Found the hit
        if (hit)
        {
            // Callback
            game.OnPlayerMove(index);

            // Start move particle
            moveParticle.Play();

            // Reset velocity
            rb.Sleep();

            // Face hit but upper a bit
            transform.LookAt(hit.transform);
            transform.Rotate(pushFaceUp, 0, 0);

            // Push to hit
            rb.AddRelativeForce(0, 0, pushForce);

            // Can't move
            canPush = false;

            // Store the floor
            currentFloor = currentFloor.nextFloor;

            // Sound
            moveSound.Play();
        }
    }

    /**
     * Update move status (canMove)
     */
    public void UpdateMoveStatus()
    {
        canPush = true;
    }

    /**
     * Properly destroy and kill with effects
     */
    public void Kill()
    {
        // Can't kill what's already dead
        if (isDead) return;

        // Set to dead
        isDead = true;

        // Destroy rigidbody
        Destroy(rb);

        // Destroy model
        Destroy(model.gameObject);

        // Destroy player after particle
        Destroy(gameObject, moveParticle.main.duration);

        // Create death effect
        Instantiate(deathEffectPrefab, transform.position, transform.rotation);

        // Death sound
        deathSound.Play();
    }
}
