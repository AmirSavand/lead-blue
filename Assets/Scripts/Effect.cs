using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public ParticleSystem particle;

    void Start()
    {
        if (particle)
        {
            Destroy(gameObject, particle.main.duration);
        }
    }
}
