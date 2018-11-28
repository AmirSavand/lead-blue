using UnityEngine;

public class Effect : MonoBehaviour
{
    public bool randomForce;
    public float randomForceMin;
    public float randomForceMax;

    public float lifetime;

    private void Start()
    {
        if (lifetime > 0)
        {
            Destroy(gameObject, lifetime);
        }

        if (randomForce)
        {
            foreach (Rigidbody piece in GetComponentsInChildren<Rigidbody>())
            {
                piece.transform.rotation = Random.rotation;
                piece.AddForce(piece.transform.forward * Random.Range(randomForceMin, randomForceMax));
            }
        }
    }
}
