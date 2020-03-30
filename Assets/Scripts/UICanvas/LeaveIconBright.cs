using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaveIconBright : MonoBehaviour
{
    private Color color = new Color(0.15f, 0.15f, 0.15f, 0.7f);
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<RawImage>().color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShineBackpack() {
        
        this.gameObject.GetComponent<RawImage>().color = color;
        this.gameObject.transform.SetSiblingIndex(0);
    }

    public void DarkBackpack() {
        Color colorW = new Color(0f, 0f, 0f, 0f);
        this.gameObject.GetComponent<RawImage>().color = colorW;
    }

    public void ShineSpellIcon() {
        this.gameObject.GetComponent<RawImage>().color = color;
        this.gameObject.transform.SetSiblingIndex(6);
        GameObject spellTree = GameObject.FindGameObjectWithTag("SpellTreeIcon");
        spellTree.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChangeAsset/Glowing Spelltree");
        spellTree.transform.localScale = new Vector2(2, 2.3f);
    }
}
