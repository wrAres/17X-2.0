using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private static Hashtable itemOnPuzzle;
    private static string[] availablePutList;

    void Start() {
        itemOnPuzzle = new Hashtable();
        itemOnPuzzle.Add("dirt", "River Flowerpot");
        
    }

    public static bool canPlace(string item, string position) {
        string available = (string)itemOnPuzzle[item];
        if (available == null)
            return false;
        availablePutList = available.Split(' ');
        for (int i = 0; i < availablePutList.Length; i++) {
            if (position.CompareTo(availablePutList[i]) == 0){
                return true;
            }
        }
        return false;
    }
    public static void puzzleEffect(string item, string position) {
        if (item.CompareTo("dirt") == 0 && position.CompareTo("River") == 0){
            GameObject river = GameObject.Find("River");
            river.GetComponent<CapsuleCollider>().radius = 0;
            GameObject.Find("River").GetComponent<Renderer>().material.color = Color.gray;
            // Destroy(GameObject.Find("dirt"));
        }
    }
}
