using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Spell : MonoBehaviour, IPointerEnterHandler {
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
    }

    // Start is called before the first frame update
    void Start() {
        ogPos = transform.position;
        ogScale = transform.localScale;
        ogSprite = GetComponent<Image>().sprite;

        if (curState == SpellState.LOCKED) {
            transform.localPosition = new Vector3(0, 0, 0);
            transform.localScale = new Vector3(1,1,1);
            GetComponent<Image>().sprite = locked;
            Debug.Log("????");
        }
        else { Debug.Log(spellName + " " + curState); }
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void ChangeState(SpellState s) {
        if (curState == SpellState.LOCKED && s != SpellState.LOCKED) {
            transform.position = ogPos;
            transform.localScale = ogScale;
            GetComponent<Image>().sprite = ogSprite;
        }

        curState = s;
    }
}
