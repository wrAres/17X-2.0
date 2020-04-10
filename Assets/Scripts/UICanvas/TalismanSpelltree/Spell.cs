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
    public Image newDisp;
    public Sprite glow;

    private Sprite ogSprite;
    private Vector3 ogPos, ogScale;
    private bool isNew = true;

    public void OnPointerEnter(PointerEventData eventData) {
        spellTreeDisp.UpdateTextBox(this);
        Vector2 thisPosition = this.gameObject.transform.parent.GetComponent<RectTransform>().anchoredPosition;
        print("position: " + thisPosition);
        if (thisPosition.y < -120) {
            spellTreeDisp.textBox.GetComponent<RectTransform>().anchoredPosition = thisPosition + new Vector2(200f, 100f);
        } else if (thisPosition.x > 300) {
            spellTreeDisp.textBox.GetComponent<RectTransform>().anchoredPosition = thisPosition + new Vector2(-100f, -120f);
        } else {
            spellTreeDisp.textBox.GetComponent<RectTransform>().anchoredPosition = thisPosition + new Vector2(200f, -120f);
        }
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

    public void SetOld() { isNew = false; newDisp.enabled = false; Debug.Log("called"); }

    private void OnEnable() {
        if (curState == SpellState.LOCKED) newDisp.enabled = false;
        else { newDisp.enabled = isNew; }
        
    }
}
