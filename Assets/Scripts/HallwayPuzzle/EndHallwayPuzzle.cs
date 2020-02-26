using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndHallwayPuzzle : MonoBehaviour
{
    public FloorControl bounds;

    private void OnTriggerEnter(Collider obj)
    {
        bounds.start = false;
        bounds.end = true;
    }
}
