﻿using UnityEngine;

public class Player : MonoBehaviour
{
    public string targetTag;
    public float pushForce;
    public float jumpForce;
    public float jumpDuration;

    public ParticleSystem moveParticle;
    public Game game;

    private bool canMove = true;

    private Rigidbody rb;
    private Collider lastHit;

    void Start()
    {
        // Init vars
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Press up key and can move
        if (Input.GetKeyUp(KeyCode.Return) && canMove)
        {
            // Go
            GoToTarget();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Is it a new hit target
        if (other.CompareTag(targetTag) && lastHit != other)
        {
            // Store it so we don't hit it again
            lastHit = other;

            // Destroy it
            Destroy(other.gameObject);

            // Callback
            game.OnHit();

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

        // Can move
        canMove = true;
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

            // Can't move
            canMove = false;
        }
    }

    /**
     * Find the next target and return its transform
     */
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
