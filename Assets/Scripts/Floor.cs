using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public GameObject hitPrefab;
    public Transform[] hitPlaces;

    void Start()
    {
        // Spawn a hit in a random hit place
        Instantiate(hitPrefab, hitPlaces[Random.Range(0, hitPlaces.Length)]);
    }
}
