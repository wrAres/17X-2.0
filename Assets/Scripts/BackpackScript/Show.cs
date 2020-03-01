using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Show : MonoBehaviour
{
    public GameObject spellTreeDisp;

    GraphicRaycaster raycaster;
    PointerEventData pointerData;
    EventSystem eventSystem;

    private void Start() {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
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
                    }
                }
                else if (result.gameObject.tag.CompareTo("Item") == 0) {
                    ItemDragHandler.itemOnGround = result.gameObject;
                    ItemDragHandler.holdItem = true;
                    ItemDragHandler.previousPosition = result.gameObject.GetComponent<RectTransform>().anchoredPosition;
                }
                else if (result.gameObject.tag.CompareTo("SpellTreeIcon") == 0) {
                    spellTreeDisp.SetActive(!spellTreeDisp.activeSelf);
                }
            }
        }
    }

    private void PrintName(GameObject obj) {
        print("Debug" + obj.name);
    }
}