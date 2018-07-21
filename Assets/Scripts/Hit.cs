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

    void Update()
    {
        // Rotate
        transform.Rotate(0, Time.deltaTime * rotateSpeed, 0);
    }

    /**
     * Propper destroy
     */
    public void Kill()
    {
        if (destroyOnKill)
        {
            Destroy(gameObject);
        }
    }
}
