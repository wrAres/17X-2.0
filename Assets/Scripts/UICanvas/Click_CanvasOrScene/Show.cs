using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TalismanManager))]
public class Show : MonoBehaviour
{
    public bool canAct => !dialogShown;
    public bool dialogShown =>
        FindObjectOfType<TipsDialog>() != null;
    // public bool talismanShown =>
    //     GameObject.FindGameObjectWithTag("Talisman") != null;

    public GameObject spellTreeDisp;
    private TalismanManager talisDisp;
    private bool earthUnlocked;
    // private int closedFirstTimeFlag = 0; // Used to mark when to pop up talisman description text
    public bool clickedObject = false;
    public bool brightBackpack = false;
      public bool brightTalisman = false;
    private bool brightSpell = false;
    private bool backpackUnlocked, spellTreeUnlocked, talismanUnlocked;
    public bool seenSpellTree = false;
    public Sprite talismanIcon;
    PickObject pick;
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
        Destroy(GameObject.Find("Theme Song"));
    }

    private void Start() {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
        pick = GameObject.FindWithTag("Ground").GetComponent<PickObject>();
        talisDisp = GetComponent<TalismanManager>();
    }

    // public Backpack bk;
    private void Update() {
        if (talisDisp.display.activeSelf || spellTreeDisp.activeSelf) {
            pick.descShow = false;
        } else {
            pick.descShow = true;
        }
        //Check if the left Mouse button is clicked
        if (raycaster == null) {

        }
        else if (Input.GetMouseButtonDown(0)) {
            //Set up the new Pointer Event
            pointerData = new PointerEventData(eventSystem);
            pointerData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            // print("raycaster");
            // print(raycaster);
            //Raycast using the Graphics Raycaster and mouse click position
            raycaster.Raycast(pointerData, results);

            int resultSize = 0;

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results) {
                resultSize += 1;
                string name = result.gameObject.name;
                string tag = result.gameObject.tag;
                // print("obj names: " + name);

                // if (tag.CompareTo("BackpackIcon") == 0 && canAct) {
                //     pick.descShow = false;
                //     if (brightBackpack) {
                //         GameObject.Find("DarkBackground").GetComponent<LeaveIconBright>().DarkBackpack();
                //         brightBackpack = false;

                //         GameObject spellTree = GameObject.FindGameObjectWithTag("BackpackIcon");
                //         spellTree.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChangeAsset/backpack_icon");
                //     }
                //     if (Backpack.backpack.activeSelf) {
                //         Backpack.backpack.GetComponent<Backpack>().Show(false);
                //     }
                //     else {
                //         Backpack.backpack.GetComponent<Backpack>().Show(true);
                //         GetComponent<FlyingSpell>().ResetFlyingSpell();
                //     }
                //     // Close other canvas
                //     spellTreeDisp.SetActive(false);
                //     talisDisp.CloseDisplay();
                // }
                
                if (tag.CompareTo("TalismanIcon") == 0 && canAct) {
                    pick.descShow = false;
                    GameObject.Find("MainUI").GetComponent<TalismanManager>().OpenTalisman();
                    
                    // Close other canvas
                    // spellTreeDisp.SetActive(false);
                    // Backpack.backpack.GetComponent<Backpack>().Show(false);
                }
                else if (tag.CompareTo("Item") == 0 && canAct) {
                    pick.descShow = false;
                    if (!ItemDragHandler.holdItem) {
                        ItemDragHandler.itemOnGround = result.gameObject;
                        ItemDragHandler.previousPosition = result.gameObject.GetComponent<RectTransform>().anchoredPosition;
                        ItemDragHandler.holdItem = true;
                        ItemDragHandler.x = ItemDragHandler.previousPosition.x;
                        RectTransform item_transform = ItemDragHandler.itemOnGround.GetComponent<RectTransform>();
                        ItemDragHandler.originalSize = item_transform.sizeDelta;
                    }
                }
                else if (tag.CompareTo("SpellTreeIcon") == 0 && canAct) {
                    pick.descShow = false;
                    UISoundScript.OpenSpellTree();
                    if (brightSpell) {
                        GameObject.Find("DarkBackground").GetComponent<LeaveIconBright>().DarkBackpack();
                        TipsDialog.PrintDialog("Spelltree 2");
                        brightSpell = false;
                        GameObject spellTree = GameObject.FindGameObjectWithTag("SpellTreeIcon");
                        spellTree.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChangeAsset/All elements");
                        // spellTree.transform.localScale = new Vector2(2.6f, 2.5f);
                        seenSpellTree = true;
                    }
                    spellTreeDisp.SetActive(!spellTreeDisp.activeSelf);
                    Backpack.backpack.GetComponent<Backpack>().Show(!spellTreeDisp.activeSelf);

                    // Close other canvas
                    talisDisp.CloseDisplay();

                    // closedFirstTimeFlag++;
                    // print(closedFirstTimeFlag);
                }
                else if (name.CompareTo("Next Button") == 0) {
                    pick.descShow = false;
                    TipsDialog.ChangeNextButton();
                    if (TipsDialog.isTyping){ // type full text
                        TipsDialog.PrintFullDialog();
                    } else {
                        bool textActive = TipsDialog.NextPage();
                        bool CalledScene = TipsDialog.CallScene();
                        // print("text act" + textActive);
                        GameObject.Find("Dialog Box").SetActive(textActive);
                        // check for water boss-->credits scene
                        if (!textActive && CalledScene) {
                            Invoke("ToLoadScene", 5);
                            print("Active Credits Scene in 5 secs");
                        }
                    }
                }
            }
            if (resultSize == 0)
                pick.ClickOnGround();
        }
        else if (Input.GetMouseButtonDown(1)) {
            //Set up the new Pointer Event
            pointerData = new PointerEventData(eventSystem);
            pointerData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            // print("raycaster");
            // print(raycaster);
            //Raycast using the Graphics Raycaster and mouse click position
            raycaster.Raycast(pointerData, results);

            int resultSize = 0;

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results) {
                resultSize += 1;
                string name = result.gameObject.name;
                string tag = result.gameObject.tag;

                if (tag.CompareTo("Item") == 0 && canAct) {
                    // print("obj names: " + name);
                    pick.descShow = false;
                    if (!ItemDragHandler.holdItem) {
                        int position = ((int)result.gameObject.GetComponent<RectTransform>().anchoredPosition.x + 680) / 80;
                        Backpack.backpack.GetComponent<Backpack>().RemoveItem(result.gameObject, position);
                        break;
                    }
                }
            }
        }
        // else if (Input.GetKeyDown(KeyCode.B) && canAct) {
        //     pick.descShow = false;
        //     if (brightBackpack) {
        //         GameObject.Find("DarkBackground").GetComponent<LeaveIconBright>().DarkBackpack();
        //         brightBackpack = false;

        //         GameObject spellTree = GameObject.FindGameObjectWithTag("BackpackIcon");
        //         spellTree.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChangeAsset/backpack_icon");
        //     }
        //     if (Backpack.backpack.activeSelf) {
        //         Backpack.backpack.GetComponent<Backpack>().Show(false);
        //     }
        //     else {
        //         Backpack.backpack.GetComponent<Backpack>().Show(true);
        //         GetComponent<FlyingSpell>().ResetFlyingSpell();
        //     }
        //     // Close other canvas
        //     spellTreeDisp.SetActive(false);
        //     talisDisp.CloseDisplay();
        // }
        else if (Input.GetKeyDown(KeyCode.Q) && canAct && spellTreeUnlocked) {
            pick.descShow = false;
            UISoundScript.OpenSpellTree();
            if (brightSpell) {
                GameObject.Find("DarkBackground").GetComponent<LeaveIconBright>().DarkBackpack();
                TipsDialog.PrintDialog("Spelltree 2");
                brightSpell = false;
                GameObject spellTree = GameObject.FindGameObjectWithTag("SpellTreeIcon");
                spellTree.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChangeAsset/All elements");
                // spellTree.transform.localScale = new Vector2(2.6f, 2.5f);
                seenSpellTree = true;
            }
            spellTreeDisp.SetActive(!spellTreeDisp.activeSelf);
            Backpack.backpack.GetComponent<Backpack>().Show(!spellTreeDisp.activeSelf);

            // Close other canvas
            talisDisp.CloseDisplay();
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
        backpackUnlocked = true;
    }
    public void ShowTalismanIcon() { 
        GameObject.FindGameObjectWithTag("TalismanIcon").GetComponent<Image>().enabled = true; 
        GameObject.Find("DarkBackground").GetComponent<LeaveIconBright>().ShineTalisman();
        brightTalisman = true;
        talismanUnlocked = true;
    }

    public void ShowSpelltreeIcon() { 
        GameObject.FindGameObjectWithTag("SpellTreeIcon").GetComponent<Image>().enabled = true; 
        GameObject.Find("DarkBackground").GetComponent<LeaveIconBright>().ShineSpellIcon();
        brightSpell = true;
        spellTreeUnlocked = true;

        if (SceneManager.GetActiveScene().name == "scene0" && !earthUnlocked) {
            earthUnlocked = true;
            GetComponent<SpellTreeManager>().UnlockElement(TalisDrag.Elements.EARTH);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "scene0" && !earthUnlocked
            && GameObject.FindGameObjectWithTag("SpellTreeIcon").GetComponent<Image>().enabled) {
            earthUnlocked = true;
            GetComponent<SpellTreeManager>().UnlockElement(TalisDrag.Elements.EARTH);
        }
    }

    public void ToggleIcons(bool isOn) {
        if (!isOn || backpackUnlocked) GameObject.FindGameObjectWithTag("BackpackIcon").GetComponent<Image>().enabled = isOn;
        if (!isOn || spellTreeUnlocked) GameObject.FindGameObjectWithTag("SpellTreeIcon").GetComponent<Image>().enabled = isOn;
        // if (!isOn || talismanUnlocked) GameObject.FindGameObjectWithTag("TalismanIcon").GetComponent<Image>().enabled = isOn;

        if (!isOn) CloseDisplays();
        else Backpack.backpack.GetComponent<Backpack>().Show(true);
    }

    void ToLoadScene(){
        SceneManager.LoadScene("Credits");
    }
}