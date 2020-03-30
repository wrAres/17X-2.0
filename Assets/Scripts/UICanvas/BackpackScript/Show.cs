using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TalismanManager))]
public class Show : MonoBehaviour
{
    public GameObject spellTreeDisp;
    private TalismanManager talisDisp;
    private bool earthUnlocked;
    private int closedFirstTimeFlag = 0; // Used to mark when to pop up talisman description text
    public bool clickedObject = false;
    private bool brightBackpack = false;
    private bool brightSpell = false;

    GraphicRaycaster raycaster;
    PointerEventData pointerData;
    EventSystem eventSystem;

    private static Show showInstance;
    void Awake() {
        DontDestroyOnLoad(this);

        if (showInstance == null) {
            showInstance = this;
        }
        else {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        GameObject.FindGameObjectWithTag("BackpackIcon").GetComponent<Image>().enabled = false;
        GameObject.FindGameObjectWithTag("SpellTreeIcon").GetComponent<Image>().enabled = false;
    }

    private void Start() {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();

        talisDisp = GetComponent<TalismanManager>();
    }

    // public Backpack bk;
    private void Update() {
        //Check if the left Mouse button is clicked
        if (raycaster == null) {

        }
        else if (Input.GetMouseButtonDown(0))
        {
            //Set up the new Pointer Event
            pointerData = new PointerEventData(eventSystem);
            pointerData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            // print("raycaster");
            // print(raycaster);
            //Raycast using the Graphics Raycaster and mouse click position
            raycaster.Raycast(pointerData, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                // Debug.Log("Hit " + result.gameObject.tag);

                if (result.gameObject.tag.CompareTo("BackpackIcon") == 0) {
                    if (brightBackpack) {
                        GameObject.Find("DarkBackground").GetComponent<LeaveIconBright>().DarkBackpack();
                        brightBackpack = false;
                    }
                    if (Backpack.backpack.activeSelf) {
                        Backpack.backpack.GetComponent<Backpack>().Show(false);
                    }
                    else {
                        Backpack.backpack.GetComponent<Backpack>().Show(true);
                        GetComponent<FlyingSpell>().ResetFlyingSpell();
                    }
                    // Close other canvas
                    spellTreeDisp.SetActive(false);
                    talisDisp.CloseDisplay();
                }
                else if (result.gameObject.tag.CompareTo("Item") == 0) {
                    if (!ItemDragHandler.holdItem) {
                        ItemDragHandler.itemOnGround = result.gameObject;
                        ItemDragHandler.previousPosition = result.gameObject.GetComponent<RectTransform>().anchoredPosition;
                        ItemDragHandler.holdItem = true;
                    }
                }
                else if (result.gameObject.tag.CompareTo("SpellTreeIcon") == 0) {
                    if (brightSpell) {
                        GameObject.Find("DarkBackground").GetComponent<LeaveIconBright>().DarkBackpack();
                        TipsDialog.PrintDialog("Spelltree 2");
                        brightSpell = false;
                    }
                    spellTreeDisp.SetActive(!spellTreeDisp.activeSelf);
             
                    // closedFirstTimeFlag++;
                    // print(closedFirstTimeFlag);

                    // Close other canvas
                    talisDisp.CloseDisplay();
                    Backpack.backpack.GetComponent<Backpack>().Show(false);
                }
                else if (result.gameObject.name.CompareTo("Next Button") == 0) {
                    bool textActive = TipsDialog.NextPage();
                    print("text act" + textActive);
                    GameObject.Find("Dialog Box").SetActive(textActive);

                }
            }
        }
        // Show talisman building description after closing the spell tree
        // if (closedFirstTimeFlag == 2)
        // {
        //     TipsDialog.PrintDialog("Talisman");
        //     clickedObject = true;
        //     closedFirstTimeFlag = 3;
        // }
        // else
        // {
        //     clickedObject = false;
        // }
    }

    private void PrintName(GameObject obj) {
        print("Debug" + obj.name);
    }

    public void CloseDisplays() {
        Backpack.backpack.GetComponent<Backpack>().Show(false);
        spellTreeDisp.SetActive(false);
    }

    public void ShowBackpackIcon() { 
        GameObject.FindGameObjectWithTag("BackpackIcon").GetComponent<Image>().enabled = true; 
        GameObject.Find("DarkBackground").GetComponent<LeaveIconBright>().ShineBackpack();
        brightBackpack = true;
    }
    public void ShowSpelltreeIcon() { 
        GameObject.FindGameObjectWithTag("SpellTreeIcon").GetComponent<Image>().enabled = true; 
        GameObject.Find("DarkBackground").GetComponent<LeaveIconBright>().ShineSpellIcon();
        brightSpell = true;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "scene0" && !earthUnlocked) {
            earthUnlocked = true;
            GetComponent<SpellTreeManager>().UnlockElement(TalisDrag.Elements.EARTH);
        }
    }
}