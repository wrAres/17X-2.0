using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaveIconBright : MonoBehaviour
{
    private Color color = new Color(0.15f, 0.15f, 0.15f, 0.7f);
    public bool shine = false;
    public GameObject arrow;
    public bool spelltreeShown;
    public Vector3 talisPos;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<RawImage>().color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        arrow.GetComponent<Image>().enabled = shine;
    }

    public void ShineBackpack() {
        shine = true;
        this.gameObject.GetComponent<RawImage>().color = color;
        this.gameObject.transform.SetSiblingIndex(0);
        // GameObject spellTree = GameObject.FindGameObjectWithTag("BackpackIcon");
        // spellTree.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChangeAsset/Glowing Backpack");
        //spellTree.transform.localScale = new Vector2(2, 2.3f);
    }
    public void ShineTalisman() {
        shine = true;
        this.gameObject.GetComponent<RawImage>().color = color;
        this.gameObject.transform.SetSiblingIndex(5);
        spelltreeShown = true;
        arrow.transform.localScale *= -1;
        arrow.transform.localPosition = talisPos;
    }

    public void DarkBackpack() {
        shine = false;
        Color colorW = new Color(0f, 0f, 0f, 0f);
        this.gameObject.GetComponent<RawImage>().color = colorW;
    }

    public void ShineSpellIcon() {
        shine = true;
        this.gameObject.GetComponent<RawImage>().color = color;
        this.gameObject.transform.SetSiblingIndex(5);
        GameObject spellTree = GameObject.FindGameObjectWithTag("SpellTreeIcon");
        spellTree.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChangeAsset/Glowing Spelltree");
        // spellTree.transform.localScale = new Vector2(2, 2.3f);
    }
}
