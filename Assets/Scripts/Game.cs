using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public FloorSpawner floorSpawner;

    public void OnHit()
    {
        floorSpawner.Spawn();
    }
}
