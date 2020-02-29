using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalismanManager : MonoBehaviour {
    public GameObject display;
    public float timer;
    public GameObject prefab, player;

    private float curTime;
    public GameObject[] elements;
    public Transform[] elePos;


    private List<GameObject> unlockedEle = new List<GameObject>();
    public GameObject eleSpell;

    private void Start() {
        UnlockElement(eleSpell);
        UnlockElement(eleSpell);
    }

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
            display.SetActive(false);
        }
    }

    private void DisplaySpellList() {
        int curPos = 0;
        for (int i = 0; i < elements.Length; i++) {
            TalisDrag ele = elements[i].GetComponent<TalisDrag>();
            // Show element if it's unlocked
            if (!ele.locked && !ele.known) {
                elements[i].SetActive(true);
                elements[i].GetComponent<RectTransform>().localPosition =
                    Vector3.zero;
                curPos += 1;
            }
            else {
                elements[i].SetActive(false);
            }
        }
    }

    // Functions to be called by other scripts
    public void UnlockElement(GameObject e) {
        if (e.GetComponent<TalisDrag>()) {
            unlockedEle.Add(e);
        }
    }
}
