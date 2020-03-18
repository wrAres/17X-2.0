using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObject : MonoBehaviour
{   
    private int layerMask;

    private bool dialogShow = false;

    private void Start() {
        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = 1 << 8;
        layerMask = ~layerMask;
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetMouseButtonDown(0)) {
            if (!dialogShow) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask)) {
                    GameObject clickObject = hitInfo.collider.gameObject;
                    print(clickObject.name);
                    if (clickObject.tag.CompareTo("Pickable") == 0){
                        Backpack.backpack.GetComponent<Backpack>().AddItem(clickObject.name);
                        Sprite item = clickObject.GetComponent<SpriteRenderer>().sprite;
                        GameObject.Find("MainUI").GetComponent<FlyingSpell>().FlyTowardsIcon(item, false);
                        Destroy(clickObject);
                    } else if (clickObject.name.CompareTo("Boss") == 0){
                        AIDataManager.DecideTrigram();
                    }
                    TipsDialog.PrintDialog(clickObject.name);
                    dialogShow = true;
                }
            } else {
                TipsDialog.HideTextBox();
                dialogShow = false;
            }
        }
    }
}