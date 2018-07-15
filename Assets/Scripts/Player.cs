using UnityEngine;

public class Player : MonoBehaviour
{
    public float pushForce;
    public float jumpForce;
    public float jumpDuration;
    public float jumpMoveDelay;
    public float rollSpeed;
    public bool canMove;

    public ParticleSystem moveParticle;
    public Transform model;
    public Game game;

    private Rigidbody rb;
    private Hit lastHit;

    void Start()
    {
        // Init vars
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Player update when game running only
        if (game.gameState != GameState.Run)
        {
            return;
        }

        // Can move (is jumping)
        if (canMove)
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
        else if (moveParticle.isPlaying)
        {
            // Role it
            model.Rotate(Time.deltaTime * rollSpeed, 0, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Get other's Hit component
        Hit hit = other.GetComponentInParent<Hit>();

        // Is it a new hit target
        if (hit != null && lastHit != hit)
        {
            // Store it so we don't hit it again
            lastHit = hit;

            // Destroy it
            Destroy(other.gameObject);

            // Callback
            game.OnPlayerHit(hit);

            // Do the jump
            Jump();
        }
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
        // Find the next floor
        Floor floor = lastHit.floor.nextFloor;

        // Found a target
        if (floor)
        {
            // Start move particle
            moveParticle.Play();

            // Reset velocity
            rb.Sleep();

            // Face target
            transform.LookAt(floor.hits[index].transform);

            // Push to target
            rb.AddRelativeForce(0, 0, pushForce);

            // Can't move
            canMove = false;
        }
    }

    /**
     * Update move status (canMove)
     */
    public void UpdateMoveStatus()
    {
        canMove = true;
    }
}
