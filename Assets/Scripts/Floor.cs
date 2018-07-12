using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public float age = 10;

    public float moveSpeed = 2;
    public float startFromY;
    private Vector3 startPosition;

    public GameObject hitPrefab;
    public Transform[] hitPlaces;

    void Start()
    {
        // Store start position
        startPosition = transform.position;

        // Set to start position from Z axis
        transform.position = startPosition + new Vector3(0, startFromY, 0);

        // Spawn a hit in a random hit place
        Instantiate(hitPrefab, hitPlaces[Random.Range(0, hitPlaces.Length)]);

        // Destroy after a while
        Destroy(gameObject, age);
    }

    void Update()
    {
        // Smoothly move to position
        transform.position = Vector3.Lerp(transform.position, startPosition, Time.deltaTime * moveSpeed);
    }
}
