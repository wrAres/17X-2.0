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
    }

    private void Start() {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();

        talisDisp = GetComponent<TalismanManager>();

        GameObject.FindGameObjectWithTag("BackpackIcon").GetComponent<Image>().enabled = false;
        GameObject.FindGameObjectWithTag("SpellTreeIcon").GetComponent<Image>().enabled = false;
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
                    spellTreeDisp.SetActive(!spellTreeDisp.activeSelf);
                    // Close other canvas
                    talisDisp.CloseDisplay();
                    Backpack.backpack.GetComponent<Backpack>().Show(false);
                }
            }
        }
    }

    private void PrintName(GameObject obj) {
        print("Debug" + obj.name);
    }

    public void CloseDisplays() {
        Backpack.backpack.GetComponent<Backpack>().Show(false);
        spellTreeDisp.SetActive(false);
    }

    public void ShowBackpackIcon() { GameObject.FindGameObjectWithTag("BackpackIcon").GetComponent<Image>().enabled = true; }
    public void ShowSpelltreeIcon() { GameObject.FindGameObjectWithTag("SpellTreeIcon").GetComponent<Image>().enabled = true; }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "scene0" && !earthUnlocked) {
            earthUnlocked = true;
            GetComponent<SpellTreeManager>().UnlockElement(TalisDrag.Elements.EARTH);
        }
    }
}