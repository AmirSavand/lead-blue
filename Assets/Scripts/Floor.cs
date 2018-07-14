using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public float age = 10;

    public float moveSpeed = 2;
    public float moveFromY;
    private Vector3 moveToPosition;

    public List<Hit> hits = new List<Hit>();
    public GameObject[] hitPrefabs;
    public Transform[] hitPlaces;

    public Floor nextFloor;

    void Start()
    {
        // Store start position
        moveToPosition = transform.position;

        // Set to start position from Z axis
        transform.position = moveToPosition + new Vector3(0, moveFromY, 0);

        // Spawn a random hit in a random hit place
        foreach (Transform hitPlace in hitPlaces)
        {
            GameObject hitPrefab = hitPrefabs[Random.Range(0, hitPrefabs.Length)];
            Hit hit = Instantiate(hitPrefab, hitPlace).GetComponent<Hit>();

            // Set it's floor
            hit.floor = this;

            // Store it in hits
            hits.Add(hit);
        }

        // Destroy after a while
        Destroy(gameObject, age);
    }

    void Update()
    {
        // Check if reached
        if (transform.position.y <= moveToPosition.y)
        {
            // Move up to initial position
            transform.position += new Vector3(0, Time.deltaTime * moveSpeed, 0);
        }
    }
}
