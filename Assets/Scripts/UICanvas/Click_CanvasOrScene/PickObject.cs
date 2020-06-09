using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickObject : MonoBehaviour
{
    public bool canAct => !dialogShown && !talismanShown && !shineIcon && !FindObjectOfType<Show>().lockGame;
    public bool dialogShown =>
        FindObjectOfType<TipsDialog>() != null;
    public bool talismanShown =>
        GameObject.FindGameObjectWithTag("Talisman") != null;
        
    public bool shineIcon =>
        GameObject.Find("DarkBackground").GetComponent<LeaveIconBright>().shine == true;

    private int layerMask;
    public bool dialogShow = false;
    public bool descShow = true;
    public int distanceToClick;
    public float cameraDistance = 0;
    private void Start() {
        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = 1 << 8;
        layerMask = ~layerMask;

        // Check to see if current scene is the lobby if so show spell tree description
        // "Scene 0" name might eb changed later
        if (SceneManager.GetActiveScene().name == "EarthRoom")
        {
            Item.getGroundNames();
        }
        else if (SceneManager.GetActiveScene().name == "scene0"){
            GameObject.Find("Main UI").GetComponent<Show>().ToggleLock(false);
            GameObject.Find("EarthSoundManager").GetComponents<AudioSource>()[2].volume = 0.3f;
            Item.getGroundNames();
            EarthSoundManager.StopPlaySound();
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

            Item.fireLevel(DontDestroyVariables.fireLevel, GameObject.Find("法阵-scene2").transform.position);
        }
        else if (SceneManager.GetActiveScene().name == "scene3") {
            GameObject.Find("Main UI").GetComponent<Show>().ToggleLock(false);
            Item.getGroundNames();
            DontDestroyVariables.enterWaterRoom = true;
            Item.flowerpot = GameObject.Find("Flowerpot");
            GameObject.Find("EarthSoundManager").GetComponents<AudioSource>()[2].volume = 0f;
        } 
    }

    // Update is called once per frame
    public void ClickOnGround(){
        // Mathf.Infinity
        if (descShow){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // print("distance " + (distanceToClick + cameraDistance));
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, (distanceToClick + cameraDistance), layerMask) && canAct) {
                GameObject clickObject = hitInfo.collider.gameObject;
                if (clickObject.tag == "Pickable"){
                    if (clickObject.name.CompareTo("Cold Fire Seed") == 0) {
                        Item.s.UnlockElement(TalisDrag.Elements.FIRE);
                        Destroy(clickObject);
                        DontDestroyVariables.fireLevel = 0;
                    }
                    else if (Backpack.backpack.GetComponent<Backpack>().CanAddItem()) {
                        //Backpack.backpack.GetComponent<Backpack>().AddItem(clickObject.name);
                        Sprite item = clickObject.GetComponent<SpriteRenderer>().sprite;
                        GameObject.Find("MainUI").GetComponent<FlyingSpell>().FlyTowardsIcon(item, false, clickObject.name);
                        GameObject.Find("pickupEffect").GetComponent<pickupEffect>().castAni(clickObject.transform.position);
                        Destroy(clickObject);
                    }
                } else if (clickObject.name.CompareTo("Flower 1") == 0 || clickObject.name.CompareTo("Flower 2") == 0 || clickObject.name.CompareTo("Flower 3") == 0 || clickObject.name.CompareTo("Flower 4") == 0 || clickObject.name.CompareTo("Flower 5") == 0 || clickObject.name.CompareTo("Flower 6") == 0) {  
                    clickObject.GetComponent<flowerInMirror>().ClickMirror();
                }
                if (clickObject.tag == "Portals") {
                    ChangeSprite change = clickObject.GetComponent<ChangeSprite>();
                    if (clickObject.name.CompareTo("EarthPortal") == 0) {
                        if (change.Trigger) {
                            if (clickObject.GetComponent<sceneTransition>().enterable)
                                TipsDialog.PrintDialog(clickObject.name + " Open");
                            else
                                TipsDialog.PrintDialog(clickObject.name + " Wait Key");
                        }
                    } else {
                        if (change.Trigger) {
                            TipsDialog.PrintDialog(clickObject.name + " Open");
                        }
                    }
                    if (!change.Trigger) {
                        change.OpenScroll();
                        TipsDialog.PrintDialog(clickObject.name);
                    } 
                } else if (clickObject.name.CompareTo("Flowerpot") == 0) {
                    if (DontDestroyVariables.growState < 3) {
                        TipsDialog.PrintDialog("Flowerpot");
                    } else if (DontDestroyVariables.growState < 4) {
                        TipsDialog.PrintDialog("Flower Need Sun");
                    }
                }
                else if (TipsDialog.dialogList.Contains(clickObject.name)){
                    // print("click on " + clickObject.name);
                    TipsDialog.PrintDialog(clickObject.name);
                    dialogShow = true;
                }
                UISoundScript.PlayPick();
            }
        }
        descShow = false;
    }
}