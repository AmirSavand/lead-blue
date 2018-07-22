using UnityEngine;

public class Floor : MonoBehaviour
{
    [Header("Initial Move")]
    public float moveSpeed = 2;
    public float moveFromY;
    private Vector3 moveToPosition;

    [Header("Spawn Hit")]
    public bool spawnHits;
    public Hit[] hits;
    public Transform[] hitPlaces;
    public GameObject[] badHitPrefabs;
    public GameObject[] goodHitPrefabs;

    [Header("Spawn Obstacle")]
    public bool spawnObstacles;
    public GameObject[] obstaclePrefabs;

    [Header("Ref")]
    public Floor nextFloor;

    void Start()
    {
        // Store start position
        moveToPosition = transform.position;

        // Set to start position from Z axis
        transform.position = moveToPosition + new Vector3(0, moveFromY, 0);

        // Resize hit based on how many hit places we got
        System.Array.Resize(ref hits, hitPlaces.Length);

        // Spawn a random good hit in a random hit place
        Transform goodHitPlace = hitPlaces[Random.Range(0, hitPlaces.Length)];
        SpawnHit(goodHitPrefabs[Random.Range(0, goodHitPrefabs.Length)], goodHitPlace);

        {
            // Spawn a random obstacle
            Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], goodHitPlace);
        }

        // Spawn a random bad hit in a random hit place
        foreach (Transform hitPlace in hitPlaces)
        {
            // Spawn bad hit if a good hit is not in the place
            if (hitPlace != goodHitPlace)
            // Randomly spawn a random obstacle in the good hit place (if should spawn)
            if (spawnObstacles && Random.value < 0.1f)
            {
                SpawnHit(badHitPrefabs[Random.Range(0, badHitPrefabs.Length)], hitPlace);
            }
        }
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

    /**
     * Spawn a hit in a hit place
     */
    void SpawnHit(GameObject hitPrefab, Transform hitPlace)
    {
        // Can spawn hits
        if (spawnHits)
        {
            // Instantiate
            Hit hit = Instantiate(hitPrefab, hitPlace).GetComponent<Hit>();

            // Set it's floor
            hit.floor = this;

            // Store it in hits
            hits.SetValue(hit, System.Array.IndexOf(hitPlaces, hitPlace));
        }
    }
}
