using UnityEngine;

public class Floor : MonoBehaviour
{
    [Header("Life")]
    public float age = 10;
    private float lifeTime = 0;

    [Header("Initial Move")]
    public float moveSpeed = 2;
    public float moveFromY;
    private Vector3 moveToPosition;

    [Header("Spawn Hit")]
    public Hit[] hits;
    public Transform[] hitPlaces;
    public GameObject[] badHitPrefabs;
    public GameObject[] goodHitPrefabs;

    [Header("Ref")]
    public Floor nextFloor;

    private Game game;

    void Start()
    {
        // Init vars
        game = Game.Get();

        // Store start position
        moveToPosition = transform.position;

        // Set to start position from Z axis
        transform.position = moveToPosition + new Vector3(0, moveFromY, 0);

        // Resize hit based on how many hit places we got
        System.Array.Resize(ref hits, hitPlaces.Length);

        // Spawn a random good hit in a random hit place
        Transform goodHitPlace = hitPlaces[Random.Range(0, hitPlaces.Length)];
        SpawnHit(goodHitPrefabs[Random.Range(0, goodHitPrefabs.Length)], goodHitPlace);

        // Spawn a random bad hit in a random hit place
        foreach (Transform hitPlace in hitPlaces)
        {
            // Spawn bad hit if a good hit is not in the place
            if (hitPlace != goodHitPlace)
            {
                SpawnHit(badHitPrefabs[Random.Range(0, badHitPrefabs.Length)], hitPlace);
            }
        }
    }

    void Update()
    {
        // Kill if reached age
        if (lifeTime >= age)
        {
            Destroy(gameObject);
            return;
        }

        // Increase age (if game is running)
        if (game.gameState == GameState.Run)
        {
            lifeTime += Time.deltaTime;
        }

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
        // Instantiate
        Hit hit = Instantiate(hitPrefab, hitPlace).GetComponent<Hit>();

        // Set it's floor
        hit.floor = this;

        // Store it in hits
        hits.SetValue(hit, System.Array.IndexOf(hitPlaces, hitPlace));
    }
}
