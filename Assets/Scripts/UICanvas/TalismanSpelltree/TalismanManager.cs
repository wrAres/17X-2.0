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
        CloseDisplay();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("space") && !display.activeSelf) {
            recipeBook = GetComponent<SpellTreeManager>().GetSpellBook();
            display.SetActive(true);
            dispManager.CloseDisplays();
            // Close any text box that is open
            TipsDialog.HideTextBox();
            DisplaySpellList();
            curTime = timer;
        }
        else if (Input.GetKeyDown("space")) {
            CloseDisplay();
        }

        // TEST MAKE BUTTON
        if (Input.GetKey(KeyCode.Return) && display.activeSelf) {
            MakeItem();
        }
    }

    // Display the list of currently usable elements
    private void DisplaySpellList() {
        int curPos = 0;
        int curPos2 = 5;
        for (int i = 0; i < elements.Length; i++) {
            TalisDrag ele = elements[i].GetComponent<TalisDrag>();
            // Show element if it's unlocked
            if (!ele.locked && !ele.known) {
                elements[i].SetActive(true);
                if (ele.isLevelTwo()) {
                    elements[i].GetComponent<RectTransform>().localPosition =
                        elePos[curPos2];
                    elements[i].GetComponent<TalisDrag>().UpdateOrigin(elePos[curPos2]);
                    curPos2 += 1;
                }
                else {
                    elements[i].GetComponent<RectTransform>().localPosition =
                        elePos[curPos];
                    elements[i].GetComponent<TalisDrag>().UpdateOrigin(elePos[curPos]);
                    curPos += 1;
                }
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
        TalisDrag.Elements[] curCraft = new TalisDrag.Elements[craft.Length];
        System.Array.Copy(craft, curCraft, craft.Length);

        for (int i = 0; i < curCraft.Length; i++) {
            if (curCraft[i] != TalisDrag.Elements.NONE) {
                if (r.recipe.Length < i+1) return false;
            }
        }

        // Make sure each element is matched
        for (int i = 0; i < r.recipe.Length; i++) {
            bool gotEle = false;
            for (int j = 0; j < craft.Length; j++) {
                if (r.recipe[i] == curCraft[j]) {
                    gotEle = true;
                    curCraft[j] = TalisDrag.Elements.NONE;
                    break;
                }
            }
            if (!gotEle) { return false; }
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
        AIDataManager.IncrementElementAccess(e);
        for (int i = 0; i < craft.Length; i++) {
            if(craft[i] == TalisDrag.Elements.NONE){
                craft[i] = e;
                slots[i].enabled = true;
                slots[i].sprite = s;
                break;
            }
        }
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

    private bool MakeItem() {
        // Check if item can be made
        for (int i = 0; i < recipeBook.Length; i++) {
            if (CheckRecipe(recipeBook[i])) {
                // Add to backpack if it's not an element
                if (recipeBook[i].element == TalisDrag.Elements.NONE) {
                    backpack.AddItem(recipeBook[i].spellName);
                    AIDataManager.IncrementSpellAccess(recipeBook[i].spellName);
                    // GameObject.Find("Backpack_Icon").GetComponent<ShakingIcon>().ShakeMe();
                    GetComponent<FlyingSpell>().FlyTowardsIcon(recipeBook[i].GetComponent<Image>().sprite, false);
                    if (recipeBook[i].curState == Spell.SpellState.KNOWN) {
                        recipeBook[i].ChangeState(Spell.SpellState.UNLOCKED);
                    }
                }
                else {
                    AIDataManager.DiscoverNewSpell(Time.time - AIDataManager.previousUnlockTime);
		            AIDataManager.previousUnlockTime = Time.time;

                    GetComponent<SpellTreeManager>().UnlockElement(recipeBook[i].element);
                    //GetComponent<FlyingSpell>().FlyTowardsIcon(recipeBook[i].GetComponent<Image>().sprite, true);
                }
                CloseDisplay();
                return true;
            }
        }

        // Failed to make item
        AIDataManager.TryNonExistentRecipe();
        Debug.Log("No items can be made");
        CloseDisplay();
        return false;

    }

}
