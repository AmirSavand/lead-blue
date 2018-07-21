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
        if (isDead)
        {
            return;
        }

        // Can move (is jumping)
        if (canPush)
        {
            int hitIndex = -1;

            // Press right key
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                hitIndex = 0;
            }

            // Press up key
            else if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                hitIndex = 1;
            }

            // Press left key
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                hitIndex = 2;
            }

            // Press any key
            if (hitIndex != -1)
            {
                GoToTarget(hitIndex);
            }

            // If there's any velocity
            if (rb.velocity != Vector3.zero)
            {
                // Role the ball (slow)
                model.Rotate(Time.deltaTime * rollSpeed / 10, 0, 0);
            }
        }

        // Going to a hit
        else if (moveParticle.isPlaying && model != null)
        {
            // Role it
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
        // Find the hit of the floor
        Hit hit = currentFloor.nextFloor.hits[index];

        // Found the hit
        if (hit)
        {
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
        // Set to dead
        isDead = true;

        // Destroy rigidbody
        Destroy(rb);

        // Destroy model
        Destroy(model.gameObject);

        // Destroy player after particle
        Destroy(gameObject, moveParticle.main.duration);

        // Create death effect
        GameObject deathEffect = Instantiate(deathEffectPrefab, transform.position, transform.rotation);

        // Destroy death effect after effect
        Destroy(deathEffect, deathEffect.GetComponent<ParticleSystem>().main.duration);
    }
}
