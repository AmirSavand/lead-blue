using UnityEngine;

public class Player : MonoBehaviour
{
    public float pushForce;
    public float jumpForce;
    public float jumpDuration;
    public string targetTag;

    public ParticleSystem moveParticle;

    private Rigidbody rb;

    void Start()
    {
        // Init vars
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Is it a target
        if (other.CompareTag(targetTag))
        {
            // Destroy it
            Destroy(other.gameObject);

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

        // Go to target after a while
        Invoke("GoToTarget", jumpDuration);
    }

    /**
     * Push twards a target
     */
    public void GoToTarget()
    {
        // Find the next target
        Transform target = FindTarget();

        // Found a target
        if (target)
        {
            // Start move particle
            moveParticle.Play();

            // Reset velocity
            rb.Sleep();

            // Face target
            transform.LookAt(target);

            // Push to target
            rb.AddRelativeForce(0, 0, pushForce);
        }
    }

    public Transform FindTarget()
    {
        // Find a target
        GameObject target = GameObject.FindGameObjectWithTag(targetTag);

        // Is there a target
        if (target)
        {
            // Return it's transform
            return target.GetComponent<Transform>();
        }

        // No target
        return null;
    }
}

