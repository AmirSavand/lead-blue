using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public float age = 10;

    public float moveSpeed = 2;
    public float startFromY;
    private Vector3 startPosition;

    public List<Hit> hits = new List<Hit>();
    public GameObject[] hitPrefabs;
    public Transform[] hitPlaces;

    public Floor nextFloor;

    void Start()
    {
        // Store start position
        startPosition = transform.position;

        // Set to start position from Z axis
        transform.position = startPosition + new Vector3(0, startFromY, 0);

        // Spawn a random hit in a random hit place
        foreach (Transform hitPlace in hitPlaces)
        {
            GameObject hitPrefab = hitPrefabs[Random.Range(0, hitPlaces.Length)];
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
        // Smoothly move to position
        transform.position = Vector3.Lerp(transform.position, startPosition, Time.deltaTime * moveSpeed);
    }
}
