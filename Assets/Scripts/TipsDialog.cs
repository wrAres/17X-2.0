using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsDialog : MonoBehaviour
{
    public static GameObject dialog;
    public static Text dialogText;
    public static Dictionary<string, string> dialogList;
    public string waterGateDiscription;
    public string riverDiscription;

    void Start() {
        dialog = GameObject.Find("Dialog Box");
        dialogText = GameObject.Find("Dialog Text").GetComponent<Text>();
        dialogList = new Dictionary<string, string>();
        dialogList.Add("法阵-scene3", waterGateDiscription);
        dialogList.Add("River", riverDiscription);
    }

    public static void PrintDialog(string objName){
        if (dialogList.ContainsKey(objName)) {
            dialogText.text = dialogList[objName];
            dialog.SetActive(true);
        }
    }

    public static void HideTextBox() {
        dialog.SetActive(false);
    }
}