using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickObject : MonoBehaviour
{
    public bool canAct => !dialogShown && !talismanShown;
    public bool dialogShown =>
        FindObjectOfType<TipsDialog>() != null;
    public bool talismanShown =>
        GameObject.FindGameObjectWithTag("Talisman") != null;

    private int layerMask;
    public bool dialogShow = false;
    public bool descShow = true;
    private void Start() {
        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = 1 << 8;
        layerMask = ~layerMask;

        // Check to see if current scene is the lobby if so show spell tree description
        // "Scene 0" name might eb changed later
        if (SceneManager.GetActiveScene().name == "EarthRoom")
        {
            TipsDialog.PrintDialog("Self Introduction");
        }
        else if (SceneManager.GetActiveScene().name == "scene0"){
            if (DontDestroyVariables.firstTimeLobbyFlag){
                TipsDialog.PrintDialog("Lobby");
                DontDestroyVariables.firstTimeLobbyFlag = false; // Used to tell if its the first time visiting the lobby scene
            } else {
                Destroy(GameObject.Find("River Tip"));
                GameObject river = GameObject.Find("River");
                // river.GetComponent<BoxCollider>().size = new Vector3(0, 0, 0);
                // river .GetComponent<Renderer>().material.color = Color.gray;
                Destroy(river);
                Destroy(GameObject.Find("River Sound 1"));
                Destroy(GameObject.Find("River Sound 2"));
                Destroy(GameObject.Find("River Sound 3"));
            }
        } else if (SceneManager.GetActiveScene().name == "scene3") {
            DontDestroyVariables.enterWaterRoom = true;
            Item.flowerpot = GameObject.Find("Flowerpot");
        }
    }

    // Update is called once per frame
    public void ClickOnGround(){
        if (descShow){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask) && canAct) {
                GameObject clickObject = hitInfo.collider.gameObject;
                // print(clickObject.name);
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
                } else if (clickObject.name.CompareTo("Flower 1") == 0 || clickObject.name.CompareTo("Flower 2") == 0 || clickObject.name.CompareTo("Flower 3") == 0 || clickObject.name.CompareTo("Flower 4") == 0 || clickObject.name.CompareTo("Flower 5") == 0 || clickObject.name.CompareTo("Flower 6") == 0) {  
                    clickObject.GetComponent<flowerInMirror>().ClickMirror();
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