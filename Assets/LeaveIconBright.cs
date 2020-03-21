using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaveIconBright : MonoBehaviour
{
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
        Color colorI = new Color(0.15f, 0.15f, 0.15f, 0.7f);
        this.gameObject.GetComponent<RawImage>().color = colorI;
        print("original" + colorI);
        print("new" + this.gameObject.GetComponent<RawImage>().color);
        this.gameObject.transform.SetSiblingIndex(0);
    }
}
