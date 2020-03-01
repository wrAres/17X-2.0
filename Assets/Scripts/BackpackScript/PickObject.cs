using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObject : MonoBehaviour
{   
    private int layerMask;
    private GameObject[] pickableList;

    private void Start() {
        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = 1 << 8;
        layerMask = ~layerMask;
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask)) {
                GameObject clickObject = hitInfo.collider.gameObject;
                if (clickObject.tag.CompareTo("Pickable") == 0){
                    Backpack.backpack.GetComponent<Backpack>().AddItem(clickObject.name);
                    Destroy(clickObject);
                }
            }
        }
    }
}