using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalismanManager : MonoBehaviour {
    [System.Serializable]
    public class Recipe {
        public string itemName;
        public TalisDrag.Elements[] element = new TalisDrag.Elements[3];
    }
    // Main display variables
    public GameObject display;
    public float timer;
    public GameObject prefab, player;
    // Element variables
    public GameObject[] elements;
    public Vector3[] elePos;

    private float curTime;
    // Cookbook variables
    private TalisDrag.Elements[] craft = new TalisDrag.Elements[3];
    public Recipe[] recipeBook;

    public Backpack backpack;

    // Update is called once per frame
    void Update() {
        if (Input.GetKey("space") && curTime <= 0) {
            display.SetActive(true);
            DisplaySpellList();
            curTime = timer;
        }
        if (!display.activeSelf && curTime > 0) {
            curTime = 0;
        }

        if (curTime > 0) {
            curTime -= Time.deltaTime;
        }
        else if (curTime <= 0 && display.activeSelf) {
            CloseDisplay();
        }
    }

    // Display the list of currently usable elements
    private void DisplaySpellList() {
        int curPos = 0;
        for (int i = 0; i < elements.Length; i++) {
            TalisDrag ele = elements[i].GetComponent<TalisDrag>();
            // Show element if it's unlocked
            if (!ele.locked && !ele.known) {
                elements[i].SetActive(true);
                elements[i].GetComponent<RectTransform>().localPosition =
                    elePos[curPos];
                elements[i].GetComponent<TalisDrag>().UpdateOrigin(elePos[curPos]);
                curPos += 1;
            }
            else {
                elements[i].SetActive(false);
            }
        }
    }

    // Reset the craft log
    private void ResetCraft() {
        for (int i = 0; i < craft.Length; i++) {
            craft[i] = TalisDrag.Elements.NONE;
        }
    }

    // Close the entire talisman display
    private void CloseDisplay() {
        display.SetActive(false);
        ResetCraft();
        curTime = 0;
    }

    // Check if recipe can be made from current craft
    private bool CheckRecipe(Recipe r) {
        TalisDrag.Elements[] curCraft = new TalisDrag.Elements[3];
        System.Array.Copy(craft, curCraft, 3);
        for (int i = 0; i < r.element.Length; i++) {
            bool gotEle = false;
            for (int j = 0; j < craft.Length; j++) {
                if (r.element[i] == curCraft[j]) {
                    gotEle = true;
                    curCraft[j] = TalisDrag.Elements.NONE;
                    break;
                }
            }

            if (!gotEle && r.element[i] != TalisDrag.Elements.NONE) {
                return false;
            }
        }
        return true;
    }

    /*
     * Functions to be called by other scripts
     */

    // Add an element to the craft log
    public void AddCraft(TalisDrag.Elements e) {
        for (int i = 0; i < craft.Length; i++) {
            if(craft[i] == TalisDrag.Elements.NONE){
                craft[i] = e;
                break;
            }
        }

        // Check if item can be made
        for (int i = 0; i < recipeBook.Length; i++) {
            if (CheckRecipe(recipeBook[i])) {
                Debug.Log("MADE ITEM: " + recipeBook[i].itemName);
                backpack.AddItem(recipeBook[i].itemName);
                CloseDisplay();
            }
        }
    }

}
