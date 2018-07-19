using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Rigidbody>())
        {
            Destroy(other.GetComponentInParent<Rigidbody>().gameObject);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
