using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickObject : MonoBehaviour
{   
    private int layerMask;
    public bool dialogShow = false;
    public bool descShow = true;
    private bool firstTimeEarthFlag = false; // Used to tell if its the first time visiting the EarthRoom scene
    private bool firstTimeLobbyFlag = false; // Used to tell if its the first time visiting the lobby scene
    private bool firstTimeWaterFlag = false; // Used to tell if its the first time visiting the water scene

    private void Start() {
        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = 1 << 8;
        layerMask = ~layerMask;

        // Check to see if current scene is the lobby if so show spell tree description
        // "Scene 0" name might eb changed later
        if (SceneManager.GetActiveScene().name == "EarthRoom" && firstTimeEarthFlag == false)
        {
            TipsDialog.PrintDialog("Self Introduction");
            firstTimeEarthFlag = true;
        }
        if (SceneManager.GetActiveScene().name == "scene0" && firstTimeLobbyFlag == false)
        {
            TipsDialog.PrintDialog("Lobby");
            firstTimeLobbyFlag = true;
        }
    }

    // Update is called once per frame
    public void ClickOnGround(){
        if (descShow){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask)) {
                GameObject clickObject = hitInfo.collider.gameObject;
                print(clickObject.name);
                if (clickObject.tag.CompareTo("Pickable") == 0){
                    if (clickObject.name == "Earth Key") {
                        GameObject.Find("MainUI").GetComponent<Show>().ShowBackpackIcon();
                    }
                    Backpack.backpack.GetComponent<Backpack>().AddItem(clickObject.name);
                    Sprite item = clickObject.GetComponent<SpriteRenderer>().sprite;
                    GameObject.Find("MainUI").GetComponent<FlyingSpell>().FlyTowardsIcon(item, false);
                    Destroy(clickObject);
                } else if (clickObject.name.CompareTo("Boss") == 0){
                    AIDataManager.DecideTrigram();
                }
                if (clickObject.tag == "Portals") {
                    ChangeSprite change = clickObject.GetComponent<ChangeSprite>();
                    if (!change.Trigger) {
                        change.OpenScroll();
                    }
                }

                if (TipsDialog.dialogList.ContainsKey(clickObject.name))
                {
                    TipsDialog.PrintDialog(clickObject.name);
                    dialogShow = true;
                }
            }
        }
    }
}