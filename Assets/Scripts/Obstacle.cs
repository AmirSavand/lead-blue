using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public bool killer;

    void OnTriggerEnter(Collider other)
    {
        if (killer)
        {
            Player player = other.GetComponentInParent<Player>();

            if (player)
            {
                player.Kill();
            }
        }
    }
}
