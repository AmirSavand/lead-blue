using UnityEngine;

public class Hit : MonoBehaviour
{
    public string hitName;
    public int score;
    public float time;

    public float rotateSpeed;
    public bool destroyOnKill;

    public bool killCollector;

    public Floor floor;

    private bool hitPlayer;

    void Update()
    {
        // Rotate
        if (rotateSpeed > 0)
        {
            transform.Rotate(0, Time.deltaTime * rotateSpeed, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Already hit player
        if (hitPlayer) return;

        // Get player
        Player player = other.GetComponentInParent<Player>();

        // Hit the player
        if (player)
        {
            // Store the hit
            hitPlayer = true;

            // Not trigger for dead player
            if (player.isDead) return;

            // This hit is a killer
            if (killCollector)
            {
                player.Kill();
                return;
            }

            // Callback
            player.OnHit(this);

            // Should destroy self on hit
            if (destroyOnKill)
            {
                Destroy(gameObject);
            }
        }
    }
}
