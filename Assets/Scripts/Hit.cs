using UnityEngine;

public class Hit : MonoBehaviour
{
    public string hitName;
    public int score;
    public float time;

    public float rotateSpeed;

    public Floor floor;

    void Update()
    {
        // Rotate
        transform.Rotate(0, Time.deltaTime * rotateSpeed, 0);
    }
}
