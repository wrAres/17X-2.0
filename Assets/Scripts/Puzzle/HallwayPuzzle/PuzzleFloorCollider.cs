using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleFloorCollider : MonoBehaviour
{
    public FloorControl bounds;

    private void OnTriggerEnter(Collider obj)
    {
        bounds.Push(name);
    }

    private void OnTriggerExit(Collider obj)
    {
        bounds.Delete(name);
        bounds.CheckIfRestart(obj.gameObject);
    }
}
