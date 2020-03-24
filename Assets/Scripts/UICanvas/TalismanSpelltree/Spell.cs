using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Spell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public SpellTreeManager spellTreeDisp;

    public enum SpellState { LOCKED, KNOWN, UNLOCKED };
    public SpellState curState;

    public string spellName;
    public TalisDrag.Elements[] recipe;
    public int level;
    public string knownResp;
    public string knownDes, unlockedDes;
    public TalisDrag.Elements element;
    public Sprite locked;

    private Sprite ogSprite;
    private Vector3 ogPos, ogScale;

    public void OnPointerEnter(PointerEventData eventData) {
        spellTreeDisp.UpdateTextBox(this);
        spellTreeDisp.textBox.transform.position = eventData.position;
    }

    public void OnPointerExit(PointerEventData eventData) {
        spellTreeDisp.HideTextBox();
    }
    

    // Start is called before the first frame update
    void Awake() {
        ogPos = GetComponent<Transform>().localPosition;
        ogScale = GetComponent<Transform>().localScale;
        ogSprite = GetComponent<Image>().sprite;

        if (curState == SpellState.LOCKED) {
            transform.localPosition = new Vector3(0, 0, 0);
            transform.localScale = new Vector3(1,1,1);
            GetComponent<Image>().sprite = locked;
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void ChangeState(SpellState s) {
        if (curState == SpellState.LOCKED && s != SpellState.LOCKED) {
            GetComponent<Transform>().localPosition = ogPos;
            GetComponent<Transform>().localScale = ogScale;
            GetComponent<Image>().sprite = ogSprite;
        }

        curState = s;
    }
}
