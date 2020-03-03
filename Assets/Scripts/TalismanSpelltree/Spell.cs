using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spell : MonoBehaviour, IPointerEnterHandler {
    public SpellTreeManager spellTreeDisp;

    public enum SpellState { LOCKED, KNOWN, UNLOCKED};
    public SpellState curState;

    public string spellName;
    public TalisDrag.Elements[] recipe;
    public int level;
    public string knownDes, unlockedDes;

    public void OnPointerEnter(PointerEventData eventData) {
        Debug.Log(spellName);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
