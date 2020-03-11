using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TalismanManager))]
public class SpellTreeManager : MonoBehaviour {
    /*
    [System.Serializable]
    public class Spell {
        public GameObject icon;
        public TalisDrag.Elements element;
        public bool locked;
    }
    */
    private Spell[] spell;
    public GameObject display;

    // Textbox display
    public GameObject textBox;
    public Text spellName, recipe, desc;

    // Start is called before the first frame update
    void Start() {
        //   UpdateIcons();
        display.SetActive(true);
        spell = GetComponentsInChildren<Spell>();
        display.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        /*
        if (Input.GetKey(KeyCode.S)) {
            display.SetActive(true);
        }
        else {
            display.SetActive(false);
        }
        */

        if (Input.GetKeyDown(KeyCode.Q)) {
            UnlockElement(TalisDrag.Elements.FIRE);
            Debug.Log("should work");
        }
    }

    private void UpdateIcons() {
        for (int i = 0; i < spell.Length; i++) {
            if (spell[i].curState == Spell.SpellState.LOCKED) {
                Image img = spell[i].gameObject.GetComponent<Image>();
                var tempColor = img.color;
                tempColor.a = .5f;
                img.color = tempColor;
            }
        }
    }

    private string EleToString(TalisDrag.Elements e) {
        switch (e) {
            case TalisDrag.Elements.EARTH: return "earth";
            case TalisDrag.Elements.FIRE: return "fire";
            case TalisDrag.Elements.METAL: return "metal";
            case TalisDrag.Elements.MOON: return "moon";
            case TalisDrag.Elements.SUN: return "sun";
            case TalisDrag.Elements.THUNDER: return "thunder";
            case TalisDrag.Elements.WATER: return "water";
            case TalisDrag.Elements.WIND: return "wind";
            case TalisDrag.Elements.WOOD: return "wood";
        }
        return "";
    }

    private bool CanCraft(Spell r) {
        for (int i = 0; i < r.recipe.Length; i++) {
            bool gotEle = false;
            for (int j = 0; j < spell.Length; j++) {
                if (spell[j].curState == Spell.SpellState.UNLOCKED) {
                    if (r.recipe[i] == spell[j].element) {
                        gotEle = true;
                        break;
                    }
                }
            }

            if (!gotEle && r.recipe[i] != TalisDrag.Elements.NONE) {
                return false;
            }
        }
        if (r.recipe.Length <= 0) return false;
        return true;
    }

    // Functions to be called by other scripts
    public void UpdateTextBox(Spell s) {
        textBox.SetActive(true);
        if (s.curState == Spell.SpellState.LOCKED) {
            spellName.text = "? ? ?";
            recipe.text = "Recipe: ? ? ?";
            desc.text = "? ? ?";
        }
        else {
            spellName.text = s.spellName;

            if (s.curState == Spell.SpellState.UNLOCKED) {
                desc.text = s.unlockedDes;
                recipe.text = "Recipe: ";
                for (int i = 0; i < s.recipe.Length; i++) {
                    recipe.text += EleToString(s.recipe[i]);
                }
                if (s.recipe.Length <= 0) { recipe.text += "N/A"; }
            }
            else {
                desc.text = s.knownDes;
                recipe.text = s.knownResp;
            }
        }
    }

    public void UnlockElement(TalisDrag.Elements e) {
        // Unlock the element
        for (int i = 0; i < spell.Length; i++) {
            if (spell[i].element == e) {
                spell[i].curState = Spell.SpellState.UNLOCKED;
                break;
            }
        }

        // Make related recipes known if locked
        for (int i = 0; i < spell.Length; i++) {
            /*
            if (spell[i].curState == Spell.SpellState.LOCKED) {
                for (int j = 0; j < spell[i].recipe.Length; j++) {
                    if (spell[i].recipe[j] == e) {
                        spell[i].curState = Spell.SpellState.KNOWN;
                        break;
                    }
                }
            }
            */
            if (CanCraft(spell[i]) && spell[i].element == TalisDrag.Elements.NONE) {
                spell[i].curState = Spell.SpellState.KNOWN;
            }
        }

        // Unlock in TalismanManager
        GetComponent<TalismanManager>().UnlockElement(e);
    }

    

    public Spell[] GetSpellBook() {
        return spell;
    }
    
}