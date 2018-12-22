using UnityEngine;

public class Hit : MonoBehaviour
{
    public string hitName;
    public int score;
    public float time;

    public float rotateSpeed;
    public bool destroyOnKill;

    public bool killCollector;

    public float activateRigidbodiesAfterTime;
    public Rigidbody[] rigidbodiesToActivate;

    public Floor floor;
    public AudioSource sound;

    private bool hitPlayer;

    void Start()
    {
        // If should activate rbs after a while
        if (activateRigidbodiesAfterTime > 0)
        {
            // Invoke activation
            Invoke("ActivateRigidbodies", activateRigidbodiesAfterTime);
        }
    }

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
        if (hitPlayer)
            return;

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

            // Detach sound, play it then destory it
            sound.transform.SetParent(null);
            sound.Play();
            Destroy(sound.gameObject, sound.clip.length);

            // Should destroy self on hit
            if (destroyOnKill)
            {
                Destroy(gameObject);
            }
        }
    }

    public void ActivateRigidbodies()
    {
        foreach (Rigidbody rb in rigidbodiesToActivate)
        {
            rb.isKinematic = false;
        }
    }
}
