using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellTreeManager : MonoBehaviour {
    [System.Serializable]
    public class Spell {
        public GameObject icon;
        public TalisDrag.Elements element;
        public bool locked;
    }
    public Spell[] spell;
    public GameObject display;

    // Start is called before the first frame update
    void Start() {
        UpdateIcons();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.S)) {
            display.SetActive(true);
        }
        else {
            display.SetActive(false);
        }
    }

    private void UpdateIcons() {
        for (int i = 0; i < spell.Length; i++) {
            if (spell[i].locked) {
                Image img = spell[i].icon.GetComponent<Image>();
                var tempColor = img.color;
                tempColor.a = .5f;
                img.color = tempColor;
            }
        }
    }
}
