using UnityEngine;

public class Edge : MonoBehaviour
{
    private Game game;

    void Start()
    {
        // Get inits
        game = Game.Get();
    }

    void OnTriggerExit(Collider other)
    {
        // Destroy if game is running
        if (game.gameState == GameState.Run)
        {
            // If other has a parent rb
            if (other.GetComponentInParent<Rigidbody>())
            {
                // Destroy the parent
                Destroy(other.GetComponentInParent<Rigidbody>().gameObject);
            }
            // Destroy self
            else
            {
                Destroy(other.gameObject);
            }
        }
    }
}
