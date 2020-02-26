using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Field
    // private Elements curEle;
    private string itemName;

    public Item(string name) {
        itemName = name;
    }

    // Functions to be called by other scripts
    public string GetName() { return itemName; }
}
