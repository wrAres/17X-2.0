using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Show))]
[RequireComponent(typeof(SpellTreeManager))]
public class TalismanManager : MonoBehaviour {
    /*
    [System.Serializable]
    public class Recipe {
        public string itemName;
        public TalisDrag.Elements[] element = new TalisDrag.Elements[3];
    }
    */
    // Main display variables
    public GameObject display;
    public float timer;
    public GameObject prefab;
    // Element variables
    public GameObject[] elements;
    public Vector3[] elePos;

    private float curTime;
    // Cookbook variables
    private TalisDrag.Elements[] craft = new TalisDrag.Elements[3];
    public Image[] slots;
    private Spell[] recipeBook;

    public Backpack backpack;
    private Show dispManager;

    private void Awake() {
        dispManager = GetComponent<Show>();
        ResetCraft();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey("space") && curTime <= 0) {
            recipeBook = GetComponent<SpellTreeManager>().GetSpellBook();
            display.SetActive(true);
            dispManager.CloseDisplays();
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
            slots[i].enabled = false;
        }
    }

    // Check if recipe can be made from current craft
    private bool CheckRecipe(Spell r) {
        TalisDrag.Elements[] curCraft = new TalisDrag.Elements[3];
        System.Array.Copy(craft, curCraft, 3);
        for (int i = 0; i < r.recipe.Length; i++) {
            bool gotEle = false;
            for (int j = 0; j < craft.Length; j++) {
                if (r.recipe[i] == curCraft[j]) {
                    gotEle = true;
                    curCraft[j] = TalisDrag.Elements.NONE;
                    break;
                }
            }

            if (!gotEle && r.recipe[i] != TalisDrag.Elements.NONE) {
                return false;
            }
        }

        // Don't make empty elements
        if (r.recipe.Length <= 0) {
            return false;
        }
        return true;
    }

    // Functions to be called by other scripts
     

    // Close the entire talisman display
    public void CloseDisplay() {
        display.SetActive(false);
        ResetCraft();
        curTime = 0;
    }

    // Add an element to the craft log
    public void AddCraft(TalisDrag.Elements e, Sprite s) {
        for (int i = 0; i < craft.Length; i++) {
            if(craft[i] == TalisDrag.Elements.NONE){
                craft[i] = e;
                AIDataManager.IncrementElementAccess(e);
                slots[i].enabled = true;
                slots[i].sprite = s;
                break;
            }
        }
        bool madeItem = false;
        // Check if item can be made
        for (int i = 0; i < recipeBook.Length; i++) {
            if (CheckRecipe(recipeBook[i])) {
                madeItem = true;
                Debug.Log("MADE ITEM: " + recipeBook[i].spellName);
                // Add to backpack if it's not an element
                if (recipeBook[i].element == TalisDrag.Elements.NONE) {
                    backpack.AddItem(recipeBook[i].spellName);
                    AIDataManager.IncrementSpellAccess(recipeBook[i].spellName);
                    GameObject.Find("Backpack_Icon").GetComponent<ShakingIcon>().ShakeMe();
                }
                else {
                    AIDataManager.DiscoverNewSpell(Time.time - AIDataManager.previousUnlockTime);
		            AIDataManager.previousUnlockTime = Time.time;

                    GetComponent<SpellTreeManager>().UnlockElement(recipeBook[i].element);
                    GameObject.Find("SpellTreeIcon").GetComponent<ShakingIcon>().ShakeMe();
                }
                CloseDisplay();
            }
        }
        if(!madeItem)
            AIDataManager.TryNonExistentRecipe();
    }

    public void UnlockElement(TalisDrag.Elements e) {
        for (int i = 0; i < elements.Length; i++) {
            TalisDrag ele = elements[i].GetComponent<TalisDrag>();
            if (ele.element == e) {
                ele.locked = false;
                ele.known = false;
                break;
            }
        }
    }

}
